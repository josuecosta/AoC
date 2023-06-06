using Aoc22.BL;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Aoc22
{
    public class Solutions
    {
        public static string Input;
        public string Solution;

        #region DAY 1

        public decimal Day1Solve()
        {
            var data = File.ReadAllLines(Input);
            var elfList = new Dictionary<int, decimal>();
            var index = 0;

            for (int i = 0; i < data.Length; i++)
            {
                if (!elfList.ContainsKey(index))
                {
                    elfList.Add(index, 0);
                }

                if (string.IsNullOrEmpty(data[i]))
                {
                    index++;
                    continue;
                }

                elfList[index] += int.Parse(data[i]);
            }

            return elfList.Max(x => x.Value);
        }

        public decimal Day1Solve2()
        {
            var data = File.ReadAllLines(Input);
            var elfList = new Dictionary<int, decimal>();
            var index = 0;

            for (int i = 0; i < data.Length; i++)
            {
                if (!elfList.ContainsKey(index))
                {
                    elfList.Add(index, 0);
                }

                if (string.IsNullOrEmpty(data[i]))
                {
                    index++;
                    continue;
                }

                elfList[index] += int.Parse(data[i]);
            }

            return elfList.OrderByDescending(x => x.Value).Take(3).Sum(x => x.Value);
        }

        #endregion DAY 1

        #region DAY 2

        public decimal Day2Solve()
        {
            var data = File.ReadAllLines(Input);
            var game = new GameRPS();
            game.BuildPlays(data);

            var score = 0;
            for (int i = 0; i < game.Plays.Count; i++)
            {
                score += game.Play(i);
            }

            return score;
        }

        #endregion DAY 2

        #region DAY 3

        public decimal Day3Solve()
        {
            var data = File.ReadAllLines(Input);
            var rucksacks = new List<Rucksack>();

            for (int i = 0; i < data.Length; i++)
            {
                rucksacks.Add(new Rucksack(data[i]));
            }

            return rucksacks.Sum(x => x.GetPriority(x.CommonType));
        }

        public decimal Day3Solve2()
        {
            var data = File.ReadAllLines(Input);
            var groups = new Dictionary<int, List<Rucksack>>();

            var groupNo = 1;
            for (int i = 0; i < data.Length; i++)
            {
                if (i != 0 && i % 3 == 0)
                {
                    groupNo++;
                }

                if (!groups.ContainsKey(groupNo))
                {
                    groups.Add(groupNo, new List<Rucksack>());
                }

                groups[groupNo].Add(new Rucksack(data[i]));
            }

            foreach (var item in groups.Values)
            {
                item.FirstOrDefault().IdentifyCommonBadge(item);
            }

            return groups.Values.Sum(x => x.FirstOrDefault().GetPriority(x.FirstOrDefault().CommonBadge));
        }

        #endregion DAY 3

        #region DAY 4

        public decimal Day4Solve()
        {
            var data = File.ReadAllLines(Input);
            var pairs = new SectionJobs(data);

            // return pairs.GetFullyContainedPairs(); Part 1
            return pairs.GetOverlapedPairs();
        }

        #endregion DAY 4

        #region DAY 5

        public decimal Day5Solve()
        {
            var data = File.ReadAllLines(Input);
            var pairs = new SectionJobs(data);

            return pairs.GetOverlapedPairs();
        }

        #endregion DAY 5

        // Day 6 ~ 11 made in Java

        #region DAY 12

        public decimal Day12Solve()
        {
            var data = File.ReadAllLines(Input);

            var hill = new HillClimbingAlgorithm(data);

            return hill.GetSteps();
        }

        #endregion DAY 12

        #region DAY 13

        public decimal Day13Solve()
        {
            var data = File.ReadAllLines(Input);

            var distressSignal = new DistressSignal(data);

           return distressSignal.GetSumOfPacketsInOrder();
        }

        public decimal Day13Solve2()
        {
            var data = File.ReadAllLines(Input);

            var distressSignal = new DistressSignal(data);

            return distressSignal.GetDecoderKey();
        }

        #endregion DAY 13

        #region DAY 14

        public decimal Day14Solve()
        {
            var data = File.ReadAllLines(Input);

            var regolithReservoir = new RegolithReservoir(data);

            return regolithReservoir.GetUnitsOfSandAtRest();
        }

        public decimal Day14Solve2()
        {
            var data = File.ReadAllLines(Input);

            var regolithReservoir = new RegolithReservoir(data);

            return regolithReservoir.GetUnitsOfSandToBlockSource();
        }

        #endregion DAY 14

        public Solutions(bool isTest = false)
        {
            Input = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), isTest ? "input-test.txt" : "input.txt");
            Solution = Day14Solve2().ToString();
        }
    }
}