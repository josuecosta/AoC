namespace AoC17
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Day13 : Master
    {
        private IDictionary<int, int> dic = new Dictionary<int, int>();
        private int picoseconds = -1;

        public void Run()
        {
            foreach (var row in this.Input)
            {
                var range_Depth = row.Split(':').ToList();
                dic.Add(int.Parse(range_Depth[0]), int.Parse(range_Depth[1]));
            }

            // Part 1
            this.Output1 = GetTripSeverity();

            // Part 2
            this.Output2 = GetNumberOfDelays();
        }

        private int GetNumberOfDelays()
        {
            var count = 0;
            picoseconds = -1;
            while (this.IsNotSafeToPassThrough())
            {
                count++;
            }
            return count;
        }

        private bool IsNotSafeToPassThrough()
        {
            for (int i = -1; i <= dic.Keys.Max(); i++)
            {
                if (this.WillBeCaught(i + 1))
                {
                    picoseconds++;
                    return true;
                }
                picoseconds++;
            }
            return false;
        }

        private int GetTripSeverity()
        {
            var caughtHistory = new List<int>();

            for (int i = -1; i < dic.Keys.Max(); i++)
            {
                if (this.WillBeCaught(i + 1))
                {
                    caughtHistory.Add(i + 1);
                }
                picoseconds++;
            }

            // Severity = Sum(Key * Value);
            return caughtHistory.Sum(key => (key * dic[key]));
        }

        private bool WillBeCaught(int position)
        {
            if (!dic.ContainsKey(position))
            {
                return false;
            }

            var securityPosition = this.GetNextSecurityPosition(dic[position]);

            return securityPosition == 0 ? true : false;
        }

        private int GetNextSecurityPosition(int length)
        {
            return (length - 1) - Math.Abs(((picoseconds + 1) % ((length - 1) * 2)) - (length - 1));
        }
    }
}