using MoreLinq;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace Aoc22
{
    public class Helpers
    {
        public static Regex RegexPairInsideBrackets = new Regex("\\[[0-9,\\[\\]]*\\]");

        public bool isValidPassword(string item)
        {
            var data = item.Split(' ').Select(x => x.Trim()).ToList();
            var minMax = data[0].Split('-');
            var min = int.Parse(minMax[0].ToString());
            var max = int.Parse(minMax[1].ToString());
            var letter = data[1][0];
            var password = data[2];
            var numberOfLettersInPassword = password.Count(x => x == letter);

            if (numberOfLettersInPassword >= min && numberOfLettersInPassword <= max)
            {
                return true;
            }
            return false;
        }

        public int FixMapCircularReference(string[] data, int x, int y)
        {
            var numberOfTrees = 0;
            var currentX = 0;
            var maxX = data.First().Length;

            for (int i = y; i <= data.Length - 1; i += y)
            {
                currentX += x;
                // Fix map circular reference
                currentX %= maxX;
                if (data[i][currentX] == '#')
                {
                    numberOfTrees++;
                }
            }
            return numberOfTrees;
        }

        public bool ContainsAllElementsOfAList(Dictionary<string, string> passport)
        {
            var requiredKeys = new string[] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };

            return requiredKeys.All(x => passport.ContainsKey(x));
        }
    }

    public class Coordinates
    {
        public Coordinates()
        {
            X = 0;
            Y = 0;
        }

        public Coordinates(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Coordinates(int x, int y, bool isMarked)
        {
            X = x;
            Y = y;
            IsMarked = isMarked;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public bool IsMarked { get; set; }

        public override bool Equals(object obj)
        {
            var item = obj as Coordinates;

            if (item == null)
            {
                return false;
            }

            return this.X == item.X && this.Y == item.Y;
        }

        public override int GetHashCode()
        {
            return (X, Y).GetHashCode();
        }
    }

    public class GameRPS
    {
        public enum ERPSOptions
        {
            MRock,
            MPaper,
            MScissor,

            ORock,
            OPaper,
            OScissor
        }

        public int Rock { get; set; }
        public int Paper { get; set; }
        public int Scissor { get; set; }
        public int Win { get; set; }
        public int Draw { get; set; }
        public List<(string Opo, string Result)> Plays { get; set; }

        public GameRPS()
        {
            Rock = 1;
            Paper = 2;
            Scissor = 3;

            Win = 6;
            Draw = 3;

            Plays = new List<(string Opo, string Result)>();
        }

        internal void BuildPlays(string[] data)
        {
            foreach (var play in data)
            {
                var playSplitted = play.Split(' ');
                Plays.Add((playSplitted[0], playSplitted[1]));
            }
        }

        internal int Play(int index)
        {
            var play = Plays[index];
            return CalcResult(play);
        }

        private int CalcResult((string Opo, string Result) play)
        {
            if (play.Result == "X") // LOSS
            {
                if (play.Opo == DecryptPlay(ERPSOptions.OPaper))
                {
                    return Rock;
                }
                if (play.Opo == DecryptPlay(ERPSOptions.ORock))
                {
                    return Scissor;
                }
                if (play.Opo == DecryptPlay(ERPSOptions.OScissor))
                {
                    return Paper;
                }
            }
            if (play.Result == "Y") // DRAW
            {
                if (play.Opo == DecryptPlay(ERPSOptions.ORock))
                {
                    return Draw + Rock;
                }
                if (play.Opo == DecryptPlay(ERPSOptions.OScissor))
                {
                    return Draw + Scissor;
                }
                if (play.Opo == DecryptPlay(ERPSOptions.OPaper))
                {
                    return Draw + Paper;
                }
            }
            if (play.Result == "Z") // WIN
            {
                if (play.Opo == DecryptPlay(ERPSOptions.OScissor))
                {
                    return Win + Rock;
                }
                if (play.Opo == DecryptPlay(ERPSOptions.OPaper))
                {
                    return Win + Scissor;
                }
                if (play.Opo == DecryptPlay(ERPSOptions.ORock))
                {
                    return Win + Paper;
                }
            }
            return 0;
        }

        private string DecryptPlay(ERPSOptions options)
        {
            switch (options)
            {
                case ERPSOptions.ORock:
                    return "A";

                case ERPSOptions.OPaper:
                    return "B";

                case ERPSOptions.OScissor:
                    return "C";

                default:
                    return "";
            }
        }
    }

    public class Rucksack
    {
        public string Material { get; set; }

        public char CommonType
        {
            get
            {
                var LComparment = Material.Substring(0, (Material.Length / 2));
                var RComparment = Material.Substring((Material.Length / 2));
                return LComparment.FirstOrDefault(lc => RComparment.Any(rc => lc == rc));
            }
        }

        public decimal GetPriority(char commonType) => PriorityDic[commonType];

        internal void IdentifyCommonBadge(List<Rucksack> rucksacks)
        {
            var firstEquals = rucksacks[0].Material.Where(lc => rucksacks[1].Material.Any(rc => lc == rc));
            CommonBadge = rucksacks[2].Material.FirstOrDefault(lc => firstEquals.Any(rc => lc == rc));
        }

        public Dictionary<char, int> PriorityDic
        {
            get
            {
                var dic = new Dictionary<char, int>();
                for (int i = 1; i <= 26; i++)
                {
                    dic.Add((char)(i + 96), i);
                }
                for (int i = 27; i <= 52; i++)
                {
                    dic.Add((char)(38 + i), i);
                }
                return dic;
            }
        }

        public char CommonBadge { get; internal set; }

        public Rucksack(string material)
        {
            this.Material = material;
        }
    }

    public class SectionJobs
    {
        private List<Pair> Pairs { get; set; }

        private class Pair
        {
            public Assignment LPair { get; set; }
            public Assignment RPair { get; set; }

            public Pair(string LAssignment, string RAssignment)
            {
                var assignmentSplitted = LAssignment.Split('-');
                LPair = new Assignment(assignmentSplitted[0], assignmentSplitted[1]);

                assignmentSplitted = RAssignment.Split('-');
                RPair = new Assignment(assignmentSplitted[0], assignmentSplitted[1]);
            }
        }

        private class Assignment
        {
            public Assignment(string v1, string v2)
            {
                this.Min = int.Parse(v1);
                this.Max = int.Parse(v2);
            }

            public int Min { get; set; }
            public int Max { get; set; }
        }

        private string[] data;

        public SectionJobs(string[] data)
        {
            this.data = data;
            this.Pairs = new List<Pair>();
            ParseData();
        }

        private void ParseData()
        {
            for (int i = 0; i < this.data.Length; i++)
            {
                var pairsSplitted = this.data[i].Split(',');
                Pairs.Add(new Pair(pairsSplitted[0], pairsSplitted[1]));
            }
        }

        internal decimal GetFullyContainedPairs()
        {
            var count = 0;
            foreach (var pair in Pairs)
            {
                if (IsFullyContainedPair(pair))
                    count++;
            }
            return count;
        }

        internal decimal GetOverlapedPairs()
        {
            var count = 0;
            foreach (var pair in Pairs)
            {
                if (IsOverlapedPair(pair))
                    count++;
            }
            return count;
        }

        private bool IsFullyContainedPair(Pair pair)
        {
            var LRange = Enumerable.Range(pair.LPair.Min, pair.LPair.Max - pair.LPair.Min + 1);
            var RRange = Enumerable.Range(pair.RPair.Min, pair.RPair.Max - pair.RPair.Min + 1);

            return LRange.All(n => RRange.Contains(n)) || RRange.All(n => LRange.Contains(n));
        }

        private bool IsOverlapedPair(Pair pair)
        {
            var LRange = Enumerable.Range(pair.LPair.Min, pair.LPair.Max - pair.LPair.Min + 1);
            var RRange = Enumerable.Range(pair.RPair.Min, pair.RPair.Max - pair.RPair.Min + 1);

            return LRange.Any(n => RRange.Contains(n));
        }
    }
}