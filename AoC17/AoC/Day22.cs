namespace AoC17
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Day22 : Master
    {
        private IDictionary<Coordinate, char> map;

        private class InfectionStatus
        {
            public static char Infected = '#';
            public static char Clean = '.';

            // Part 2
            public static char Weakened = 'W';

            public static char Flagged = 'F';
        }

        public void Run()
        {
            InitMap(this.Input);

            // Create the Virus
            var virus = new Virus(Direction.N, GetInitialPosition(this.Input));

            // Perform bursts
            for (int i = 0; i < 10_000; i++)
            {
                virus.Move(map);
            }

            this.Output1 = virus.InfectedNodes;

            // Part 2
            InitMap(this.Input);

            var virusII = new VirusII(Direction.N, GetInitialPosition(this.Input));

            for (int i = 0; i < 10_000_000; i++)
            {
                virusII.Move(map);
            }

            this.Output2 = virusII.InfectedNodes;
        }

        private Coordinate GetInitialPosition(List<string> input) => new Coordinate(input.First().Count() / 2, input.Count / 2);

        private void InitMap(List<string> input)
        {
            map = new Dictionary<Coordinate, char>();

            for (int y = 0; y < input.Count; y++)
            {
                for (int x = 0; x < input[y].Length; x++)
                {
                    map.Add(new Coordinate(x, y), input[y][x]);
                }
            }
        }

        internal class Virus
        {
            public Virus(Direction currentDirection, Coordinate currentPosition)
            {
                CurrentDirection = currentDirection;
                CurrentPosition = currentPosition;
                InfectedNodes = 0;
            }

            public Direction CurrentDirection { get; set; }

            public Coordinate CurrentPosition { get; set; }

            public int InfectedNodes { get; set; }

            public virtual void Move(IDictionary<Coordinate, char> map)
            {
                var mapPosition = this.GetInfectionStatus(map, this.CurrentPosition);

                if (mapPosition != InfectionStatus.Clean)
                {
                    map[CurrentPosition] = InfectionStatus.Clean;
                    TurnRight();
                }
                else
                {
                    map[CurrentPosition] = InfectionStatus.Infected;
                    TurnLeft();
                    InfectedNodes++;
                }
                UpdatePosition(CurrentDirection);
            }

            internal char GetInfectionStatus(IDictionary<Coordinate, char> map, Coordinate position)
            {
                var coordinate = new Coordinate(position.X, position.Y);
                if (!map.ContainsKey(position))
                {
                    map.Add(coordinate, InfectionStatus.Clean);
                }
                return map[coordinate];
            }

            internal void TurnRight()
            {
                switch (CurrentDirection)
                {
                    case Direction.N:
                        CurrentDirection = Direction.E;
                        break;

                    case Direction.S:
                        CurrentDirection = Direction.W;
                        break;

                    case Direction.E:
                        CurrentDirection = Direction.S;
                        break;

                    case Direction.W:
                        CurrentDirection = Direction.N;
                        break;
                }
            }

            internal void TurnLeft()
            {
                switch (CurrentDirection)
                {
                    case Direction.N:
                        CurrentDirection = Direction.W;
                        break;

                    case Direction.S:
                        CurrentDirection = Direction.E;
                        break;

                    case Direction.E:
                        CurrentDirection = Direction.N;
                        break;

                    case Direction.W:
                        CurrentDirection = Direction.S;
                        break;
                }
            }

            internal void UpdatePosition(Direction currentDirection)
            {
                switch (currentDirection)
                {
                    case Direction.N:
                        CurrentPosition.Y--;
                        break;

                    case Direction.S:
                        CurrentPosition.Y++;
                        break;

                    case Direction.E:
                        CurrentPosition.X++;
                        break;

                    case Direction.W:
                        CurrentPosition.X--;
                        break;
                }
            }
        }

        internal class VirusII : Virus
        {
            public VirusII(Direction currentDirection, Coordinate currentPosition)
                : base(currentDirection, currentPosition)
            {
                CurrentDirection = currentDirection;
                CurrentPosition = currentPosition;
                InfectedNodes = 0;
            }

            public override void Move(IDictionary<Coordinate, char> map)
            {
                var mapPosition = this.GetInfectionStatus(map, this.CurrentPosition);

                if (mapPosition == InfectionStatus.Infected)
                {
                    map[CurrentPosition] = InfectionStatus.Flagged;
                    TurnRight();
                }
                else if (mapPosition == InfectionStatus.Clean)
                {
                    map[CurrentPosition] = InfectionStatus.Weakened;
                    TurnLeft();
                }
                else if (mapPosition == InfectionStatus.Weakened)
                {
                    map[CurrentPosition] = InfectionStatus.Infected;
                    InfectedNodes++;
                }
                else
                {
                    map[CurrentPosition] = InfectionStatus.Clean;
                    TurnBack();
                }
                UpdatePosition(CurrentDirection);
            }

            internal void TurnBack()
            {
                switch (CurrentDirection)
                {
                    case Direction.N:
                        CurrentDirection = Direction.S;
                        break;

                    case Direction.S:
                        CurrentDirection = Direction.N;
                        break;

                    case Direction.E:
                        CurrentDirection = Direction.W;
                        break;

                    case Direction.W:
                        CurrentDirection = Direction.E;
                        break;
                }
            }
        }
    }

    public enum Direction
    {
        N,
        S,
        E,
        W,
    }
}