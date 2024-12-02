namespace AOC24;

public class Solutions
{
    public static string Input = string.Empty;
    public string Solution;

    #region DAY 1

    public decimal Day1Solve()
    {
        var data = File.ReadAllLines(Input);
        decimal sum = 0;

        var leftList = new List<int>();
        var rightList = new List<int>();

        for (int i = 0; i < data.Length; i++)
        {
            var locationIds = data[i].Split(' ');
            leftList.Add(int.Parse(locationIds.First()));
            rightList.Add(int.Parse(locationIds.Last()));
        }

        leftList.Sort();
        rightList.Sort();

        for (int i = 0; i < leftList.Count; i++)
        {
            sum += Math.Abs(leftList[i] - rightList[i]);
        }

        return sum;
    }

    public decimal Day1Solve2()
    {
        var data = File.ReadAllLines(Input);
        decimal sum = 0;

        var leftList = new List<int>();
        var rightList = new List<int>();

        for (int i = 0; i < data.Length; i++)
        {
            var locationIds = data[i].Split(' ');
            leftList.Add(int.Parse(locationIds.First()));
            rightList.Add(int.Parse(locationIds.Last()));
        }

        for (int i = 0; i < leftList.Count; i++)
        {
            sum += leftList[i] * rightList.Count(n => n == leftList[i]);
        }

        return sum;
    }

    #endregion DAY 1

    #region DAY 2

    public decimal Day2Solve()
    {
        var data = File.ReadAllLines(Input);

        var reports = new ReportsRedNosed(data);

        return reports.Reports.Count(r => reports.IsValidReport(r));
    }

    public decimal Day2Solve2()
    {
        var data = File.ReadAllLines(Input);

        var reports = new ReportsRedNosed(data);

        return reports.Reports.Count(g => reports.IsValidReport(g, true)); // < 586
    }

    #endregion DAY 2

    public Solutions(bool isTest = false)
    {
        Input = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)!, isTest ? "input-test.txt" : "input.txt");
        Solution = Day2Solve2().ToString();
    }
}