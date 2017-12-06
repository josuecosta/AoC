namespace AoC17
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class Master
    {
        public List<string> Input { get; set; }
        public int Day { get; set; }
        public int Output1 { get; set; }
        public int Output2 { get; set; }

        public void ReadInput()
        {
            var path = "Inputs/" + this.Day + ".txt";
            this.Input = File.ReadAllLines(path).ToList();
        }

        public void WriteOutput()
        {
            Console.WriteLine("Results Day #" + this.Day);
            Console.WriteLine("Part 1: " + this.Output1);
            Console.WriteLine("Part 2: " + this.Output2);
            Console.Read();
        }
    }
}