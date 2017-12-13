namespace AoC17
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Day3 : Master
    {
        public void Run()
        {
            // Part 1
            foreach (var row in this.Input)
            {
                var number = int.Parse(row);
                var x = 0;
                var y = 0;
                var steps = 1;

                var dic = new Dictionary<string, int> { { "0,0", 1 } };

                for (int i = 2; i <= number;)
                {
                    var count = 0;

                    while (count < steps)
                    {
                        x = steps % 2 == 0 ? x + 1 : x - 1;
                        dic.Add(x + "," + y, i++);
                        count++;
                    }

                    count = 0;
                    while (count < steps)
                    {
                        y = steps % 2 == 0 ? y + 1 : y - 1;
                        dic.Add(x + "," + y, i++);
                        count++;
                    }

                    steps++;
                }

                var coordinates = dic.Where(p => p.Value == number).First().Key.Split(",");
                this.Output1 = Math.Abs(int.Parse(coordinates[0])) + Math.Abs(int.Parse(coordinates[1]));
            }

            // Part 2
            foreach (var row in this.Input)
            {
                var number = int.Parse(row);
                var x = 0;
                var y = 0;
                var steps = 1;

                var dic = new Dictionary<string, int> { { "0,0", 1 } };

                for (int i = 2; i <= number;)
                {
                    var count = 0;

                    while (count < steps)
                    {
                        x = steps % 2 == 0 ? x + 1 : x - 1;
                        var res = GetSumNeighbors(x, y, dic);
                        dic.Add(x + "," + y, res);

                        if (res > number)
                        {
                            i = number + 1; steps = -1;
                            this.Output2 = res;
                            break;
                        }

                        count++;
                    }

                    count = 0;
                    while (count < steps)
                    {
                        y = steps % 2 == 0 ? y + 1 : y - 1;
                        var res = GetSumNeighbors(x, y, dic);
                        dic.Add(x + "," + y, res);

                        if (res > number)
                        {
                            i = number + 1; steps = -1;
                            this.Output2 = res;
                            break;
                        }
                        count++;
                    }
                    steps++;
                }
            }
        }

        private int GetSumNeighbors(int x, int y, Dictionary<string, int> dic)
        {
            var neighbors = new List<string>
            {
                (x-1) + "," + (y+1), x + "," + (y+1), (x+1) + "," + (y+1),
                (x-1) + "," + y, (x+1) + "," + y,
                (x-1) + "," + (y-1), x + "," + (y-1), (x+1) + "," + (y-1)
            };

            return dic.Where(k => neighbors.Contains(k.Key)).Sum(k => k.Value);
        }
    }
}