namespace AoC17
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal class Day17 : Master
    {
        public void Run()
        {
            //var steps = int.Parse(this.Input.First());
            var steps = 3;

            var buffer = new List<int> {0};
            var position = 0;
            var counter = 0;

            for (int i = 0; i < 2017; i++)
            {
                position = (steps + position) % buffer.Count;

                if (++position == buffer.Count)
                {
                    buffer.Add(++counter);
                    continue;
                }
                buffer.Insert(position, ++counter);
            }

            this.Output1 = buffer[((position+1)%buffer.Count)];

            //buffer = new List<int> { 0 };
            //position = 0;
            //counter = 0;
            //for (int i = 0; i < 1000; i++)
            //{
            //    position = (steps + position) % buffer.Count;

            //    if (++position == buffer.Count)
            //    {
            //        buffer.Add(++counter);
            //        continue;
            //    }
            //    buffer.Insert(position, ++counter);
            //    Console.WriteLine(string.Join(" ", buffer));
            //}

            this.Output2 = buffer[1];
        }
    }
}