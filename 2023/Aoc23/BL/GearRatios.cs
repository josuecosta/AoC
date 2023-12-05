using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text.RegularExpressions;

namespace Aoc23
{
    internal class GearRatios
    {
        private List<MotorNumber> map;

        public GearRatios(string[] data)
        {
            map = InitMap(data);
        }

        public List<int> GetPartNumbers() 
            => map.Where(mn => IsPartNumber(mn))
                  .Select(x => x.Value)
                  .ToList();

        private bool IsPartNumber(MotorNumber motorNumber)
        {
            if (motorNumber.Symbol != null)
            {
                return false;
            }

            var neighbours = GetNeighbours(motorNumber, motorNumber.Value.ToString().Length)
                                .Where(k => k.Symbol != null);

            return neighbours.Count() > 0;
        }

        private List<MotorNumber> InitMap(string[] data)
        {
            var list = new List<MotorNumber>();
            for (int row = 0; row < data.Length; row++)
            {
                // Numbers
                var numbers = Helpers.RegexNumbers.Matches(data[row]);
                foreach (Match number in numbers)
                {
                    list.Add(new MotorNumber(number.Index, row, int.Parse(number.Value)));
                }

                // Symbols » Periods (.) do not count as a symbol
                var symbols = new Regex("[^0-9.]+").Matches(data[row]);
                foreach (Match symbol in symbols)
                {
                    list.Add(new MotorNumber(symbol.Index, row, symbol.Value));
                }
            }
            return list;
        }

        public List<decimal> GetGearRatios()
        {
            var racios = new List<decimal>();

            var dic = new Dictionary<Coordinates, List<MotorNumber>>();

            foreach (var partNumber in map.Where(mn => IsPartNumber(mn)))
            {
                var neighbours = GetNeighbours(partNumber, partNumber.Value.ToString().Length)
                                        .Where(mn => mn.Symbol == "*");

                foreach (var neighbour in neighbours)
                {
                    var coordinate = new Coordinates(neighbour.X, neighbour.Y);
                    if (!dic.ContainsKey(coordinate))
                    {
                        dic[coordinate] = new List<MotorNumber>();
                    }
                    dic[coordinate].Add(partNumber);
                }
            }

            foreach (var gear in dic.Where(mn => mn.Value.Count == 2))
            {
                var listOfValues = gear.Value.Select(mn => Convert.ToDecimal(mn.Value));
                racios.Add(listOfValues.Aggregate((acc, x) => acc * x));
            }

            return racios;
        }

        public IEnumerable<MotorNumber> GetNeighbours(Coordinates c, int length)
        {
            var y = c.Y;
            var x = c.X;
            var xMax = x + length - 1;

            return map.Where(k =>
                               (x - 1) <= k.X && k.X <= (xMax + 1)
                            && (y - 1) <= k.Y && k.Y <= (y + 1));
        }
    }

    internal class MotorNumber : Coordinates
    {
        public MotorNumber(int x, int y, int value)
            :base(x, y)
        {
            this.Value = value;
        }

        public MotorNumber(int x, int y, string symbol)
            :base(x, y)
        {
            this.Symbol = symbol;
        }

        public int Value { get; set; }
        public string Symbol { get; set; }
    }
}