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

        return reports.Reports.Count(g => reports.IsValidReport(g, true));
    }

    #endregion DAY 2

    #region DAY 3

    public decimal Day3Solve()
    {
        var data = File.ReadAllLines(Input);

        var mull = new MullItOver(data);

        return mull.GetSum;
    }

    public decimal Day3Solve2()
    {
        var data = File.ReadAllLines(Input);

        var mull = new MullItOver(data);

        return mull.GetSumEnableDisable;
    }

    #endregion DAY 3

    #region DAY 4

    public decimal Day4Solve()
    {
        var data = File.ReadAllLines(Input);

        var mull = new CeresSearch(data);

        mull.FindNumberOfOccurrences();

        return mull.CountOfOccurrences;
    }

    public decimal Day4Solve2()
    {
        var data = File.ReadAllLines(Input);

        var mull = new CeresSearch(data);

        mull.FindNumberOfCrossOccurrences();

        return mull.CountOfOccurrences;
    }

    #endregion DAY 4

    #region DAY 5

    public decimal Day5Solve()
    {
        var data = File.ReadAllLines(Input);

        var day = new Day5(data);

        return day.SumMiddleNumbers;
    }

    public decimal Day5Solve2()
    {
        var data = File.ReadAllLines(Input);

        var day = new Day5(data);

        return day.SumMiddleNumbersOfIncorrectOrder;
    }

    #endregion DAY 5

    #region DAY 6

    public decimal Day6Solve()
    {
        var data = File.ReadAllLines(Input);

        var day = new Day6(data);

        day.DoPatroll();

        return day.VisitedPositions.Count;
    }

    public decimal Day6Solve2()
    {
        var data = File.ReadAllLines(Input);

        var day = new Day6(data);

        day.DoPatrollWithLoops();

        return day.SuccessObstructions;
    }

    #endregion DAY 6

    #region DAY 7

    public decimal Day7Solve()
    {
        var data = File.ReadAllLines(Input);

        var day = new Day7(data);

        return day.NumbersWrap.Where(n => n.IsCalibrated()).Sum(n => n.TestValue);
    }

    public decimal Day7Solve2()
    {
        var data = File.ReadAllLines(Input);

        var day = new Day7(data);

        return day.NumbersWrap.Where(n => n.IsCalibrated(true)).Sum(n => n.TestValue); // 44841372855953 == 45071967642319
    }

    #endregion DAY 7

    public Solutions(bool isTest = false)
    {
        Input = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)!, isTest ? "input-test.txt" : "input.txt");
        Solution = Day7Solve2().ToString();
    }
}