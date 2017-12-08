namespace AoC17
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Day8 : Master
    {
        public IDictionary<string, int> Registers { get; set; }

        public void Run()
        {
            // Part 1
            this.Registers = new Dictionary<string, int>();
            this.Input.Select(i => i.Split(' ').First()).Distinct().ToList().ForEach(r => Registers.Add(r, 0));

            var max = 0;

            foreach (var row in this.Input)
            {
                var parameters = row.Split(' ').ToList();
                var register = parameters[0];

                Registers[register] = this.GetValue(parameters[1] == "inc" ? true : false, int.Parse(parameters[2]), parameters[4], parameters[5], int.Parse(parameters[6]), Registers[register]);

                // Part 2
                if (Math.Abs(Registers[register]) > max)
                {
                    max = Math.Abs(Registers[register]);
                }
            }

            this.Output1 = Registers.Values.Max();
            this.Output2 = max;
        }

        private int GetValue(bool inc, int value, string conditionRegister, string op, int conditionValue, int actualValue)
        {
            switch (op)
            {
                case "<":
                    return Registers[conditionRegister] < conditionValue ? inc ? actualValue + value : actualValue - value : actualValue;
                case ">":
                    return Registers[conditionRegister] > conditionValue ? inc ? actualValue + value : actualValue - value : actualValue;
                case "<=":
                    return Registers[conditionRegister] <= conditionValue ? inc ? actualValue + value : actualValue - value : actualValue;
                case ">=":
                    return Registers[conditionRegister] >= conditionValue ? inc ? actualValue + value : actualValue - value : actualValue;
                case "==":
                    return Registers[conditionRegister] == conditionValue ? inc ? actualValue + value : actualValue - value : actualValue;
                case "!=":
                    return Registers[conditionRegister] != conditionValue ? inc ? actualValue + value : actualValue - value : actualValue;
                default:
                    return actualValue;
            }
        }
    }
}