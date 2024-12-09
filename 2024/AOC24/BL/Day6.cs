namespace AOC24;

internal class Day6
{
    private readonly Dictionary<Coordinates, string> _map = [];

    public Day6(string[] data)
    {
        for (int row = 0; row < data.Length; row++)
        {
            for (int col = 0; col < data[row].Length; col++)
            {
                var value = data[row][col].ToString();
                _map.Add(new Coordinates(col, row), value);
            }
        }
        _currentDirection = Directions.Up;
    }

    private Directions _currentDirection;

    public void DoPatroll()
    {
        KeyValuePair<Coordinates, string>? position = _map.Single(p => p.Value == "^");
        _map[position.Value.Key] = ".";

        Coordinates? currentCoordinate = position.Value.Key;
        while (currentCoordinate != null)
        {
            VisitedPositions.Add(currentCoordinate);
            currentCoordinate = GetNextCoordinate(currentCoordinate);
        };
    }

    public HashSet<Coordinates> VisitedPositions = [];

    private Coordinates? GetNextCoordinate(Coordinates position)
    {
        while (true)
        {
            var (rowDelta, colDelta, nextDirection) = Helpers.DirectionLookup[_currentDirection];

            var nextCoordinate = new Coordinates(position.X + colDelta, position.Y + rowDelta);

            if (!_map.TryGetValue(nextCoordinate, out var nextValue))
            {
                return null;
            }

            if (nextValue != ".")
            {
                _currentDirection = nextDirection;
            }
            else
            {
                return nextCoordinate;
            }
        }
    }

    internal void DoPatrollWithLoops()
    {
        KeyValuePair<Coordinates, string>? position = _map.Single(p => p.Value == "^");
        _map[position.Value.Key] = ".";

        var initialPosition = new Coordinates(position.Value.Key.X, position.Value.Key.Y);

        Coordinates? currentCoordinate = position.Value.Key;
        while (currentCoordinate != null)
        {
            VisitedPositions.Add(currentCoordinate);
            currentCoordinate = GetNextCoordinate(currentCoordinate);
        };

        var path = VisitedPositions.Except([initialPosition]).ToList();
        for (int i = 0; i < path.Count; i++)
        {
            var historyPath = new Dictionary<Coordinates, HashSet<Directions>>();
            var obstructionCoordinate = path[i];

            _currentDirection = Directions.Up;
            currentCoordinate = initialPosition;

            _map[obstructionCoordinate] = "O";
            while (true)
            {
                currentCoordinate = GetNextCoordinate(currentCoordinate!);
                if (currentCoordinate == null)
                {
                    break;
                }

                if (IsLoop(historyPath, currentCoordinate))
                {
                    SuccessObstructions++;
                    break;
                }

                if (!historyPath.TryGetValue(currentCoordinate, out var directions))
                {
                    directions = [];
                    historyPath[currentCoordinate] = directions;
                }
                directions.Add(_currentDirection);
            };
            _map[obstructionCoordinate] = ".";
        }
    }

    public decimal SuccessObstructions = 0;

    private bool IsLoop(Dictionary<Coordinates, HashSet<Directions>> history, Coordinates position)
        => history.TryGetValue(position, out var directions) && directions.Contains(_currentDirection);
}