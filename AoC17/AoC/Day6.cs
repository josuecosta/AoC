namespace AoC17
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Day6 : Master
    {
        public void Run()
        {
            foreach (var row in this.Input)
            {
                var cycles = 0;
                var memory = row.Split("\t", StringSplitOptions.RemoveEmptyEntries).Select(n => int.Parse(n)).ToList();
                var combinations = new List<string> { string.Join("", memory) };
                var firstCombinationThatRepeats = string.Empty;

                while (true)
                {
                    var index = memory.FindIndex(n => n == memory.Max());
                    var blocks = memory[index] / (memory.Count - 1);
                    var remainder = memory[index] % (memory.Count - 1);

                    this.UpdateMemory(memory, index, blocks > 0 ? blocks : 1, remainder, memory[index]);

                    cycles++;

                    var memoryStr = string.Join("", memory);
                    if (combinations.Contains(memoryStr))
                    {
                        if (memoryStr == firstCombinationThatRepeats)
                        {
                            // Part 2
                            break;
                        }

                        if (string.IsNullOrEmpty(firstCombinationThatRepeats))
                        {
                            firstCombinationThatRepeats = memoryStr;
                            this.Output1 = cycles; // Part 1
                            cycles = 0;
                        }
                    }

                    combinations.Add(memoryStr);
                }

                this.Output2 = cycles;
            }
        }

        private void UpdateMemory(List<int> memory, int index, int blocks, int remainder, int totalBlocks)
        {
            for (int i = 0; i < memory.Count && i < totalBlocks; i++)
            {
                memory[index] = 0;
                var position = (index + 1 + i) % memory.Count;
                memory[position] = index == position ? memory[position] += remainder : memory[position] += blocks;
            }
        }
    }
}