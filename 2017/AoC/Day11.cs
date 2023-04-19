namespace AoC17
{
    using System;
    using System.Linq;

    internal class Day11 : Master
    {
        public void Run()
        {
            // Part 1
            var x = 0;
            var y = 0;
            var maxDistance = 0;

            foreach (var direction in this.Input.First().Split(','))
            {
                switch (direction)
                {
                    case "n":
                        y++;
                        break;

                    case "ne":
                        x++;
                        break;

                    case "se":
                        x++; y--;
                        break;

                    case "s":
                        y--;
                        break;

                    case "sw":
                        x--;
                        break;

                    case "nw":
                        x--; y++;
                        break;
                }

                // Part 2
                var distance = this.GetHexDistance(x, y);
                maxDistance = distance > maxDistance ? distance : maxDistance;
            }

            this.Output1 = this.GetHexDistance(x, y);
            this.Output2 = maxDistance;
        }

        private int GetHexDistance(int x, int y)
        {
            if (x > 0 && y > 0)
            {
                return x + y;
            }
            else if (x < 0 && y < 0)
            {
                return Math.Abs(x) + Math.Abs(y);
            }
            else
            {
                return x > y ? x : y;
            }
        }
    }
}