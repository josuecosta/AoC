using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC21.BL
{
    public class SnailfishHomework
    {
        //private string[] data;
        //private string pairPattern = "\\[[0-9]+,[0-9]+\\]";
        private List<Snailfish> snailfishes;

        private Snailfish homework;

        public SnailfishHomework(string[] data)
        {
            this.snailfishes = data.Select(s => new Snailfish(s)).ToList();
        }

        internal void DoHomework()
        {
            homework = snailfishes.First();
            homework = homework.Reduce();
            for (int i = 1; i < snailfishes.Count; i++)
            {
                //homework = homework.Add(snailfishes[i]);
                homework = homework.Reduce();
            }
        }

        internal decimal GetMagnitude()
        {
            return homework.Magnitude;
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

        public Snailfish Parent { get; set; }

        public Snailfish(string snailfish)
        {
            var commaIndex = FoundPairMiddle(snailfish);
            var left = snailfish.Substring(1, commaIndex - 1);
            var right = snailfish.Substring(commaIndex + 1);
            right = right.Remove(right.Length - 1, 1); // remove last char

            var isNumber = int.TryParse(left, out var number);
            this.X = isNumber ? new Snailfish(number) : new Snailfish(left);
            this.X.Parent = this;

            isNumber = int.TryParse(right, out number);
            this.Y = isNumber ? new Snailfish(number) : new Snailfish(right);
            this.Y.Parent = this;
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

        private int FoundPairMiddle(string pair)
        {
            var level = -1;
            for (int i = 0; i < pair.Length; i++)
            {
                var c = pair[i];
                if (c == ',' && level == 0)
                {
                    return i;
                }
                else if (c == '[')
                {
                    level++;
                }
                else if (c == ']')
                {
                    level--;
                }
            }
            return 0;
        }

        internal Snailfish Add(Snailfish newSnailfish)
        {
            var addedSnailfish = new Snailfish(this, newSnailfish);
            return addedSnailfish;
        }

        internal Snailfish Reduce()
        {
            // explode
            Explode(1);

            // split
            throw new NotImplementedException();
        }

        private bool Explode(int level)
        {
            // Find
            if (IsNumber)
            {
                return false;
            }

            if (level < 4)
            {
                level++;
                if(X.Explode(level))
                {
                    var x = X.X.X.Value;
                    var y = X.X.Y.Value;
                    Parent.X.IsNumber;
                }
                if (Y.Explode(level))
                {

                }
            }
            else
            {
                if (!X.IsNumber)
                {
                    //var x = X.X.Value;
                    //var y = X.Y.Value;
                    //ExplodePair(X);
                    return true;
                    //UpdateNeighbors(x,y);
                }
                else if (!Y.IsNumber)
                {
                    //var x = Y.X.Value;
                    //var y = Y.Y.Value;
                    //ExplodePair(Y);
                    return true;
                    //UpdateNeighbors(x,y);
                }
            }
            return false;
        }

        private void UpdateNeighbors(int x, int y)
        {
            // convert to strign
            // update string
            // convert back to entity
            throw new NotImplementedException();
        }

        private void ExplodePair(Snailfish snailfish)
        {
            snailfish.X = null;
            snailfish.Y = null;
            snailfish.Value = 0;
        }
    }
}