namespace AoC17
{
    using System.Linq;

    internal class Day2 : Master
    {
        public void Run()
        {
            // Part 1
            foreach (var row in this.Input)
            {
                var allNumbers = row.Split(" ", System.StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x));

                var min = allNumbers.Min();
                var max = allNumbers.Max();

                this.Output1 += (max - min);
            }

            // Part 2
            foreach (var row in this.Input)
            {
                var allNumbers = row.Split(" ", System.StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();

                var result = 1;
                foreach (var number in allNumbers)
                {
                    for (int i = 0; i < allNumbers.Count(); i++)
                    {
                        if (number != allNumbers[i] && number % allNumbers[i] == 0)
                        {
                            result = number / allNumbers[i];
                            break;
                        }
                    }
                }

                this.Output2 += result;
            }
        }
    }
}