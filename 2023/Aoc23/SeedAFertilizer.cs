using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Aoc23
{
    internal class SeedAFertilizer
    {
        private List<Category> _categories;
        private List<int> _seeds;
        private List<FarmRange> _seedsRange;

        public SeedAFertilizer(string[] data)
        {
            this._seeds = new List<int>();
            this._seedsRange = new List<FarmRange>();
            this._categories = InitCategories(data);
        }

        private List<Category> InitCategories(string[] data)
        {
            var categories = new List<Category>();

            // Seeds
            var seeds = Helpers.RegexNumbers.Matches(data[0]);
            //foreach (Match seed in seeds) 
            //{
            //    _seeds.Add(decimal.Parse(seed.Value));
            //}
            for (int i = 0; i < seeds.Count; i+=2)
            {
                _seedsRange.Add(new FarmRange(
                    decimal.Parse(seeds[i].Value), 
                    0, 
                    decimal.Parse(seeds[i+1].Value)));
            }

            // Categories 
            for (int i = 2; i < data.Length; i++)
            {
                var typeName = data[i++].Split(' ')[0].Split('-').Last();
                var category = new Category(typeName);
                while (i < data.Length && !string.IsNullOrEmpty(data[i]))
                {
                    var rawInfo = Helpers.RegexNumbers.Matches(data[i]);
                    var farmRange = new FarmRange(
                            decimal.Parse(rawInfo[1].Value),    // source
                            decimal.Parse(rawInfo[0].Value),    // destination
                            decimal.Parse(rawInfo[2].Value)     // length
                        );
                    category.Ranges.Add(farmRange);
                    i++;
                }
                categories.Add(category);
            }

            // Map categories
            var soil        = categories.First(c => c.Type == CategoryType.Soil);
            var fertilizer  = categories.First(c => c.Type == CategoryType.Fertilizer);
            var water       = categories.First(c => c.Type == CategoryType.Water);
            var light       = categories.First(c => c.Type == CategoryType.Light);
            var temperature = categories.First(c => c.Type == CategoryType.Temperature);
            var humidity    = categories.First(c => c.Type == CategoryType.Humidity);
            var location    = categories.First(c => c.Type == CategoryType.Location);

            soil.Destination        = fertilizer;
            fertilizer.Destination  = water;
            water.Destination       = light;
            light.Destination       = temperature;
            temperature.Destination = humidity;
            humidity.Destination    = location;

            return categories;
        }

        internal decimal GetLowestLocation()
        {
            var min = decimal.MaxValue;
            foreach (var seed in _seeds)
            {
                var category = _categories.FirstOrDefault(c => c.Type == CategoryType.Soil);
                var location = category.GetNextMapping(seed);
                if (location < min)
                {
                    min = location;
                }
            }
            return min;
        }

        internal decimal GetLowestLocationByRange()
        {
            var min = decimal.MaxValue;
            var soil = _categories.FirstOrDefault(c => c.Type == CategoryType.Soil);
            Parallel.ForEach(_seedsRange, range =>
            {
                for (int i = 0; i < range.Length; i++)
                {
                    var location = soil.GetNextMapping(range.Source + i);
                    if (location < min)
                    {
                        min = location;
                    }
                }
            });
            return min;
        }
    }

    internal class Category
    {
        public Category(string typeName)
        {
            if (Enum.TryParse(typeName, true, out CategoryType parsedEnum))
            {
                this.Type = (CategoryType)parsedEnum;
            }
            Ranges = new List<FarmRange>();
        }

        public CategoryType Type { get; set; }
        public Category Destination { get; set; }
        public List<FarmRange> Ranges { get; set; }
        private decimal GetMappingNumber(decimal seed)
        {
            var numberRange = Ranges.FirstOrDefault(n =>
                (n.Source <= seed && seed <= (n.Source + n.Length - 1)));

            if (numberRange == null)
            {
                return seed;
            }

            return seed + (numberRange.Destination - numberRange.Source);
        }        

        public decimal GetNextMapping(decimal seed)
        {
            var number = GetMappingNumber(seed);
            if (this.Type == CategoryType.Location)
            {
                return number;
            } 
            return Destination.GetNextMapping(number);
        }
    }
    
    internal class FarmRange
    {
        public FarmRange(decimal source, decimal destination, decimal length)
        {
            Source = source;
            Destination = destination;
            Length = length;
        }
        public decimal Source { get; set; }
        public decimal Destination { get; set; }
        public decimal Length { get; set; }
    }

    internal enum CategoryType
    {
        Seed,
        Soil,
        Fertilizer,
        Water,
        Light,
        Temperature,
        Humidity,
        Location
    }
}