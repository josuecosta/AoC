namespace AoC17
{
    using System.Linq;
    using System;
    using System.Collections.Generic;
    using System.Text;

    internal class Day10 : Master
    {
        public void Run()
        {
            // Part 1
            var numbersList = this.GetNumbersList(256);
            var position = 0;
            var skipSize = 0;
            var lengthsList = this.Input.First().Split(',').Select(e => int.Parse(e)).ToList();

            foreach (var length in lengthsList)
            {
                this.ReverseList(ref numbersList, length, position);
                position = (position + length + skipSize) % numbersList.Count;
                skipSize++;
            }

            this.Output1 = numbersList[0] * numbersList[1];

            // Part 2
            this.Output2Str = this.GetKnotHash(this.Input.First());
        }

        private string GetDenseHash(List<int> numbersList)
        {
            var denseHash = string.Empty;
            for (int i = 0; i < 256; i += 16)
            {
                var block = numbersList.Skip(i).Take(16).ToList();
                var xorResult = block[0];
                for (int j = 1; j < 16; j++)
                {
                    xorResult = xorResult ^ block[j];
                }
                denseHash += xorResult.ToString("X2");
            }
            return denseHash;
        }

        private void ReverseList(ref List<int> numbersList, int length, int position)
        {
            var listToReverse = this.GetRange(numbersList, position, length);
            listToReverse.Reverse();
            for (int i = 0; i < length; i++)
            {
                var index = (position + i) % numbersList.Count;
                numbersList[index] = listToReverse[i];
            }
        }

        private List<int> GetRange(List<int> numbersList, int position, int length)
        {
            var list = new List<int>();
            for (int i = 0; i < length; i++)
            {
                var index = (position + i) % numbersList.Count;
                list.Add(numbersList[index]);
            }
            return list;
        }

        private List<int> GetNumbersList(int limit)
        {
            var list = new List<int>();
            for (int i = 0; i < limit; i++)
            {
                list.Add(i);
            }
            return list;
        }

        public string GetKnotHash(string input)
        {
            var numbersList = this.GetNumbersList(256);
            var position = 0;
            var skipSize = 0;

            var lengthsList = Encoding.ASCII.GetBytes(input).Select(b => b.ToString()).ToList();
            lengthsList.AddRange("17,31,73,47,23".Split(',').ToList());

            for (int c = 0; c < 64; c++)
            {
                foreach (var length in lengthsList)
                {
                    this.ReverseList(ref numbersList, int.Parse(length), position);
                    position = (position + int.Parse(length) + skipSize) % numbersList.Count;
                    skipSize++;
                }
            }

            return this.GetDenseHash(numbersList);
        }

        public string GetKnotHashBinary(string hash)
        {
            return String.Join(String.Empty, this.GetKnotHash(hash).Select(
                c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
        }
    }
}