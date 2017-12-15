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

            for (int row = 0; row < 128; row++)
            {
                for (int col = 0; col < 128; col++)
                {
                    if (grid[row][col] == '1')
                    {
                        this.MarkAdjacent(row, col, grid);
                        regions++;
                    }
                }
            }

            return regions;
        }

        private void MarkAdjacent(int row, int col, List<string> grid)
        {
            var strB = new StringBuilder(grid[row]);
            strB[col] = 'X';
            grid[row] = strB.ToString();

            var adjacents = this.GetAdjacent(row, col, grid);
            foreach (var adjacent in adjacents)
            {
                this.MarkAdjacent(adjacent.Item1, adjacent.Item2, grid);
            }
        }

        private List<Tuple<int, int>> GetAdjacent(int row, int col, List<string> grid)
        {
            var adjacent = new List<Tuple<int, int>>();

            if (row > 0 && row < 127)
            {
                if (grid[row + 1][col] == '1')
                {
                    adjacent.Add(new Tuple<int, int>(row + 1, col));
                }
                if (grid[row - 1][col] == '1')
                {
                    adjacent.Add(new Tuple<int, int>(row - 1, col));
                }

                if (col > 0 && col < 127)
                {
                    if (grid[row][col + 1] == '1')
                    {
                        adjacent.Add(new Tuple<int, int>(row, col + 1));
                    }
                    if (grid[row][col - 1] == '1')
                    {
                        adjacent.Add(new Tuple<int, int>(row, col - 1));
                    }
                }
                else if (col == 0)
                {
                    if (grid[row][col + 1] == '1')
                    {
                        adjacent.Add(new Tuple<int, int>(row, col + 1));
                    }
                }
                else // Col == 127
                {
                    if (grid[row][col - 1] == '1')
                    {
                        adjacent.Add(new Tuple<int, int>(row, col - 1));
                    }
                }
            }
            else if (row == 0)
            {
                if (grid[row + 1][col] == '1')
                {
                    adjacent.Add(new Tuple<int, int>(row + 1, col));
                }
                if (col > 0 && col < 127)
                {
                    if (grid[row][col + 1] == '1')
                    {
                        adjacent.Add(new Tuple<int, int>(row, col + 1));
                    }
                    if (grid[row][col - 1] == '1')
                    {
                        adjacent.Add(new Tuple<int, int>(row, col - 1));
                    }
                }
                else if (col == 0)
                {
                    if (grid[row][col + 1] == '1')
                    {
                        adjacent.Add(new Tuple<int, int>(row, col + 1));
                    }
                }
                else // Col == 127
                {
                    if (grid[row][col - 1] == '1')
                    {
                        adjacent.Add(new Tuple<int, int>(row, col - 1));
                    }
                }
            }
            else // Row == 127
            {
                if (grid[row - 1][col] == '1')
                {
                    adjacent.Add(new Tuple<int, int>(row - 1, col));
                }
                if (col > 0 && col < 127)
                {
                    if (grid[row][col + 1] == '1')
                    {
                        adjacent.Add(new Tuple<int, int>(row, col + 1));
                    }
                    if (grid[row][col - 1] == '1')
                    {
                        adjacent.Add(new Tuple<int, int>(row, col - 1));
                    }
                }
                else if (col == 0)
                {
                    if (grid[row][col + 1] == '1')
                    {
                        adjacent.Add(new Tuple<int, int>(row, col + 1));
                    }
                }
                else // Col == 127
                {
                    if (grid[row][col - 1] == '1')
                    {
                        adjacent.Add(new Tuple<int, int>(row, col - 1));
                    }
                }
            }

            return adjacent;
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