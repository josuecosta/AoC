using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc23
{
    internal class CubeConundrumGame
    {
        private List<GameCube> games;

        public CubeConundrumGame(string[] data)
        {
            this.games = ParseGames(data);
        }

        private List<GameCube> ParseGames(string[] data)
        {
            var games = new List<GameCube>();
            for (int i = 0; i < data.Length; i++)
            {
                var game = new GameCube
                {
                    ID = int.Parse(data[i].Substring(5, data[i].IndexOf(':') - 5)),
                    Subsets = GetSubsets(data[i].Substring(data[i].IndexOf(':') + 1))
                };

                games.Add(game);
            }
            return games;
        }

        private List<SubsetsCube> GetSubsets(string data)
        {
            var subsets = new List<SubsetsCube>();
            var splitData = data.Split(';');
            foreach (var subset in splitData)
            {
                var plays = subset.Split(',');
                foreach (var play in plays)
                {
                    subsets.Add(new SubsetsCube(play));
                }
            }
            return subsets; 
        }

        public List<GameCube> Games => games;  
    }

    internal class GameCube
    {
        public int ID { get; set; }
        public bool IsPossible
            => !Subsets.Any(v =>
                        (v.Color == ColorCube.Red && v.Value > 12)
                     || (v.Color == ColorCube.Green && v.Value > 13)
                     || (v.Color == ColorCube.Blue && v.Value > 14));
        public List<SubsetsCube> Subsets { get; set; }
        public decimal PowerOfSetCubes
        {
            get
            {
                var group = Subsets.GroupBy(s => s.Color);

                var green = group.Where(s => s.Key == ColorCube.Green).SelectMany(s => s).Max(x => x.Value);
                var red = group.Where(s => s.Key == ColorCube.Red).SelectMany(s => s).Max(x => x.Value);
                var blue = group.Where(s => s.Key == ColorCube.Blue).SelectMany(s => s).Max(x => x.Value);
                
                return green * red * blue;
            }
        }
    }

    internal class SubsetsCube
    {
        private string subset;

        public SubsetsCube(string subset)
        {
            this.subset = subset.Trim();
            var data = subset.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            this.Value = int.Parse(data[0]);
            this.Color = GetColor(data[1]);
        }

        private ColorCube GetColor(string str)
        {
            switch (str)
            {
                case "red":
                    return ColorCube.Red;
                case "green":
                    return ColorCube.Green;
                case "blue":
                    return ColorCube.Blue;
                default:
                    throw new ArgumentException();
            }
        }

        public int Value { get; set; }
        public ColorCube Color { get; set; }
    }

    internal enum ColorCube
    {
        Red,
        Green,
        Blue
    }
}