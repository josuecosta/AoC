namespace AOC24;

internal class CeresSearch(string[] data)
{
    private readonly Dictionary<Coordinates, string> _map = InitMap(data);

    private static Dictionary<Coordinates, string> InitMap(string[] data)
    {
        var dic = new Dictionary<Coordinates, string>();
        for (int row = 0; row < data.Length; row++)
        {
            for (int col = 0; col < data[row].Length; col++)
            {
                dic.Add(new Coordinates(col, row), data[row][col].ToString());
            }
        }
        return dic;
    }

    public decimal CountOfOccurrences => _map.Sum(p => p.Key.Occurrences);

    internal void FindNumberOfOccurrences()
    {
        foreach (var coordinate in _map.Where(k => k.Value.Equals("X")))
        {
            foreach (Directions direction in Enum.GetValues(typeof(Directions)))
            {
                coordinate.Key.Occurrences += HasWord(direction, coordinate.Key) ? 1 : 0;
            }
        }
    }

    private bool HasWord(Directions direction, Coordinates coordinate)
    {
        int row = 0;
        int col = 0;

        switch (direction)
        {
            case Directions.Right:
                row = 0; col = 1;
                break;

            case Directions.Left:
                row = 0; col = -1;
                break;

            case Directions.Up:
                row = -1; col = 0;
                break;

            case Directions.Down:
                row = 1; col = 0;
                break;

            case Directions.Diagonal1:
                row = -1; col = -1;
                break;

            case Directions.Diagonal2:
                row = -1; col = 1;
                break;

            case Directions.Diagonal3:
                row = 1; col = -1;
                break;

            case Directions.Diagonal4:
                row = 1; col = 1;
                break;

            default:
                break;
        }

        return HasWord(col, row, coordinate.X, coordinate.Y);
    }

    private readonly string WORD = "XMAS";

    private bool HasWord(int colOffset, int rowOffset, int x, int y, int nextPosition = 1)
    {
        if (nextPosition >= WORD.Length)
        {
            return true;
        }

        x += colOffset;
        y += rowOffset;

        if (_map.TryGetValue(new Coordinates(x, y), out string? letter))
        {
            if (letter == WORD[nextPosition].ToString())
            {
                return HasWord(colOffset, rowOffset, x, y, nextPosition + 1);
            }
        }

        return false;
    }

    internal void FindNumberOfCrossOccurrences()
    {
        foreach (var coordinate in _map.Where(k => k.Value.Equals("A")))
        {
            coordinate.Key.Occurrences += HasCrossWord(coordinate.Key) ? 1 : 0;
        }
    }

    private bool HasCrossWord(Coordinates coordinate)
    {
        List<int[][]> offSets = [[[-1, -1], [1, 1]], [[1, -1], [-1, 1]]];
        foreach (var group in offSets)
        {
            List<string> letters = [];
            foreach (var offset in group)
            {
                if (!_map.TryGetValue(new Coordinates(coordinate.X + offset[0], coordinate.Y + offset[1]), out string? letter))
                {
                    return false;
                }
                letters.Add(letter);
            }

            if (!(letters.Count(l => l == "M") == 1
               && letters.Count(l => l == "S") == 1))
            {
                return false;
            }
        }
        return true;
    }
}