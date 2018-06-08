namespace AoC17
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal class Day23 : Master
    {
        public void Run()
        {
            // Part 1 --> (x-2)^2 ['x' is the first input value]
            this.Output1 = (int)Math.Pow((double.Parse(this.Input[0].Split(' ').Last()) - 2), 2);

            int a = 1, b = 57, c = 0, d = 0, e = 0, f = 0, g = 0, h = 0;
            c = b;
            if (a != 0)
            {
                b = b * 100 + 100000;
                c = b + 17000;
            }
            do
            {
                f = 1;
                d = 2;
                e = 2;
                for (d = 2; d * d <= b; d++)
                {
                    if ((b % d == 0))
                    {
                        f = 0;
                        break;
                    }
                }
                if (f == 0)
                    h++;
                g = b - c;
                b += 17;
            } while (g != 0);

            this.Output2 = h;
        }
    }
}