using MoreLinq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace Aoc23
{
    public class Helpers
    {
        public static Regex RegexPairInsideBrackets = new Regex("\\[[0-9,\\[\\]]*\\]");
        public static Regex RegexNumbers = new Regex("[0-9]+");

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

        public static decimal CalcManhattanDistance(Coordinates c1, Coordinates c2)
            => Math.Abs(c1.X - c2.X) + Math.Abs(c1.Y - c2.Y);

        public static decimal CalcManhattanDistance(int x1, int y1, int x2, int y2)
            => CalcManhattanDistance(new Coordinates(x1, y1), new Coordinates(x2, y2));
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
}