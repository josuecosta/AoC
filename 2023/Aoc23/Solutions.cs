using Aoc23.BL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Aoc23
{
    public class Solutions
    {
        public static string Input;
        public string Solution;

        #region DAY 1

        public decimal Day1Solve()
        {
            var data = File.ReadAllLines(Input);
            decimal sum = 0;

            for (int i = 0; i < data.Length; i++)
            {
                var l = data[i].FirstOrDefault(c => char.IsDigit(c)).ToString();
                var r = data[i].LastOrDefault(c => char.IsDigit(c)).ToString();
                sum += decimal.Parse($"{l}{r}");
            }

            return sum;
        }

        public decimal Day1Solve2()
        {
            var data = File.ReadAllLines(Input);
            var dic = new Dictionary<string, int>()
            {
                { "1", 1 },   { "2", 2 },   { "3", 3 },     { "4", 4 },    { "5", 5 },    { "6", 6 },   { "7", 7 },     { "8", 8 },     { "9", 9 },
                { "one", 1 }, { "two", 2 }, { "three", 3 }, { "four", 4 }, { "five", 5 }, { "six", 6 }, { "seven", 7 }, { "eight", 8 }, { "nine", 9 }
            };

            decimal sum = 0;

            for (int i = 0; i < data.Length; i++)
            {
                var l = dic.Select(kv => new KeyValuePair<int, int>(data[i].IndexOf(kv.Key), kv.Value))
                           .OrderBy(k => k.Key)
                           .FirstOrDefault(k => k.Key > -1)
                           .Value;

                var r = dic.Select(kv => new KeyValuePair<int, int>(data[i].LastIndexOf(kv.Key), kv.Value))
                           .OrderBy(k => k.Key)
                           .LastOrDefault(k => k.Key > -1)
                           .Value;

                sum += decimal.Parse($"{l}{r}");
            }

            return sum;
        }

        #endregion DAY 1

        #region DAY 2

        public decimal Day2Solve()
        {
            var data = File.ReadAllLines(Input);

            var cube = new CubeConundrumGame(data);

            return cube.Games.Where(g => g.IsPossible).Sum(g => g.ID);
        }

        public decimal Day2Solve2()
        {
            var data = File.ReadAllLines(Input);

            var cube = new CubeConundrumGame(data);

            return cube.Games.Sum(g => g.PowerOfSetCubes);
        }

        #endregion DAY 2

        #region DAY 3

        public decimal Day3Solve()
        {
            var data = File.ReadAllLines(Input);

            var gear = new GearRatios(data);

            return gear.GetPartNumbers().Sum();
        }

        public decimal Day3Solve2()
        {
            var data = File.ReadAllLines(Input);

            var gear = new GearRatios(data);

            return gear.GetGearRatios().Sum();
        }

        #endregion DAY 3

        #region DAY 4

        public decimal Day4Solve()
        {
            var data = File.ReadAllLines(Input);

            var cards = new Scratchcards(data);

            return (decimal)cards.GetPoints();
        }

        public decimal Day4Solve2()
        {
            var data = File.ReadAllLines(Input);

            var cards = new Scratchcards(data);

            return cards.GetTotalScratchcards();
        }

        #endregion DAY 4

        public Solutions(bool isTest = false)
        {
            Input = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), isTest ? "input-test.txt" : "input.txt");
            Solution = Day4Solve2().ToString();
        }
    }
}