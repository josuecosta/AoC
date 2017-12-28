namespace AoC17
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Day19 : Master
    {
        private StringBuilder letters = new StringBuilder();
        private long counter = 0;

        public void Run()
        {
            this.Walk('v', this.Input);

            // Part 1
            this.Output1Str = letters.ToString();

            this.Output2Str = counter.ToString();
        }

        private void Walk(char direction, List<string> input)
        {
            for (int row = 0; row < input.Count;)
            {
                for (int col = 13; col < input.First().Length;)
                {
                    var digit = input[row][col];
                    if (digit == ' ')
                    {
                        return;
                    }

                    counter++; // Part 2
                    if (digit == '+')
                    {
                        (row, col, direction) = this.ChangeDirection(row, col, direction, input); continue;
                    }
                    else if (digit != '|' && digit != '-')
                    {
                        letters.Append(digit);
                    }

                    (row, col) = this.KeepGoing(row, col, direction, input);
                }
            }
        }

        private (int, int, char) ChangeDirection(int row, int col, char direction, List<string> input)
        {
            if (direction == 'v' || direction == '^')
            {
                if (col == input.First().Length - 1 || input[row][col + 1] == ' ')
                {
                    col--;
                    direction = '<';
                }
                else
                {
                    col++;
                    direction = '>';
                }
            }
            else // if (direction == '<' && direction == '>')
            {
                if (row == input.Count || input[row + 1][col] == ' ')
                {
                    row--;
                    direction = '^';
                }
                else
                {
                    row++;
                    direction = 'v';
                }
            }

            return (row, col, direction);
        }

        private (int, int) KeepGoing(int row, int col, char direction, List<string> input)
        {
            if (direction == 'v')
                row++;
            else if (direction == '<')
                col--;
            else if (direction == '^')
                row--;
            else if (direction == '>')
                col++;

            return (row, col);
        }
    }
}