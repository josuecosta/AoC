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
        public string Output1Str { get; set; }
        public string Output2Str { get; set; }

        public void ReadInput()
        {
            var path = "Inputs/" + this.Day + ".txt";
            this.Input = File.ReadAllLines(path).ToList();
        }

        public void WriteOutput()
        {
            Console.WriteLine("Results Day #" + this.Day);
            Console.WriteLine(string.Format("Part 1: {0}", this.Output1Str != null ? this.Output1Str : this.Output1.ToString()));
            Console.WriteLine(string.Format("Part 2: {0}", this.Output2Str != null ? this.Output2Str : this.Output2.ToString()));
            //Console.Read();
        }
    }
}