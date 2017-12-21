namespace AoC17
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal class Day18 : Master
    {
        private IDictionary<string, int> dic = new Dictionary<string, int>();
        private IDictionary<string, int> sounds = new Dictionary<string, int>();
        private int lastSoundPlayed = 0;
        private int currentInstruction = 0;

        public void Run()
        {
            var instructions = this.Input;

            while (currentInstruction < instructions.Count)
            {
                var parameters = instructions[currentInstruction].Split(' ');
                if (!dic.ContainsKey(parameters[1]))
                {
                    dic.Add(parameters[1], 0);
                }
                this.Action(parameters[0], parameters[1], parameters.Count() > 2 ? parameters[2] : null);
                if (parameters[0] == "rcv" && lastSoundPlayed > 0) break; // Part 1
            }

            this.Output1 = lastSoundPlayed;
            this.Output2 = 1;
        }

        private void Action(string instruction, string value, string _number = null)
        {
            int number = 0;
            if (_number != null && !int.TryParse(_number, out number))
            {
                number = dic[_number];
            }

            switch (instruction)
            {
                case "snd":
                    this.PlaysSound(value);
                    currentInstruction++; break;

                case "set":
                    this.SetsValue(value, number);
                    currentInstruction++; break;

                case "add":
                    this.AddValue(value, number);
                    currentInstruction++; break;

                case "mul":
                    this.MulValue(value, number);
                    currentInstruction++; break;

                case "mod":
                    this.ModValue(value, number);
                    currentInstruction++; break;

                case "rcv":
                    if (dic[value] != 0)
                    {
                        this.lastSoundPlayed = this.RecoversFrequency(value);
                    }
                    currentInstruction++; break;

                case "jgz":
                    var x = 0;
                    if (!int.TryParse(value, out x))
                    {
                        x = dic[value];
                    }
                    this.Jump(x, number);
                    break;

                default:
                    currentInstruction++; break;
            }
        }

        private void Jump(int value, int number)
        {
            if (value > 0)
            {
                currentInstruction += number;
            }
            else
            {
                currentInstruction++;
            }
        }

        private int RecoversFrequency(string value)
        {
            if (sounds.ContainsKey(value))
            {
                return sounds[value];
            }
            return 0;
        }

        private void ModValue(string value, int number)
        {
            dic[value] = dic[value] % number;
        }

        private void MulValue(string value, int number)
        {
            dic[value] *= number;
        }

        private void AddValue(string value, int number)
        {
            dic[value] += number;
        }

        private void SetsValue(string value, int number)
        {
            dic[value] = number;
        }

        private void PlaysSound(string value)
        {
            if (!sounds.ContainsKey(value))
            {
                sounds.Add(value, this.dic[value]);
            }
            else
            {
                sounds[value] = this.dic[value];
            }
        }
    }
}