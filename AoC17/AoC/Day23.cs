namespace AoC17
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal class Day23 : Master
    {
        private IDictionary<string, long> dic = new Dictionary<string, long>();

        private long counter = 0;

        private int currentInstruction = 0;

        public void Run()
        {
            var instructions = this.Input;

            while (currentInstruction < instructions.Count)
            {
                var parameters = instructions[currentInstruction].Split(' ');
                if (!dic.ContainsKey(parameters[1]) && !int.TryParse(parameters[1], out var temp))
                {
                    dic.Add(parameters[1], 0);
                }

                this.Action(parameters[0], parameters[1], parameters.Count() > 2 ? parameters[2] : null);

                if (parameters[0] == "mul")
                {
                    counter++;
                }
            }
            this.Output1Str = counter.ToString();
        }

        private void Action(string instruction, string value, string _number = null)
        {
            long number = 0;
            if (_number != null && !long.TryParse(_number, out number))
            {
                number = dic[_number];
            }

            switch (instruction)
            {
                case "set":
                    this.SetsValue(value, number);
                    currentInstruction++; break;

                case "sub":
                    this.SubValue(value, number);
                    currentInstruction++; break;

                case "mul":
                    this.MulValue(value, number);
                    currentInstruction++; break;

                case "jnz":
                    long x = 0;
                    if (!long.TryParse(value, out x))
                    {
                        x = dic[value];
                    }
                    this.Jump(x, Int32.Parse(number.ToString()));
                    break;

                default:
                    currentInstruction++; break;
            }
        }

        private void Jump(long value, int number)
        {
            if (value != 0)
            {
                currentInstruction += number;
            }
            else
            {
                currentInstruction++;
            }
        }

        private void MulValue(string value, long number)
        {
            dic[value] *= number;
        }

        private void SubValue(string value, long number)
        {
            dic[value] -= number;
        }

        private void SetsValue(string value, long number)
        {
            dic[value] = number;
        }
    }
}