namespace AOC24;

internal class Day7
{
    private readonly List<NumbersWrap> _numbers = [];

    public Day7(string[] data)
    {
        for (int row = 0; row < data.Length; row++)
        {
            var rawData = data[row].Split(':');
            var testValue = decimal.Parse(rawData[0]);

            var numbers = rawData[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(n => decimal.Parse(n.Trim())).ToList();
            _numbers.Add(new(testValue, numbers));
        }
    }

    public List<NumbersWrap> NumbersWrap => _numbers;
}

public class NumbersWrap(decimal testValue, List<decimal> numbers)
{
    public decimal TestValue => testValue;

    public List<decimal> Numbers = numbers;

    public bool IsCalibrated(bool hasConcatenate = false)
    {
        HashSet<decimal> numbers = [Numbers[0]];

        for (int i = 1; i < Numbers.Count; i++)
        {
            numbers = GetResults(numbers, Numbers[i], hasConcatenate);
        }

        return numbers.Contains(testValue);
    }

    private HashSet<decimal> GetResults(HashSet<decimal> sets, decimal number, bool hasConcatenate = false)
    {
        HashSet<decimal> results = [];
        foreach (var value in sets)
        {
            if (value > testValue) continue;
            results.Add(value * number);
            results.Add(value + number);
            if (hasConcatenate)
            {
                results.Add(decimal.Parse($"{value}{number}"));
            }
        }
        return results;
    }
}