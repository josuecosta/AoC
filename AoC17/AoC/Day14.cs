namespace AoC17
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal class Day14 : Master
    {
        private IDictionary<int, int> dic = new Dictionary<int, int>();

        public void Run()
        {
            var input = this.GetHashInputs(this.Input.First());
            //var input = this.GetHashInputs("flqrgnkx");

            var knotHashes = new Day10();
            var grid = new List<string>();
            foreach (var hash in input)
            {
                grid.Add(knotHashes.GetKnotHashBinary(hash));
            }

            // Part 1
            this.Output1 = grid.Sum(d => d.Count(c => c == '1'));

            // Part 2
            this.Output2 = this.GetNumberOfRegions(grid.Clone());
        }

        private int GetNumberOfRegions(List<string> grid)
        {
            var regions = 0;
            //grid = grid.Select(r => r.Replace('0', '.')).Select(r => r.Replace('1', '0')).ToList();

            for (int row = 0; row < 128; row++)
            {
                var startPosition = 0;
                for (int col = 0; col < grid.Count; col++, startPosition++)
                {
                    if (grid[row][col] == '1')
                    {
                        regions++;
                        while (this.HasAdjacent(row, col, grid, out var rowAdj, out var colAdj))
                        {
                            var strB = new StringBuilder(grid[row]);
                            strB[col] = 'X';
                            grid[row] = strB.ToString();

                            row = rowAdj;
                            col = colAdj;
                        }
                        col = startPosition;
                    }
                }
            }

            return regions;
        }

        private bool HasAdjacent(int row, int col, List<string> grid, out int rowAdj, out int colAdj)
        {
            if (row == 0 && col == 0)
            {
                if (grid[row][col + 1] == '1')
                {
                    rowAdj = row; colAdj = col + 1; return true;
                }
                if (grid[row + 1][col] == '1')
                {
                    rowAdj = row + 1; colAdj = col; return true;
                }
            }

            if (row == 127 && col == 127)
            {
                if (grid[row][col - 1] == '1')
                {
                    rowAdj = row; colAdj = col - 1; return true;
                }
                if (grid[row - 1][col] == '1')
                {
                    rowAdj = row - 1; colAdj = col; return true;
                }
            }

            rowAdj = row; colAdj = col;
            return false;
        }

        private List<string> GetHashInputs(string input)
        {
            var list = new List<string>();
            for (int i = 0; i < 128; i++)
            {
                list.Add(input + "-" + i);
            }
            return list;
        }
    }
}