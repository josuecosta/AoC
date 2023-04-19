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
            var towers = new List<Tower>();
            foreach (var row in this.Input)
            {
                var parameters = row.Split(" ", 4, StringSplitOptions.RemoveEmptyEntries);
                var towerStr = parameters.First();
                var number = parameters.Skip(1).First();
                var otherTowers = parameters.Skip(3).FirstOrDefault();
                var subTowers = new List<Tower>();

                if (otherTowers != null)
                {
                    var towersToAdd = otherTowers.Split(',');

                    foreach (var subTower in towersToAdd)
                    {
                        if (!towers.Any(t => t.Name == subTower.Trim()))
                        {
                            towers.Add(new Tower { Name = subTower.Trim() });
                        }

                        subTowers.Add(towers.First(t => t.Name == subTower.Trim()));
					}
                }

                var weight = int.Parse(number.Substring(1, number.Length - 2));

                if (!towers.Any(t => t.Name == towerStr))
                {
                    towers.Add(new Tower { Name = towerStr, SubTowers = subTowers, Weight = weight });
                    continue;
                }

                var tower = towers.First(t => t.Name == towerStr);
                tower.SubTowers = subTowers;
                tower.Weight = weight;
            }

            var allSubTowers = towers.SelectMany(t => t.SubTowers).Select(x => x.Name).Distinct();
            var allTowers = towers.Select(t => t.Name).Distinct();
            var root = towers.First(t => t.Name == allTowers.Except(allSubTowers).First());
            this.Output1Str = root.Name;

            // Part 2
            var unbalancedBranch = this.GetUnbalancedBranch(root);

            var weights = root.SubTowers.Select(t => t.TowerWeight).Distinct().ToList();

            this.Output2 = unbalancedBranch.Weight - Math.Abs(weights[0] - weights[1]);
        }

        private Tower GetUnbalancedBranch(Tower root)
        {
            if (this.IsBalancedTower(root))
            {
                return root;
            }

            var nextRoot = root.SubTowers.GroupBy(t => t.TowerWeight)
                               .OrderBy(t => t.Count())
                               .FirstOrDefault()
                               .Select(t => t).Distinct().First();

            return this.GetUnbalancedBranch(nextRoot);
        }

        private bool IsBalancedTower(Tower branch)
        {
            for (int i = 0; i < branch.SubTowers.Count-1; i++)
            {
                if (branch.SubTowers[i].TowerWeight != branch.SubTowers[i+1].TowerWeight)
                {
                    return false;
                }
            }
            return true;
        }
    }

    internal class Tower
    {
        public Tower()
        {
            this.SubTowers = new List<Tower>();
        }

        public string Name { get; set; }

        public List<Tower> SubTowers { get; set; }

        public int Weight { get; set; }

        public int TowerWeight { get => this.CalcWeight(); }

        private int CalcWeight()
        {
            if (this.SubTowers == null)
            {
                return Weight;
            }

            var sumWeight = this.Weight;

            foreach (var tower in SubTowers)
            {
                sumWeight += tower.CalcWeight();
            }

            return sumWeight;
        }
    }
}