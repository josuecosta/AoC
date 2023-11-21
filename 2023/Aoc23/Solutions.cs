using Aoc23.BL;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Aoc23
{
    public class Solutions
    {
        public static string Input;
        public string Solution;

        #region DAY 1

        public decimal Day1Solve()
        {
            var data = File.ReadAllLines(Input);

            for (int i = 0; i < data.Length; i++)
            {

            }

            return 1;
        }

        #endregion DAY 1

        public Solutions(bool isTest = false)
        {
            Input = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), isTest ? "input-test.txt" : "input.txt");
            Solution = Day1Solve().ToString();
        }
    }
}