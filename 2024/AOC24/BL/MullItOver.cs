using System.Text.RegularExpressions;

namespace AOC24;

internal class MullItOver(string[] data)
{
    private readonly string _line = string.Join(string.Empty, data);

    public decimal GetSum => GetSumOfResults(_line);

    private static readonly Regex RegexMul = new("mul\\([0-9]*,[0-9]*\\)+");

    private static decimal GetSumOfResults(string line)
        => RegexMul.Matches(line)
                   .Select(m => CalcMultiplicationsFromExpression(m.Value))
                   .Sum();

    private static decimal CalcMultiplicationsFromExpression(string expression)
        => Helpers.RegexNumbers.Matches(expression)
                               .Select(number => Convert.ToDecimal(number.Value))
                               .Aggregate((acc, n) => acc * n);

    public decimal GetSumEnableDisable => GetSumByLineEnableDisable(_line);

    private static readonly Regex RegexEnable = new("do\\(\\)+");
    private static readonly Regex RegexDisable = new("don't\\(\\)+");

    public decimal GetSumByLineEnableDisable(string line)
    {
        line = RemoveDisablesRecursive(line);
        return GetSumOfResults(line);
    }

    private string RemoveDisablesRecursive(string line, int index = 0)
    {
        var nextDont = RegexDisable.Match(line, index);
        if (!nextDont.Success) return line;
        var nextDo = RegexEnable.Match(line, nextDont.Index);
        if (!nextDo.Success) return line.Remove(nextDont.Index);
        line = line.Remove(nextDont.Index, nextDo.Index - nextDont.Index);
        return RemoveDisablesRecursive(line, nextDont.Index);
    }
}