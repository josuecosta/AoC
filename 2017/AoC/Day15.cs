namespace AoC17
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal class Day15 : Master
    {
        public void Run()
        {
            var generatorA = int.Parse(this.Input[0].Split(' ').Last());
            var generatorB = int.Parse(this.Input[1].Split(' ').Last());

            long factorA = 16807;
            long factorB = 48271;
            long remainderFactor = 2147483647;
            long valueA = generatorA;
            long valueB = generatorB;

            // Part 1
            var count = 0;
            for (int i = 0; i < 40000000; i++)
            {
                valueA = (valueA * factorA) % remainderFactor;
                valueB = (valueB * factorB) % remainderFactor;

                var binaryValueA = Convert.ToString(valueA, 2).PadLeft(16, '0');
                var binaryValueB = Convert.ToString(valueB, 2).PadLeft(16, '0');

                var generatedValueA = binaryValueA.Substring(binaryValueA.Length - 16);
                var generatedValueB = binaryValueB.Substring(binaryValueB.Length - 16);

                if (generatedValueA == generatedValueB)
                {
                    count++;
                }
            }

            this.Output1 = count;

            // Part 2
            count = 0;
            var multipleA = 4;
            var multipleB = 8;
            for (int i = 0; i < 5000000; i++)
            {
                var generatedValueA = this.GenerateValidValue(factorA, remainderFactor, ref valueA, multipleA);
                var generatedValueB = this.GenerateValidValue(factorB, remainderFactor, ref valueB, multipleB);

                if (generatedValueA == generatedValueB)
                {
                    count++;
                }
            }

            this.Output2 = count;
        }

        private string GenerateValidValue(long factor, long remainderFactor, ref long value, int multiple)
        {
            do
            {
                value = (value * factor) % remainderFactor;
            } while (value % multiple != 0);

            var binaryValue = Convert.ToString(value, 2).PadLeft(16, '0');
            var generatedValue = binaryValue.Substring(binaryValue.Length - 16);

            return generatedValue;
        }
    }
}