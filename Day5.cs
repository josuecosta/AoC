namespace AoC17
{
    using System.Linq;

    internal class Day5 : Master
    {
        public void Run()
        {
            // Part 1
            var position = 0;
            var steps = 0;
            var offSets = this.Input.Select(o => int.Parse(o)).ToArray();
            while (position < offSets.Length)
            {
                var temp = offSets[position];
                offSets[position]++;
                position = position + temp;
                steps++;
            }
            this.Output1 = steps;

            // Part 2
            position = 0;
            steps = 0;
            offSets = this.Input.Select(o => int.Parse(o)).ToArray();
            while (position < offSets.Length)
            {
                var temp = offSets[position];
                offSets[position] = temp >= 3 ? offSets[position] - 1 : offSets[position] + 1;
                position = position + temp;
                steps++;
            }
            this.Output2 = steps;
        }
    }
}