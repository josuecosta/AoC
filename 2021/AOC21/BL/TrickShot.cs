using Aoc21;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC21.BL
{
    internal class TrickShot
    {
        private int xMin;
        private int xMax;
        private int yMin;
        private int yMax;
        private Func<int, bool> IsXAtTargetArea;
        private Func<int, bool> IsYAtTargetArea;
        public bool IsAtTargetArea => IsXAtTargetArea(ProbePosition.X)
                                   && IsYAtTargetArea(ProbePosition.Y);
        private int xVelocity;
        private int yVelocity;
        public Coordinates ProbePosition { get; set; }
        private HashSet<string> velocities;

        public TrickShot(int xMin, int xMax, int yMin, int yMax)
        {
            this.xMin = xMin;
            this.xMax = xMax;
            this.yMin = yMin;
            this.yMax = yMax;
            this.IsXAtTargetArea = x => Enumerable.Range(xMin, Math.Abs(xMax - xMin) + 1).Contains(x);
            this.IsYAtTargetArea = y => Enumerable.Range(yMin, Math.Abs(yMax - yMin) + 1).Contains(y);
            this.ProbePosition = new Coordinates(0, 0);
            this.velocities = new HashSet<string>();
        }

        internal decimal GetHighestY()
        {
            var highestY = int.MinValue;

            int minX = GetMinX(0, 0);

            for (int i = minX; i <= xMax; i++)
            {
                for (int j = yMin; j < 1000; j++)
                {
                    var bestY = Shot(i, j);
                    if (highestY < bestY)
                    {
                        highestY = bestY;
                    }
                }
            }
            return highestY;
        }

        private int Shot(int i, int j)
        {
            this.xVelocity = i;
            this.yVelocity = j;
            ProbePosition = new Coordinates(0, 0);
            var highestY = int.MinValue;

            while (!IsAtTargetArea)
            {
                DoStep(ProbePosition);
                UpdateVelocity();

                // Update High
                highestY = highestY < ProbePosition.Y ? ProbePosition.Y : highestY;

                if (ProbePosition.Y < this.yMin
                 || ProbePosition.X > this.xMax)
                {
                    return -1;
                }
            }

            this.velocities.Add($"{i},{j}"); // Part 2
            return highestY;
        }

        private void UpdateVelocity()
        {
            if (xVelocity != 0)
            {
                xVelocity = xVelocity > 0 ? xVelocity - 1 : xVelocity + 1;
            }

            yVelocity--;
        }

        private void DoStep(Coordinates probe)
        {
            probe.X += xVelocity;
            probe.Y += yVelocity;
        }

        private int GetMinX(int index, int accumulator)
        {
            if (accumulator >= this.xMin)
            {
                return index;
            }
            index++;
            return GetMinX(index, index + accumulator);
        }

        internal decimal GetAllInitialVelocityValues()
        {
            GetHighestY();
            return this.velocities.Count;
        }

    }
}
