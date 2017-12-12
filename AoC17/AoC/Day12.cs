namespace AoC17
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Day12 : Master
    {
        private IDictionary<int, List<int>> dic = new Dictionary<int, List<int>>();
        private List<int> programs = new List<int>();

        public void Run()
        {
            // Part 1
            foreach (var row in this.Input)
            {
                var relations = row.Split(new[] { "<->" }, System.StringSplitOptions.RemoveEmptyEntries).ToList();
                var key = int.Parse(relations[0]);
                var programsThatCommunicate = relations[2].Split(',').Select(e => int.Parse(e)).ToList();
                dic.Add(key, programsThatCommunicate);
                programs.Add(key);

                this.WhoElseCommunicatesWithYou(programsThatCommunicate);
            }

            // Part 2
            foreach (var row in this.Input)
            {
            }
        }

        private void WhoElseCommunicatesWithYou(List<int> programsThatCommunicate)
        {
            foreach (var program in programsThatCommunicate)
            {
                if (!dic.ContainsKey(program))
                {
                }
            }
        }
    }
}