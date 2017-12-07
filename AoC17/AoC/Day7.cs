namespace AoC17
{
    using System.Linq;
    using System;
    using System.Collections.Generic;

    internal class Day7 : Master
    {
        public void Run()
        {
            // Part 1
            var dic = new Dictionary<string, int>();
            foreach (var row in this.Input)
            {
                var parameters = row.Split(" ");
                var key = parameters.First();
                var number = parameters.Skip(1).First();
                var subTowers = parameters.Skip(4).FirstOrDefault();

                if (!dic.ContainsKey(""))
                {
                    // adiciona (str, 0)
                }
            }

            // Part 2
            foreach (var row in this.Input)
            {
            }
        }
    }
}