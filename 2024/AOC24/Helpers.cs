using System.Text.RegularExpressions;

namespace AOC24;

public class Helpers
{
    public static Regex RegexPairInsideBrackets = new Regex("\\[[0-9,\\[\\]]*\\]");
    public static Regex RegexNumbers = new Regex("[0-9]+");
    public static Regex RegexAlphaNumericChars = new Regex("[0-9a-zA-Z]");

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

        return requiredKeys.All(passport.ContainsKey);
    }

    public static decimal CalcManhattanDistance(Coordinates c1, Coordinates c2)
        => Math.Abs(c1.X - c2.X) + Math.Abs(c1.Y - c2.Y);

    public static decimal CalcManhattanDistance(int x1, int y1, int x2, int y2)
        => CalcManhattanDistance(new Coordinates(x1, y1), new Coordinates(x2, y2));

    public static readonly Dictionary<Directions, (int RowDelta, int ColDelta, Directions NextDirection)> DirectionLookup = new()
    {
        { Directions.Right, (0, 1, Directions.Down) },
        { Directions.Left, (0, -1, Directions.Up) },
        { Directions.Up, (-1, 0, Directions.Right) },
        { Directions.Down, (1, 0, Directions.Left) }
    };
}

public class Coordinates
{
    public Coordinates()
    {
        X = 0;
        Y = 0;
        Occurrences = 0;
    }

    public Coordinates(int x, int y)
    {
        X = x;
        Y = y;
        Occurrences = 0;
    }

    public Coordinates(int x, int y, bool isMarked) : this(x, y)
    {
        IsMarked = isMarked;
    }

    public int X { get; set; }
    public int Y { get; set; }
    public bool IsMarked { get; set; }
    public decimal Occurrences { get; set; }

    public override bool Equals(object? obj)
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

public enum Directions
{
    Right,
    Left,
    Up,
    Down,

    Diagonal1,
    Diagonal2,
    Diagonal3,
    Diagonal4
}