﻿using NUnit.Framework;

namespace Aoc22.Tests
{
    [TestFixture()]
    public class SolutionsTests
    {
        [Test()]
        public void Day_Test()
        {
            var result = new Solutions(true);
            Assert.AreEqual("CMZ", result.Solution);
        }

        [Test()]
        public void Day_Solution()
        {
            var result = new Solutions();
            System.Console.WriteLine(result.Solution);
        }

    }
}