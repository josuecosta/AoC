namespace AoC17
{
    using System.Linq;

    internal class Day4 : Master
    {
        public void Run()
        {
            // Part 1
            var words = 0;
            foreach (var row in this.Input)
            {
                var allWords = row.Split(' ');
                if (allWords.Count() == allWords.Distinct().Count())
                {
                    words++;
                }
            }
            this.Output1 = words;

            // Part 2
            words = 0;
            foreach (var row in this.Input)
            {
                var allWords = row.Split(' ');

                for (int i = 0; i < allWords.Length; i++)
                {
                    allWords[i] = string.Concat(allWords[i].OrderBy(c => c));
                }

                if (allWords.Count() == allWords.Distinct().Count())
                {
                    words++;
                }
            }
            this.Output2 = words;
        }
    }
}