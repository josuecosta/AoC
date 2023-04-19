using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aoc21
{
    public class Helpers
    {
        public bool isValidPassword(string item)
        {
            var data = item.Split(' ').Select(x => x.Trim()).ToList();
            var minMax = data[0].Split('-');
            var min = int.Parse(minMax[0].ToString());
            var max = int.Parse(minMax[1].ToString());
            var letter = data[1][0];
            var password = data[2];
            var numberOfLettersInPassword = password.Count(x => x == letter);

            if (numberOfLettersInPassword >= min && numberOfLettersInPassword <= max)
            {
                return true;
            }
            return false;
        }

        public int FixMapCircularReference(string[] data, int x, int y)
        {
            var numberOfTrees = 0;
            var currentX = 0;
            var maxX = data.First().Length;

            for (int i = y; i <= data.Length - 1; i += y)
            {
                currentX += x;
                // Fix map circular reference
                currentX %= maxX;
                if (data[i][currentX] == '#')
                {
                    numberOfTrees++;
                }
            }
            return numberOfTrees;
        }

        public bool ContainsAllElementsOfAList(Dictionary<string, string> passport)
        {
            var requiredKeys = new string[] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };

            return requiredKeys.All(x => passport.ContainsKey(x));
        }
    }

    public class Coordinates
    {
        public Coordinates()
        {
            X = 0;
            Y = 0;
            Weight = 0;
        }

        public Coordinates(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Weight { get; set; }
        public bool IsMarked { get; set; }

        public override bool Equals(object obj)
        {
            var item = obj as Coordinates;

            if (item == null)
            {
                return false;
            }

            return this.X == item.X && this.Y == item.Y;
        }

        public override int GetHashCode()
        {
            return (X, Y).GetHashCode();
        }
    }

    public class Submarine
    {
        public Submarine()
        {
            CurrentPosition = new Coordinates();
        }

        public Submarine(int x, int y)
        {
            CurrentPosition = new Coordinates(x, y);
        }

        public Coordinates CurrentPosition { get; set; }

        public void Move(string direction, int number)
        {
            if (direction == "forward")
            {
                CurrentPosition.X += number;
                CurrentPosition.Y += (number * CurrentPosition.Weight);
            }
            else if (direction == "down")
            {
                CurrentPosition.Weight += number;
            }
            else if (direction == "up")
            {
                CurrentPosition.Weight -= number;
            }
        }
    }

    public class Card
    {
        public Dictionary<int, Coordinates> Numbers { get; set; }
        public int Row { get; set; }
        public int WinNumber { get; internal set; }

        public Card()
        {
            Row = 0;
            Numbers = new Dictionary<int, Coordinates>();
        }

        internal void AddRow(string line)
        {
            var numbers = line.Split(new string[] { " " }, System.StringSplitOptions.RemoveEmptyEntries).Select(n => int.Parse(n.Trim())).ToArray();
            for (int i = 0; i < numbers.Length; i++)
            {
                Numbers.Add(numbers[i], new Coordinates(Row, i));
            }
            Row++;
        }

        internal void Play(int drawNumber)
        {
            if (Numbers.ContainsKey(drawNumber))
            {
                Numbers[drawNumber].IsMarked = true;
            }
        }

        internal bool AmIWinner()
        {
            var positions = Numbers.Values.ToArray();
            for (int row = 0; row < 5; row++)
            {
                var line = positions.Where(c => c.X == row);
                if (line.All(c => c.IsMarked))
                {
                    return true;
                }
            }
            for (int col = 0; col < 5; col++)
            {
                var column = positions.Where(c => c.Y == col);
                if (column.All(c => c.IsMarked))
                {
                    return true;
                }
            }
            return false;
        }

        internal int CalcScore()
        {
            var unMarked = Numbers.Where(d => !d.Value.IsMarked).Sum(v => v.Key);
            return unMarked * WinNumber;
        }
    }

    public class Point
    {
        internal string[] map;

        public Point(string[] data, int y, int x)
        {
            map = data;
            Coordinate = new Coordinates(x, y);
            Value = int.Parse(data[y][x].ToString());
            IsLowPoint = CheckLowPoint(data);
            if (IsLowPoint)
            {
                Basin = GetBasinPoints(Value, x, y).Distinct().ToList();
            }
        }

        private List<Coordinates> GetBasinPoints(int value, int x, int y)
        {
            var l = x - 1;
            var u = y - 1;
            var r = x + 1;
            var d = y + 1;
            var coordinates = new List<Coordinates> { new Coordinates(x, y) };

            if (l >= 0)
            {
                var left = int.Parse(map[y][l].ToString());
                if (left > value && left < 9)
                {
                    coordinates.AddRange(GetBasinPoints(left, l, y));
                }
            }
            if (r < map[0].Length)
            {
                var rigth = int.Parse(map[y][r].ToString());
                if (rigth > value && rigth < 9)
                {
                    coordinates.AddRange(GetBasinPoints(rigth, r, y));
                }
            }
            if (u >= 0)
            {
                var up = int.Parse(map[u][x].ToString());
                if (up > value && up < 9)
                {
                    coordinates.AddRange(GetBasinPoints(up, x, u));
                }
            }
            if (d < map.Length)
            {
                var down = int.Parse(map[d][x].ToString());
                if (down > value && down < 9)
                {
                    coordinates.AddRange(GetBasinPoints(down, x, d));
                }
            }
            return coordinates;
        }

        private bool CheckLowPoint(string[] data)
        {
            var l = Coordinate.X - 1;
            var u = Coordinate.Y - 1;
            var r = Coordinate.X + 1;
            var d = Coordinate.Y + 1;

            if (l >= 0 && int.Parse(data[Coordinate.Y][l].ToString()) <= Value)
            {
                return false;
            }
            if (r < data[0].Length && int.Parse(data[Coordinate.Y][r].ToString()) <= Value)
            {
                return false;
            }
            if (u >= 0 && int.Parse(data[u][Coordinate.X].ToString()) <= Value)
            {
                return false;
            }
            if (d < data.Length && int.Parse(data[d][Coordinate.X].ToString()) <= Value)
            {
                return false;
            }

            return true;
        }

        public Coordinates Coordinate { get; set; }
        public bool IsLowPoint { get; set; }
        public int Value { get; set; }
        public List<Coordinates> Basin { get; set; }
    }

    public class Sea
    {
        public Sea(string[] data)
        {
            OctopusList = new List<Octopus>();
            for (int row = 0; row < data.Length; row++)
            {
                for (int col = 0; col < data[0].Length; col++)
                {
                    OctopusList.Add(new Octopus(col, row, int.Parse(data[row][col].ToString())));
                }
            }
        }

        public static List<Octopus> OctopusList { get; set; }
    }

    public class Octopus
    {
        public Octopus(int col, int row, int value)
        {
            this.Coordinate = new Coordinates(col, row);
            this.Value = value;
        }

        public Coordinates Coordinate { get; set; }
        public int Value { get; set; }
        public bool HasFlashed { get; set; }

        internal void IncreaseEnergyLevel()
        {
            if (!HasFlashed)
            {
                Value++;
                if (Value > 9)
                {
                    HasFlashed = true;
                    Value = 0;
                }
            }
        }

        public override bool Equals(object obj)
        {
            var item = obj as Octopus;

            if (item == null)
            {
                return false;
            }

            return this.Coordinate == item.Coordinate;
        }

        public override int GetHashCode()
        {
            return (this.Coordinate, this.Value, this.HasFlashed).GetHashCode();
        }
    }

    public class Cave
    {
        private static Coordinates lastPosition;
        private static List<Nodes> map;

        public Cave(string[] data, bool isFull = false)
        {
            map = new List<Nodes>();

            for (int row = 0; row < data.Length; row++)
            {
                for (int col = 0; col < data[0].Length; col++)
                {
                    map.Add(new Nodes(col, row, int.Parse(data[row][col].ToString())));
                }
            }

            if (isFull) GenerateFullMap(data);

            lastPosition = new Coordinates(map.Max(c => c.Node.X), map.Max(c => c.Node.Y));
        }

        public static List<Nodes> Map { get => map; }

        public static Coordinates LastPosition { get => lastPosition; }

        internal static bool IsLastPostion(Coordinates coordinate) => coordinate == LastPosition;

        private void GenerateFullMap(string[] data)
        {
            var maxX = data[0].Length;
            var maxY = data.Length;
            for (int right = 1; right < 5; right++)
            {
                for (int row = 0; row < maxY; row++)
                {
                    for (int col = 0; col < maxX; col++)
                    {
                        var indexCol = col + (maxX * right);
                        var num = (int.Parse(data[row][col].ToString()) + right) % 9;
                        var nextNum = num == 0 ? 9 : num;
                        map.Add(new Nodes(indexCol, row, nextNum));
                    }
                }
            }
            maxX = map.Max(c => c.Node.X) + 1;
            for (int down = 1; down < 5; down++)
            {
                for (int row = 0; row < maxY; row++)
                {
                    for (int col = 0; col < maxX; col++)
                    {
                        var indexRow = row + (maxY * down);
                        var num = (map.FirstOrDefault(n => n.Node.X == col && n.Node.Y == row).RiskValue + down) % 9;
                        var nextNum = num == 0 ? 9 : num;
                        map.Add(new Nodes(col, indexRow, nextNum));
                    }
                }
            }
        }
    }

    public class Nodes
    {
        public Nodes(int x, int y, int v)
        {
            this.Node = new Coordinates(x, y);
            this.RiskValue = v;
            this.Weight = decimal.MaxValue;
        }

        public Coordinates Node { get; set; }
        public decimal Weight { get; set; }
        public int RiskValue { get; set; }
    }

    public class Dijkstras
    {
        public Dictionary<string, decimal> ShortestPathTree { get; set; }

        public Dijkstras()
        {
            ShortestPathTree = new Dictionary<string, decimal>();
        }

        public decimal GetMinDistance(Coordinates startPoint, Dictionary<string, Nodes> nodes)
        {
            nodes["X0Y0"].Weight = 0;
            var lastNode = nodes[$"X{nodes.Max(n => n.Value.Node.X)}Y{nodes.Max(n => n.Value.Node.Y)}"];
            while (lastNode.Weight == decimal.MaxValue)
            {
                var nodesToVisit = nodes.Where(p => !ShortestPathTree.ContainsKey(p.Key)).Where(p => p.Value.Weight != decimal.MaxValue);
                var currentNode = nodesToVisit.MinBy(n => n.Value.Weight);
                var currentNodeKey = $"X{currentNode.Value.Node.X}Y{currentNode.Value.Node.Y}";
                ShortestPathTree.Add(currentNodeKey, currentNode.Value.Weight);
                var adjacents = nodes.Where(
                    p => (p.Key == $"X{currentNode.Value.Node.X}Y{currentNode.Value.Node.Y + 1}"
                       || p.Key == $"X{currentNode.Value.Node.X}Y{currentNode.Value.Node.Y - 1}"
                       || p.Key == $"X{currentNode.Value.Node.X + 1}Y{currentNode.Value.Node.Y}"
                       || p.Key == $"X{currentNode.Value.Node.X - 1}Y{currentNode.Value.Node.Y}")
                    ).Where(n => !ShortestPathTree.ContainsKey(n.Key)).ToList();

                UpdateAdjacentDistance(adjacents, currentNode.Value.Weight);
            }

            return lastNode.Weight;
        }

        private void UpdateAdjacentDistance(List<KeyValuePair<string, Nodes>> adjacents, decimal nodeValue)
        {
            foreach (var adj in adjacents)
            {
                var sum = adj.Value.RiskValue + nodeValue;
                adj.Value.Weight = adj.Value.Weight > sum ? sum : adj.Value.Weight;
            }
        }
    }

    public class Package
    {
        public Package()
        {
            SubPackages = new List<Package>();
        }

        public Package(string binNumber)
        {
            var pck = GetPackageInformation(new StringBuilder(binNumber));
            Version = pck.Version;
            TypeID = pck.TypeID;
            Value = pck.Value;
            SubPackages = pck.SubPackages;
        }

        public int Version { get; set; }
        public int TypeID { get; set; }

        private long _value;
        public long Value
        {
            get
            {
                switch (TypeID)
                {
                    case 0:
                        return SubPackages.Sum(p => p.Value);
                    case 1:
                        return SubPackages.Aggregate((long)1, (acc, val) => acc * val.Value);
                    case 2:
                        return SubPackages.Min(p => p.Value);
                    case 3:
                        return SubPackages.Max(p => p.Value);
                    case 5:
                        return SubPackages[0].Value > SubPackages[1].Value ? 1 : 0;
                    case 6:
                        return SubPackages[0].Value < SubPackages[1].Value ? 1 : 0;
                    case 7:
                        return SubPackages[0].Value == SubPackages[1].Value ? 1 : 0;

                    default:
                        return _value;
                }
            }
            set => _value = value;
        }

        public List<Package> SubPackages { get; set; }

        internal Package GetPackageInformation(StringBuilder binNumber)
        {
            Package pck = new Package();

            pck.Version = Convert.ToInt32(binNumber.ToString().Substring(0, 3), 2);
            binNumber = binNumber.Remove(0, 3);
            pck.TypeID = Convert.ToInt32(binNumber.ToString().Substring(0, 3), 2);
            binNumber = binNumber.Remove(0, 3);

            if (pck.TypeID == 4)
            {
                pck.Value = GetLiteralValue(binNumber);
            }
            else
            {
                var lengthTypeId = binNumber[0];
                binNumber = binNumber.Remove(0, 1);
                if (lengthTypeId == '0')
                {
                    var subPackLength = Convert.ToInt32(binNumber.ToString().Substring(0, 15), 2);
                    binNumber = binNumber.Remove(0, 15);

                    var tempSb = new StringBuilder(binNumber.ToString().Substring(0, subPackLength));
                    binNumber = binNumber.Remove(0, subPackLength);
                    while (tempSb.Length > 0)
                    {
                        pck.SubPackages.Add(GetPackageInformation(tempSb));
                    }
                }
                else
                {
                    var subPackLength = Convert.ToInt32(binNumber.ToString().Substring(0, 11), 2);
                    binNumber = binNumber.Remove(0, 11);
                    for (int i = 0; i < subPackLength; i++)
                    {
                        pck.SubPackages.Add(GetPackageInformation(binNumber));
                    }
                }
            }
            return pck;
        }

        private long GetLiteralValue(StringBuilder binNumber)
        {
            var value = new StringBuilder();
            var isLastGroup = false;
            while (!isLastGroup)
            {
                isLastGroup = binNumber[0] == '0';
                value.Append(binNumber.ToString().Substring(1, 4));
                binNumber = binNumber.Remove(0, 5);
            }
            return Convert.ToInt64(value.ToString(), 2);
        }

        internal decimal GetSumOfVersions()
        {
            if (SubPackages == null || !SubPackages.Any())
            {
                return Version;
            }
            return SubPackages.Sum(p => p.GetSumOfVersions()) + Version;
        }
    }
}