namespace AOC24;

internal class ReportsRedNosed
{
    private readonly List<int[]> reports;

    public ReportsRedNosed(string[] data)
    {
        this.reports = ParseReports(data);
    }

    private List<int[]> ParseReports(string[] data)
    {
        List<int[]> reports = [];
        for (int i = 0; i < data.Length; i++)
        {
            var report = data[i].Split(' ').Select(n => int.Parse(n)).ToArray();
            reports.Add(report);
        }
        return reports;
    }

    public List<int[]> Reports => reports;

    public bool IsValidReport(int[] report, bool includeDampener = false)
    {
        return AreAllIncreasingOrDecreasing(report) && HaveCorrectAdjacentLevels(report)
            || (includeDampener && CheckWithDampenerVersion(report));
    }

    private bool CheckWithDampenerVersion(int[] report)
    {
        var alternativeVersions = GetAllAlternativeVersions(report);
        return alternativeVersions.Any(v => AreAllIncreasingOrDecreasing(v) && HaveCorrectAdjacentLevels(v));
    }

    private static List<int[]> GetAllAlternativeVersions(int[] report)
    {
        List<int[]> allVersions = [];
        for (int i = 0; i < report.Length; i++)
        {
            var newVersion = report.ToList();
            newVersion.RemoveAt(i);
            allVersions.Add(newVersion.ToArray());
        }

        return allVersions;
    }

    private bool HaveCorrectAdjacentLevels(int[] report)
    {
        for (int i = 0; i < report.Length - 1; i++)
        {
            var diff = Math.Abs(report[i] - report[i + 1]);
            if (1 > diff || diff > 3)
            {
                return false;
            }
        }
        return true;
    }

    private bool AreAllIncreasingOrDecreasing(int[] report)
    {
        return AreAllIncreasing(report) || AreAllDecreasing(report);
    }

    private bool AreAllDecreasing(int[] report)
    {
        for (int i = 0; i < report.Length - 1; i++)
        {
            if (report[i] < report[i + 1])
            {
                return false;
            }
        }
        return true;
    }

    private bool AreAllIncreasing(int[] report)
    {
        for (int i = 0; i < report.Length - 1; i++)
        {
            if (report[i] > report[i + 1])
            {
                return false;
            }
        }
        return true;
    }
}