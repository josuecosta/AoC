using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc23
{
    internal class Scratchcards
    {
        private List<Cards> _cards;

        public Scratchcards(string[] data)
        {
            this._cards = InitCards(data);
        }

        private List<Cards> InitCards(string[] data)
        {
            var cards = new List<Cards>();
            for (int i = 0; i < data.Length; i++)
            {
                var card = new Cards();
                var numbersArr = data[i].Substring(data[i].IndexOf(":")+1).Split('|');
                card.WinningNumbers = numbersArr[0].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                                                  .Select(n => int.Parse(n.Trim())).ToHashSet();
                card.Numbers = numbersArr[1].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                                           .Select(n => int.Parse(n.Trim())).ToHashSet();
                cards.Add(card);
            }
            return cards;
        }

        internal double GetPoints() => _cards.Sum(p => CalculateClassification(p.MatchingNumbers));

        internal double CalculateClassification(int points) => points > 0 ? Math.Pow(2, points-1) : 0;

        internal decimal GetTotalScratchcards()
        {
            var queue = new Queue<Cards>();

            _cards.ForEach(c => queue.Enqueue(c));

            var count = 0;
            while (queue.Count > 0)
            {
                count++;
                var card = queue.Dequeue();
                var copies = _cards.Skip(card.Id).Take(card.MatchingNumbers).ToList();
                copies.ForEach(c => queue.Enqueue(c));
            }

            return count;
        }
    }

    internal class Cards
    {
        private static int _id = 1;
        public Cards()
        {
            this.Id = _id++;
        }
        public int Id { get; set; }
        public HashSet<int> WinningNumbers { get; set; }
        public HashSet<int> Numbers { get; set; }
        public int MatchingNumbers => WinningNumbers.Count(wn => Numbers.Contains(wn));
    }
}