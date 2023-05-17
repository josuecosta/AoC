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
            // var neighbours = GetNeighbours(// start one);
            throw new NotImplementedException();
        }

        private IEnumerable<Coordinates> GetNeighbours(Elevation elevation)
        {
            var x = elevation.Position.X;
            var y = elevation.Position.Y;

            return Map.Keys.ToArray().Where(k =>
                (k.X == x || k.X == x + 1 || k.X == x - 1)
             && (k.Y == y || k.Y == y + 1 || k.Y == y - 1)
             && (!k.IsMarked));
        }
    }
}