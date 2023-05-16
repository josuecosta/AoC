using Aoc21;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AOC21.BL
{
    public class SnailfishHomework
    {
        private List<Snailfish> snailfishes;

        private Snailfish homework;

        public SnailfishHomework(string[] data)
        {
            this.snailfishes = data.Select(s => new Snailfish(s)).ToList();
        }

        internal void DoHomework()
        {
            homework = snailfishes.First();
            for (int i = 1; i < snailfishes.Count; i++)
            {
                Addition(snailfishes[i]);
            }
        }

        private void Addition(Snailfish snailfish)
        {
            homework = homework.Add(snailfish);
            homework = homework.Reduce();
        }

        internal decimal GetMagnitude()
        {
            return homework.Magnitude;
        }

        internal decimal GetLargestMagnitude()
        {
            var bestMagnitude = 0;

            for (int line = 0; line < snailfishes.Count; line++)
            {
                for (int i = 0; i < snailfishes.Count; i++)
                {
                    homework = snailfishes[line];

                    if (line == i)
                    {
                        continue;
                    }

                    Addition(snailfishes[i]);

                    var magnitude = homework.Magnitude;
                    if (magnitude > bestMagnitude)
                    {
                        bestMagnitude = magnitude;
                    }
                }
            }
            return bestMagnitude;
        }
    }

    public class Snailfish
    {
        public Snailfish X { get; set; }
        public Snailfish Y { get; set; }
        public int Value { get; set; }
        public bool IsNumber => X == null && Y == null;

        public int Magnitude
        {
            get
            {
                if (IsNumber)
                {
                    return this.Value;
                }
                var x = X.IsNumber ? X.Value : X.Magnitude;
                var y = Y.IsNumber ? Y.Value : Y.Magnitude;
                return (x * 3) + (y * 2);
            }
        }

        public Snailfish(string snailfish)
        {
            var commaIndex = FoundPairMiddle(snailfish);
            var left = snailfish.Substring(1, commaIndex - 1);
            var right = snailfish.Substring(commaIndex + 1);
            right = right.Remove(right.Length - 1, 1); // remove last char

            var isNumber = int.TryParse(left, out var number);
            this.X = isNumber ? new Snailfish(number) : new Snailfish(left);

            isNumber = int.TryParse(right, out number);
            this.Y = isNumber ? new Snailfish(number) : new Snailfish(right);
        }

        public Snailfish(int number)
        {
            this.Value = number;
        }

        public Snailfish(Snailfish x, Snailfish y)
        {
            this.X = x;
            this.Y = y;
        }

        internal Snailfish Add(Snailfish newSnailfish) => new Snailfish(this, newSnailfish);

        internal Snailfish Reduce()
        {
            var snailfish = this.ToString();

            var isSplit = true;
            while (isSplit)
            {
                snailfish = ExplodeAll(snailfish);

                var numbers = Helpers.RegexNumber.Matches(snailfish);
                isSplit = numbers.Any(v => int.Parse(v.Value) >= 10) ? true : false;

                if (isSplit)
                {
                    snailfish = Split(snailfish);
                }
            }

            return new Snailfish(snailfish);
        }

        private string ExplodeAll(string snailfishAsString)
        {
            var level = -1;
            for (int i = 0; i < snailfishAsString.Length; i++)
            {
                var c = snailfishAsString[i];
                level = UpdateLevel(level, c);

                if (level == 4)
                {
                    snailfishAsString = Explode(snailfishAsString, i);
                    i = 0;
                    level = 0;
                }
            }
            return snailfishAsString;
        }

        private string Explode(string snailfishAsString, int index)
        {
            var sb = new StringBuilder(snailfishAsString);
            // Get numbers from pair to be removed
            var numbers = GetNumbersFromSnailfish(snailfishAsString, index);
            // Remove nested pair » [x,y]
            sb.Remove(index, snailfishAsString.IndexOf(']', index) - index + 1);
            // Update with '0' value
            sb.Insert(index, "0");
            // Update neighbors with value from removed pair
            UpdateNeighbors(sb, numbers.x, numbers.y, index);
            return sb.ToString();
        }

        private static string Split(string snailfish)
        {
            var splitNumber = Helpers.RegexNumber.Matches(snailfish)
                                                 .FirstOrDefault(m => int.Parse(m.Value) >= 10);
            var newPair = GetPairFromSplit(splitNumber.Value);

            var sb = new StringBuilder(snailfish);
            sb.Remove(splitNumber.Index, splitNumber.Length);
            sb.Insert(splitNumber.Index, newPair);

            return sb.ToString();
        }

        private static int FoundPairMiddle(string pair)
        {
            var level = -1;
            for (int i = 0; i < pair.Length; i++)
            {
                var c = pair[i];
                if (c == ',' && level == 0)
                {
                    return i;
                }
                level = UpdateLevel(level, c);
            }
            return 0;
        }

        private static (int x, int y) GetNumbersFromSnailfish(string snailfishAsString, int index)
        {
            var matches = Helpers.RegexPairOfNumbersInsideBrackets.Matches(snailfishAsString, index);
            var snailfish = matches.First().Value;
            var numbers = Helpers.RegexNumber.Matches(snailfish);
            return (int.Parse(numbers.First().Value), int.Parse(numbers.Last().Value));
        }

        private static string GetPairFromSplit(string splitNumber)
        {
            var number = int.Parse(splitNumber);
            var numDivBy2 = number / 2;
            var x = numDivBy2;
            var y = number % 2 == 0 ? numDivBy2 : numDivBy2 + 1;
            return $"[{x},{y}]";
        }

        private static int UpdateLevel(int level, char c)
            => c == '[' ? level + 1
             : c == ']' ? level - 1
             : level;

        private void UpdateNeighbors(StringBuilder snailfish, int x, int y, int startIndex)
        {
            var leftPart = new StringBuilder(snailfish.ToString().Substring(0, startIndex));
            var rightPart = new StringBuilder(snailfish.ToString().Substring(startIndex));
            snailfish.Clear();

            var numbers = Helpers.RegexNumber.Matches(leftPart.ToString());
            var leftNumber = numbers.LastOrDefault();
            snailfish.Append(UpdatePairValue(leftPart, leftNumber, x));

            numbers = Helpers.RegexNumber.Matches(rightPart.ToString());
            var rightNumber = numbers.Skip(1).FirstOrDefault(); // ignore added 0
            snailfish.Append(UpdatePairValue(rightPart, rightNumber, y));
        }

        private static string UpdatePairValue(StringBuilder sb, Match number, int valueToSum)
        {
            if (number != null)
            {
                var newValue = int.Parse(number.Value) + valueToSum;
                sb.Remove(number.Index, number.Length);
                sb.Insert(number.Index, newValue);
            }
            return sb.ToString();
        }

        public override string ToString()
        {
            var sb = new StringBuilder("[");
            sb.Append(X.IsNumber ? X.Value : X.ToString());
            sb.Append(',');
            sb.Append(Y.IsNumber ? Y.Value : Y.ToString());
            sb.Append(']');
            return sb.ToString();
        }
    }
}