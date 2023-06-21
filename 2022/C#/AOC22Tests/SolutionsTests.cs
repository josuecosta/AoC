using NUnit.Framework;

namespace Aoc22.Tests
{
    [TestFixture()]
    public class SolutionsTests
    {
        [Test()]
        public void Day_Test()
        {
            var result = new Solutions(true);
            Assert.AreEqual("56000011", result.Solution);
        }

        [Test()]
        public void Day_Solution()
        {
            var result = new Solutions();
            System.Console.WriteLine(result.Solution);
        }
    }
}