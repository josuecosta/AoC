﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc22.BL
{
    internal class BeaconExclusionZone
    {
        private HashSet<CoordinatesSB> map;

        public BeaconExclusionZone(string[] data)
        {
            map = InitMap(data);
        }

        private HashSet<CoordinatesSB> InitMap(string[] data)
        {
            var map = new HashSet<CoordinatesSB>();
            foreach (var line in data)
            {
                var regex = Helpers.RegexNumbers.Matches(line);

                var x1Value = int.Parse(regex[0].Value);
                var y1Value = int.Parse(regex[1].Value);

                var x2Value = int.Parse(regex[2].Value);
                var y2Value = int.Parse(regex[3].Value);

                var distance = Helpers.CalcManhattanDistance(x1Value, y1Value, x2Value, y2Value);

                map.Add(new CoordinatesSB(x1Value, y1Value, true, distance));
                map.Add(new CoordinatesSB(x2Value, y2Value, false, 0));
            }
            return map;
        }

        internal decimal GetPositionsWithoutBeacons(int row)
        {
            CalculateSafeArea(row);
            Print();
            return map
                .Where(c =>
                            c.Y == row
                         && c.IsSafe.HasValue
                         && c.IsSafe.Value
                         && !c.IsBeacon)
                .Count();
        }

        private void Print()
        {
            for (int row = -2; row <= 22; row++)
            {
                for (int col = -2; col <= 25; col++)
                {
                    var spot = map.FirstOrDefault(m => m.X == col && m.Y == row);
                    if (spot != null)
                    {
                        Console.Write(spot.IsScanner ? "S" : spot.IsBeacon ? "B" : "#");
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }
                Console.WriteLine();
            }
        }

        private void CalculateSafeArea(int row)
        {
            var scanners = map.Where(c => c.IsScanner).ToList();

            foreach (var scanner in scanners)
            {
                CreateSafeArea(scanner.X, scanner.Y, (int)scanner.Distance, row);
            }
        }

        private void CreateSafeArea(int x, int y, int distance, int row)
        {
            var originalPosition = new Coordinates(x, y);
            // Only create safe area if desired row is in the middle
            if (y - distance <= row && row <= y + distance)
            {
                for (int col = x - distance; col <= x + distance; col++)
                {
                    var coordinate = new Coordinates(col, row);
                    if (Helpers.CalcManhattanDistance(originalPosition, coordinate) <= distance)
                    {
                        map.Add(new CoordinatesSB(coordinate.X, coordinate.Y));
                    }
                }
            }
        }

        private class CoordinatesSB : Coordinates
        {
            public bool? IsSafe { get; set; }
            public bool IsScanner { get; set; }
            public bool IsBeacon { get; set; }
            public decimal Distance { get; set; }

            public CoordinatesSB() : base()
            {
            }

            public CoordinatesSB(int x, int y) : base(x, y)
            {
                IsSafe = true;
            }

            public CoordinatesSB(int x, int y, bool isScanner, decimal distance) : base(x, y)
            {
                IsSafe = true;
                IsScanner = isScanner;
                IsBeacon = !isScanner;
                Distance = distance;
            }
        }
    }
}