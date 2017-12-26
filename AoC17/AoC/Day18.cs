namespace AoC17
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal class Day18 : Master
    {
        private IDictionary<string, long> dic = new Dictionary<string, long>();
        private IDictionary<string, long> dicA = new Dictionary<string, long> { { "p", 0 } };
        private IDictionary<string, long> dicB = new Dictionary<string, long> { { "p", 1 } };

        private List<long> queueA = new List<long>();
        private List<long> queueB = new List<long>();

        private bool lockA = false;
        private bool lockB = false;

        private long counter = 0;

        private long lastSoundPlayed = 0;

        private int currentInstruction = 0;
        private int currentInstructionA = 0;
        private int currentInstructionB = 0;

        public void Run()
        {
            var instructions = this.Input;

            // Part 1
            {
                while (currentInstruction < instructions.Count)
                {
                    var parameters = instructions[currentInstruction].Split(' ');
                    if (!dic.ContainsKey(parameters[1]))
                    {
                        dic.Add(parameters[1], 0);
                    }
                    this.ActionPart1(parameters[0], parameters[1], parameters.Count() > 2 ? parameters[2] : null);

                    if (parameters[0] == "rcv" && lastSoundPlayed != 0 && this.Output1Str == null)
                    {
                        this.Output1Str = lastSoundPlayed.ToString();
                        break;
                    }
                }
            }

            // Part 2
            {
                var deadlock = false;
                var runA = true;

                while (!deadlock)
                {
                    if (runA)
                    {
                        var parameters = instructions[currentInstructionA].Split(' ');
                        if (!dicA.ContainsKey(parameters[1]))
                        {
                            dicA.Add(parameters[1], 0);
                        }
                        this.ActionPart2(0, parameters[0], parameters[1], parameters.Count() > 2 ? parameters[2] : null);
                    }
                    else
                    {
                        var parameters = instructions[currentInstructionB].Split(' ');
                        if (!dicB.ContainsKey(parameters[1]))
                        {
                            dicB.Add(parameters[1], 0);
                        }
                        this.ActionPart2(1, parameters[0], parameters[1], parameters.Count() > 2 ? parameters[2] : null);
                    }

                    runA = !runA;

                    deadlock = lockA && lockB;
                }

                this.Output2Str = counter.ToString();
            }
        }

        #region Part1

        private void ActionPart1(string instruction, string value, string _number = null)
        {
            long number = 0;
            if (_number != null && !long.TryParse(_number, out number))
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

                case "jgz":
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
            if (value > 0)
            {
                currentInstruction += number;
            }
            else
            {
                currentInstruction++;
            }
        }

        private void ModValue(string value, long number)
        {
            dic[value] = dic[value] % number;
        }

        private void MulValue(string value, long number)
        {
            dic[value] *= number;
        }

        private void AddValue(string value, long number)
        {
            dic[value] += number;
        }

        private void SetsValue(string value, long number)
        {
            dic[value] = number;
        }

        private void PlaysSound(string value)
        {
            lastSoundPlayed = dic[value];
            //sounds.Add(new Tuple<string, long>(value, this.dic[value]));
        }

        #endregion Part1

        #region Part2

        private void ActionPart2(int program, string instruction, string value, string _number = null)
        {
            long number = 0;
            if (_number != null && !long.TryParse(_number, out number))
            {
                if (program == 0)
                {
                    number = dicA[_number];
                }
                else
                {
                    number = dicB[_number];
                }
            }

            switch (instruction)
            {
                case "snd":
                    this.SndQueue(program, value);
                    break;

                case "rcv":
                    this.RcvQueue(program, value);
                    break;

                case "set":
                    this.SetsValue(program, value, number);
                    break;

                case "add":
                    this.AddValue(program, value, number);
                    break;

                case "mul":
                    this.MulValue(program, value, number);
                    break;

                case "mod":
                    this.ModValue(program, value, number);
                    break;

                case "jgz":
                    long x = 0;
                    if (!long.TryParse(value, out x))
                    {
                        if (program == 0)
                        {
                            x = dicA[value];
                        }
                        else
                        {
                            x = dicB[value];
                        }
                    }
                    this.Jump(program, x, Int32.Parse(number.ToString()));
                    break;
            }
        }

        private void Jump(int program, long value, int number)
        {
            if (program == 0)
            {
                if (value > 0)
                {
                    currentInstructionA += number;
                }
                else
                {
                    currentInstructionA++;
                }
            }
            else
            {
                if (value > 0)
                {
                    currentInstructionB += number;
                }
                else
                {
                    currentInstructionB++;
                }
            }
        }

        private void ModValue(int program, string value, long number)
        {
            if (program == 0)
            {
                dicA[value] = dicA[value] % number;
                currentInstructionA++;
            }
            else
            {
                dicB[value] = dicB[value] % number;
                currentInstructionB++;
            }
        }

        private void MulValue(int program, string value, long number)
        {
            if (program == 0)
            {
                dicA[value] *= number;
                currentInstructionA++;
            }
            else
            {
                dicB[value] *= number;
                currentInstructionB++;
            }
        }

        private void AddValue(int program, string value, long number)
        {
            if (program == 0)
            {
                dicA[value] += number;
                currentInstructionA++;
            }
            else
            {
                dicB[value] += number;
                currentInstructionB++;
            }
        }

        private void SetsValue(int program, string value, long number)
        {
            if (program == 0)
            {
                dicA[value] = number;
                currentInstructionA++;
            }
            else
            {
                dicB[value] = number;
                currentInstructionB++;
            }
        }

        private void RcvQueue(int program, string value)
        {
            if (program == 0)
            {
                if (queueA.Any())
                {
                    this.SetsValue(program, value, queueA.First());
                    queueA.RemoveAt(0);
                    lockA = false;
                }
                else
                {
                    lockA = true;
                }
            }
            else
            {
                if (queueB.Any())
                {
                    this.SetsValue(program, value, queueB.First());
                    queueB.RemoveAt(0);
                    lockB = false;
                }
                else
                {
                    lockB = true;
                }
            }
        }

        private void SndQueue(int program, string value)
        {
            if (program == 0)
            {
                queueB.Add(dicA[value]);
                currentInstructionA++;
            }
            else
            {
                queueA.Add(dicB[value]);
                currentInstructionB++;
                counter++;
            }
        }

        #endregion Part2
    }
}