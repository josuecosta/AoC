namespace AoC17
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Day19 : Master
    {
        private StringBuilder letters = new StringBuilder();

        public void Run()
        {
            this.Walk('v', this.Input);

            this.Output1Str = letters.ToString();
            this.Output2 = 0;
        }

        private void Walk(char direction, List<string> input)
        {
            for (int row = 0; row < input.Count;)
            {
                for (int col = 0; col < input.First().Length;)
                {
                    var digit = input[row][col];
                    if (digit == '|')
                    {
                        if (direction == 'v')
                        {
                            row++;
                        }
                        else
                        {
                            row--;
                        }
                    }
                    else if (digit == '-')
                    {
                        if (direction == '>')
                        {
                            col++;
                        }
                        else
                        {
                            col--;
                        }
                    }
                    else if (digit == '+')
                    {
                        (row, col, direction) = this.ChangeDirection(row, col, direction, input);
                    }
                    else
                    {
                        letters.Append(digit);
                        (row, col) = this.KeepGoing(row, col, direction, input);
                    }
                }
            }
        }

        private (int, int, char) ChangeDirection(int row, int col, char prevDirection, List<string> input)
        {
            if (prevDirection == 'v' || prevDirection == '^')
            {
                if (input[row][col + 1] == ' ')
                {
                    col--;
                    prevDirection = '<';
                }
                else
                {
                    col++;
                    prevDirection = '>';
                }
            }
            else if (prevDirection == '<' && prevDirection == '>')
            {
                if (input[row + 1][col] == ' ')
                {
                    row--;
                    prevDirection = '^';
                }
                else
                {
                    row++;
                    prevDirection = 'v';
                }
            }

            return (row, col, prevDirection);
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