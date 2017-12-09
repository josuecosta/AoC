namespace AoC17
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Day9 : Master
    {
        public void Run()
        {
            // Part 1
            this.Output1 = this.GetGroupsScore(this.Input.First(), 1);
        }

        private int GetGroupsScore(string input, int level)
        {
            var score = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '{')
                {
                    score += level;
                    level++;
                }
                else if (input[i] == '}')
                {
                    level--;
                }
                else if (input[i] == '<')
                {
                    var garbage = this.GetGarbageEnd(input.Substring(i));

                    // Part 2
                    this.Output2 += this.GetGarbageLength(input.Substring(i, garbage));

                    i += garbage;
                }
            }
            return score;
        }

        private int GetGarbageLength(string garbage)
        {
            var length = 0;
            for (int i = 1; i < garbage.Length; i++)
            {
                if (garbage[i] == '!')
                {
                    i++;
                    continue;
                }
                length++;
            }
            return length;
        }

        private int GetGarbageEnd(string garbage)
        {
            for (int i = 0; i < garbage.Length; i++)
            {
                if (garbage[i] == '>')
                {
                    return i;
                }

                if (garbage[i] == '!')
                {
                    i++;
                    continue;
                }
            }
            return garbage.Length;
        }
    }
}