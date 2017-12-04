namespace AoC17
{
    using System.Linq;

    public class Day1 : Master
    {
        public void Run()
        {
            var chair = base.Input.First();

            var steps = 1; // Part 1

            for (int i = 0; i < chair.Length; i++)
            {
                var number = int.Parse(chair[i].ToString());
                var pos = (steps + i) % chair.Length;
                if (chair[i] == chair[pos])
                {
                    this.Output1 += number;
                }
            }

            steps = chair.Length / 2; // Part 2

            for (int i = 0; i < chair.Length; i++)
            {
                var number = int.Parse(chair[i].ToString());
                var pos = (steps + i) % chair.Length;
                if (chair[i] == chair[pos])
                {
                    this.Output2 += number;
                }
            }
        }
    }
}
