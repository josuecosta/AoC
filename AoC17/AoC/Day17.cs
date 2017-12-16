namespace AoC17
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal class Day16 : Master
    {

        public void Run()
        {
            var original = "abcdefghijklmnop";
            var line = new StringBuilder(original);
            var dance = this.Input.First().Split(',');

            // Part 1
            line = MakeDance(line, dance);

            this.Output1Str = line.ToString();

            // Part 2
            var repeatAfter = 1;
            while (line.ToString() != original)
            {
                repeatAfter++;
                line = MakeDance(line, dance);
            }

            var repeatXTimes = 1000000000 % repeatAfter;

            for (long i = 0; i < repeatXTimes; i++)
            {
                line = MakeDance(line, dance);
            }

            this.Output2Str = line.ToString();
        }

        private StringBuilder MakeDance(StringBuilder line, string[] dance)
        {
            foreach (var move in dance)
            {
                var moves = move[0]; // s, x, p 
                line = this.MakeMove(moves, move, line);
            }
            return line;
        }

        private StringBuilder MakeMove(char move, string pair, StringBuilder line)
        {
            switch (move)
            {
                case 's':
                    var number = int.Parse(pair.Substring(1));
                    line = this.Spin(line, number);
                    break;
                case 'x':
                    var pairNumber = pair.Substring(1).Split('/').Select(p => int.Parse(p)).ToList();
                    line = this.Exchange(line, pairNumber);
                    break;
                case 'p':
                    var pairLetter = pair.Substring(1).Split('/').ToList();
                    line = this.Partner(line, pairLetter);
                    break;
            }
            return line;
        }

        private StringBuilder Partner(StringBuilder line, List<string> pairLetter)
        {
            // Makes the programs named A and B swap places.
            var a = line.ToString().IndexOf(pairLetter[0].First());
            var b = line.ToString().IndexOf(pairLetter[1].First());

            var oldA = line[a];
            line[a] = line[b];
            line[b] = oldA;

            return line;
        }

        private StringBuilder Exchange(StringBuilder line, List<int> pairs)
        {
            // Makes the programs at positions A and B swap places.
            var a = pairs[0];
            var b = pairs[1];

            var oldA = line[a];
            line[a] = line[b];
            line[b] = oldA;

            return line;
        }

        private StringBuilder Spin(StringBuilder line, int number)
        {
            // Makes X programs move from the end to the front, but maintain their order otherwise.
            var temp = line.ToString();
            for (int i = 0; i < line.Length; i++)
            {
                line[(i + number) % line.Length] = temp[i];
            }
            return line;
        }
    }
}