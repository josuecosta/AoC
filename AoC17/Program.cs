namespace AoC17
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            var AoC = new Day14();
            AoC.Day = int.Parse(AoC.GetType().Name.Substring(3));
            AoC.ReadInput();
            AoC.Run();
            AoC.WriteOutput();
        }
    }
}