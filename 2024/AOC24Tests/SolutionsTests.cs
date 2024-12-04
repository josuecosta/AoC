using AOC24;
using Xunit.Abstractions;

namespace AOC24Tests;

public class SolutionsTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void Day_Test()
    {
        var result = new Solutions(true);
        result.Solution.Should().Be("9");
    }

    [Fact]
    public void Day_Solution()
    {
        var result = new Solutions();
        testOutputHelper.WriteLine(result.Solution);
    }
}