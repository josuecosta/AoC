using System.Data;

namespace AOC24;

internal class Day5
{
    private readonly List<(int X, int Y)> _rules = [];
    private readonly List<List<int>> _updates = [];

    public Day5(string[] data)
    {
        var line = 0;
        while (!string.IsNullOrWhiteSpace(data[line]))
        {
            var rule = data[line].Split("|").Select(int.Parse).ToList();
            _rules.Add((rule[0], rule[1]));
            line++;
        }

        for (int i = line + 1; i < data.Length; i++)
        {
            var numbers = data[i].Split(",").Select(int.Parse).ToList();
            _updates.Add(numbers);
        }
    }

    public decimal SumMiddleNumbers
        => _updates.Where(IsInCorrectOrder)
                   .Sum(GetMiddleNumber);

    private bool IsInCorrectOrder(List<int> update)
        => update.All(n => IsInCorrectOrder(n, update));

    private bool IsInCorrectOrder(int number, List<int> update)
    {
        var rules = _rules.Where(r => update.Contains(r.X) && update.Contains(r.Y));

        var afterNumbers = rules.Where(e => e.X == number).Select(e => e.Y);
        var previousNumbers = rules.Where(e => e.Y == number).Select(e => e.X);

        return afterNumbers.All(n => update.IndexOf(number) < update.IndexOf(n))
            && previousNumbers.All(n => update.IndexOf(number) > update.IndexOf(n));
    }

    private decimal GetMiddleNumber(List<int> update) => update[(update.Count - 1) / 2];

    public decimal SumMiddleNumbersOfIncorrectOrder
        => _updates.Where(n => !IsInCorrectOrder(n))
                   .Select(Sorted)
                   .Sum(GetMiddleNumber);

    private List<int> Sorted(List<int> update)
    {
        var sorted = new List<int>();

        while (update.Any())
        {
            while (!IsInCorrectOrder(update[0], update))
            {
                var wrongNumber = update[0];
                update.RemoveAt(0);
                update.Add(wrongNumber);
            }

            sorted.Add(update[0]);
            update.RemoveAt(0);
        }

        return sorted;
    }
}