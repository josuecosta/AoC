﻿using NUnit.Framework;

namespace Aoc21.Tests
{
    [TestFixture()]
    public class SolutionsTests
    {
        [Test()]
        public void Day_Test()
        {
            var result = new Solutions(true);
            Assert.AreEqual("3993", result.Solution);
        }

        [Test()]
        public void Day_Solution()
        {
            var result = new Solutions();
            System.Console.WriteLine(result.Solution);
        }

    }
}