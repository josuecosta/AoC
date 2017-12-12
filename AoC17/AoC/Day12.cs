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
            foreach (var row in this.Input)
            {
                var key_Program = row.Split(new[] { "<->" }, StringSplitOptions.None).ToList();
                dic.Add(int.Parse(key_Program[0]), key_Program[1].Split(',').Select(e => int.Parse(e)).ToList());
            }

			// Part 1
            this.Output1 = GetNumberOfPrograms(dic[0]);

            // Part 2
            this.Output2 = GetTotalNumberOfGroups();

        }

        private int GetTotalNumberOfGroups()
        {
            var groups = new List<List<int>>();

            foreach (var program in dic.Keys)
            {
                // Check if the program already belogs to a group
                if (!groups.Any(g => g.Contains(program)))
                {
                    // This method will also fill the global programs list
                    this.GetNumberOfPrograms(dic[program]);
                    groups.Add(programs.Clone());
                    programs.Clear();
                }
            }

            return groups.Count();
        }

        private int GetNumberOfPrograms(List<int> keys)
        {
            var news = new List<int>();

            foreach (var key in keys)
            {
                if (!programs.Contains(key))
                {
                    programs.Add(key);
                    news.AddRange(dic[key]);
                }
            }

            // Recursive: Stops only when there are no more new programs to add to the group.
            return news.Count() == 0 ? programs.Count() : GetNumberOfPrograms(news.Distinct().ToList());
        }
    }
}