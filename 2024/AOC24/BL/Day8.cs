namespace AOC24;

internal class Day8
{
    private readonly Dictionary<Coordinates, string> _map = [];

    public Day8(string[] data)
    {
        for (int row = 0; row < data.Length; row++)
        {
            for (int col = 0; col < data[row].Length; col++)
            {
                var value = data[row][col].ToString();
                _map.Add(new Coordinates(col, row), value);
            }
        }
    }

    public decimal GetAntiNodes()
    {
        HashSet<Coordinates> antiNodes = [];
        var groups = _map.Where(d => Helpers.RegexAlphaNumericChars.IsMatch(d.Value))
                         .GroupBy(k => k.Value)
                         .ToDictionary(k => k.Key, v => v.Select(x => x.Key));

        foreach (var group in groups)
        {
            foreach (var coordinate in group.Value)
            {
                antiNodes.UnionWith(GetAntiNodesByCoordinate(coordinate, group.Value));
            }
        }

        return antiNodes.Count;
    }

    private IEnumerable<Coordinates> GetAntiNodesByCoordinate(Coordinates coordinate, IEnumerable<Coordinates> coordinates)
    {
        HashSet<Coordinates> antiNodes = [];
        foreach (var item in coordinates)
        {
            if (item == coordinate) continue;
            var distance = GetDistance(coordinate, item);
            var antinode = new Coordinates(coordinate.X + distance.X, coordinate.Y + distance.Y);
            if (_map.ContainsKey(antinode))
            {
                antiNodes.Add(antinode);
            }
        }
        return antiNodes;
    }

    public decimal GetAntiNodesWithUpdatedModel()
    {
        HashSet<Coordinates> antiNodes = [];
        var groups = _map.Where(d => Helpers.RegexAlphaNumericChars.IsMatch(d.Value))
                         .GroupBy(k => k.Value)
                         .ToDictionary(k => k.Key, v => v.Select(x => x.Key));

        foreach (var group in groups.Where(v => v.Value.Count() > 1))
        {
            foreach (var coordinate in group.Value)
            {
                antiNodes.UnionWith(GetAntiNodesByCoordinateWithUpdatedModel(coordinate, group.Value));
            }
        }

        return antiNodes.Count;
    }

    private IEnumerable<Coordinates> GetAntiNodesByCoordinateWithUpdatedModel(Coordinates coordinate, IEnumerable<Coordinates> coordinates)
    {
        HashSet<Coordinates> antiNodes = [];
        foreach (var item in coordinates)
        {
            if (item == coordinate)
            {
                antiNodes.Add(coordinate);
                continue;
            }

            var distance = GetDistance(coordinate, item);
            var xDelta = distance.X;
            var yDelta = distance.Y;

            while (true)
            {
                var antinode = new Coordinates(coordinate.X + distance.X, coordinate.Y + distance.Y);
                if (!_map.ContainsKey(antinode)) break;
                antiNodes.Add(new(antinode.X, antinode.Y));
                distance.X += xDelta;
                distance.Y += yDelta;
            }
        }
        return antiNodes;
    }

    private Coordinates GetDistance(Coordinates coordinate1, Coordinates coordinate2)
        => new(coordinate1.X - coordinate2.X, coordinate1.Y - coordinate2.Y);
}