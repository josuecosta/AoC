using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc23.BL
{
    internal class RegolithReservoir
    {
        private HashSet<Coordinates> cave;
        private Coordinates startPoint = new Coordinates(500, 0);
        private int floorPoint;

        public RegolithReservoir(string[] data)
        {
            cave = InitMap(data);
            floorPoint = cave.Select(c => c.Y).Max() + 2;
        }

        private HashSet<Coordinates> InitMap(string[] data)
        {
            var map = new HashSet<Coordinates>();
            foreach (var line in data)
            {
                var paths = line.Split(new string[] { " -> " }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < paths.Length - 1; i++)
                {
                    var xy = paths[i].Split(',');
                    var xyFrom = new Coordinates(int.Parse(xy[0]), int.Parse(xy[1]));
                    xy = paths[i + 1].Split(',');
                    var xyTo = new Coordinates(int.Parse(xy[0]), int.Parse(xy[1]));
                    FillMap(map, xyFrom, xyTo);
                }
            }
            return map;
        }

        private void FillMap(HashSet<Coordinates> map, Coordinates xyFrom, Coordinates xyTo)
        {
            var isEqualX = xyFrom.X == xyTo.X;

            var count = isEqualX
                ? Math.Abs(xyFrom.Y - xyTo.Y)
                : Math.Abs(xyFrom.X - xyTo.X);

            var x = isEqualX
                ? xyFrom.X
                : xyFrom.X > xyTo.X
                    ? xyTo.X
                    : xyFrom.X;

            var y = !isEqualX
                ? xyFrom.Y
                : xyFrom.Y > xyTo.Y
                    ? xyTo.Y
                    : xyFrom.Y;

            for (int i = 0; i <= count; i++)
            {
                map.Add(new Coordinates(x, y, true));
                if (isEqualX)
                {
                    y++;
                }
                else
                {
                    x++;
                }
            }
        }

        internal decimal GetUnitsOfSandAtRest()
        {
            var steps = 0;

            while (HasAvailableSpace())
            {
                steps++;
            }

            return steps;
        }

        private bool HasAvailableSpace()
        {
            var x = startPoint.X;
            var y = startPoint.Y;

            var minX = cave.Select(c => c.X).Min();
            var maxX = cave.Select(c => c.X).Max();
            var maxY = cave.Select(c => c.Y).Max();

            while (minX <= x && x <= maxX && y < maxY)
            {
                if (!cave.Contains(new Coordinates(x, y + 1)))
                {
                    y++;
                    continue;
                }

                // blocked
                if (!cave.Contains(new Coordinates(x - 1, y + 1)))
                {
                    x--;
                    y++;
                    continue;
                }

                if (!cave.Contains(new Coordinates(x + 1, y + 1)))
                {
                    x++;
                    y++;
                    continue;
                }

                cave.Add(new Coordinates(x, y));
                return true;
            }
            return false;
        }

        internal decimal GetUnitsOfSandToBlockSource()
        {
            var steps = 1M;

            while (!IsBlocked())
            {
                steps++;
            }

            return steps;
        }

        private bool IsBlocked()
        {
            var x = startPoint.X;
            var y = startPoint.Y;

            while (true)
            {
                if (!cave.Contains(new Coordinates(x, y + 1)) && y + 1 < floorPoint)
                {
                    y++;
                    continue;
                }

                if (!cave.Contains(new Coordinates(x - 1, y + 1)) && y + 1 < floorPoint)
                {
                    x--;
                    y++;
                    continue;
                }

                if (!cave.Contains(new Coordinates(x + 1, y + 1)) && y + 1 < floorPoint)
                {
                    x++;
                    y++;
                    continue;
                }

                if (x == startPoint.X && y == startPoint.Y)
                {
                    return true;
                }

                cave.Add(new Coordinates(x, y));
                return false;
            }
        }
    }
}