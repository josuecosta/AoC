using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc22.BL
{
    internal class HillClimbingAlgorithm
    {
        private enum EPositions
        {
            S = 'S',
            E = 'E',
        }

        private class Elevation
        {
            private char high;

            public Elevation(char high, Coordinates coordinate)
            {
                this.high = high;
                this.Position = coordinate;
            }

            public int Value
            {
                get
                {
                    switch (High)
                    {
                        case (char)EPositions.S:
                            return (int)'a';

                        case (char)EPositions.E:
                            return (int)'z';

                        default:
                            return (int)this.high;
                    }
                }
            }

            public char High => this.high;
            public Coordinates Position { get; set; }
        }

        private IDictionary<Coordinates, Elevation> Map;

        public HillClimbingAlgorithm(string[] data)
        {
            this.Map = InitMap(data);
        }

        private Dictionary<Coordinates, Elevation> InitMap(string[] data)
        {
            var map = new Dictionary<Coordinates, Elevation>();
            for (int y = 0; y < data.Length; y++)
            {
                for (int x = 0; x < data[y].Length; x++)
                {
                    var coordinate = new Coordinates(x, y);
                    map.Add(coordinate, new Elevation(data[y][x], coordinate));
                }
            }
            return map;
        }

        internal decimal GetSteps()
        {
            var start = this.Map.Values.Single(e => e.High == (char)EPositions.S);
            var end = this.Map.Values.Single(e => e.High == (char)EPositions.E);
            start.Position.IsMarked = true;

            return CalcDistance(start, end, 0);
        }

        private int CalcDistance(Elevation start, Elevation end, int count)
        {
            if (start == end)
            {
                return count;
            }

            var neighbours = GetNeighbours(start);

            var possibleNextSteps = GetNextSteps(start.Value, neighbours);

            possibleNextSteps = FilterMore(possibleNextSteps, start.Value);

            var minDistance = int.MaxValue;
            foreach (var nextStep in possibleNextSteps)
            {
                nextStep.Position.IsMarked = true;
                var tempDist = CalcDistance(nextStep, end, count+1);
                nextStep.Position.IsMarked = false;
                if (tempDist < minDistance)
                {
                    minDistance = tempDist;
                }
            }
            return minDistance;
        }

        private IEnumerable<Elevation> FilterMore(IEnumerable<Elevation> possibleNextSteps, int value)
        {
            var best = possibleNextSteps.Where(e => e.Value != value);
            return !best.Any() ? possibleNextSteps : best;
        }

        private static IEnumerable<Elevation> GetNextSteps(int value, IEnumerable<Elevation> neighbours)
            => neighbours.Where(e => value <= e.Value && e.Value <= value + 1 
                                 && !e.Position.IsMarked);

        private IEnumerable<Elevation> GetNeighbours(Elevation elevation)
        {
            var eX = elevation.Position.X;
            var eY = elevation.Position.Y;

            var keys = Map.Keys.Where(k =>
                (k.X == eX && (k.Y == eY + 1 || k.Y == eY - 1))
             || (k.Y == eY && (k.X == eX + 1 || k.X == eX - 1)));

            return Map.Where(k => keys.Contains(k.Key)).Select(k => k.Value);
        }
    }
}