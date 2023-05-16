using AOC21.BL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Aoc21
{
    public class Solutions
    {
        public static string Input;
        public string Solution;

        #region DAY OTHER

        public decimal Day0Solve()
        {
            var data = File.ReadAllLines(Input);
            var dic = new Dictionary<int, Queue<int>> { { 1, new Queue<int>() }, { 2, new Queue<int>() } };
            // Parse data
            var player = 0;
            foreach (var item in data)
            {
                if (string.IsNullOrEmpty(item)) continue;
                if (int.TryParse(item, out var card))
                {
                    dic[player].Enqueue(card);
                }
                else
                {
                    player++;
                }
            }

            // Game
            var player1 = dic[1];
            var player2 = dic[2];
            while (player1.Any() && player2.Any())
            {
                var card1 = player1.Dequeue();
                var card2 = player2.Dequeue();
                if (card1 > card2)
                {
                    player1.Enqueue(card1);
                    player1.Enqueue(card2);
                }
                else
                {
                    player2.Enqueue(card2);
                    player2.Enqueue(card1);
                }
            }

            // Calc Score
            var winner = player1.Union(player2).ToList();

            decimal score = 0;
            for (int i = 0; i < winner.Count; i++)
            {
                score += winner[i] * (winner.Count - i);
            }

            return score;
        }

        #endregion DAY OTHER

        #region DAY 1

        public int Day1Solve()
        {
            var data = File.ReadAllLines(Input).Select(x => int.Parse(x)).ToList();

            var previousMeasure = data[0];
            var count = 0;
            for (int i = 1; i < data.Count; i++)
            {
                var actualMeasure = data[i];
                if (actualMeasure > previousMeasure)
                {
                    count++;
                }
                previousMeasure = actualMeasure;
            }

            return count;
        }

        public int Day1Solve2()
        {
            var data = File.ReadAllLines(Input).Select(x => int.Parse(x)).ToList();

            var previousMeasure = data[0] + data[1] + data[2];
            var count = 0;
            for (int i = 1; i < data.Count - 2; i++)
            {
                var actualMeasure = data[i] + data[i + 1] + data[i + 2];
                if (actualMeasure > previousMeasure)
                {
                    count++;
                }
                previousMeasure = actualMeasure;
            }

            return count;
        }

        #endregion DAY 1

        #region DAY 2

        public int Day2Solve()
        {
            var data = File.ReadAllLines(Input);

            var submarine = new Submarine();

            foreach (var line in data)
            {
                var instruction = line.Split(' ');
                var direction = instruction[0];
                var number = int.Parse(instruction[1]);

                submarine.Move(direction, number);
            }

            return submarine.CurrentPosition.X * submarine.CurrentPosition.Y;
        }

        #endregion DAY 2

        #region DAY 3

        public decimal Day3Solve()
        {
            var data = File.ReadAllLines(Input);

            var oxygen = data.ToArray();
            var CO2 = data.ToArray();

            oxygen = GetOxygenRating(oxygen, 0);
            CO2 = GetCO2Rating(CO2, 0);

            return Convert.ToInt32(oxygen[0], 2) * Convert.ToInt32(CO2[0], 2);
        }

        private string[] GetCO2Rating(string[] data, int position)
        {
            if (data.Length == 1)
            {
                return data;
            }

            var mostCommon = GetMostCommonValueAtindex(data, position) == '1' ? '0' : '1';

            data = data.Where(c => c.ElementAt(position) == mostCommon).ToArray();

            return GetCO2Rating(data, position + 1);
        }

        private string[] GetOxygenRating(string[] data, int position)
        {
            if (data.Length == 1)
            {
                return data;
            }

            var mostCommon = GetMostCommonValueAtindex(data, position);

            data = data.Where(c => c.ElementAt(position) == mostCommon).ToArray();

            return GetOxygenRating(data, position + 1);
        }

        public char GetMostCommonValueAtindex(string[] data, int position)
        {
            var one = 0;
            var zero = 0;
            for (int row = 0; row < data.Length; row++)
            {
                if (data[row][position] == '1')
                {
                    one++;
                }
                else
                {
                    zero++;
                }
            }
            return one >= zero ? '1' : '0';
        }

        #endregion DAY 3

        #region DAY 4

        public int Day4Solve()
        {
            var data = File.ReadAllLines(Input);

            // Parse data
            var numbers = data[0].Split(',').Select(n => int.Parse(n)).ToArray();

            // Get Cards
            var card = new Card();
            var cards = new List<Card>();
            foreach (var line in data.Skip(2))
            {
                if (!string.IsNullOrEmpty(line))
                {
                    card.AddRow(line);
                    continue;
                }
                card = new Card();
                cards.Add(card);
            }

            var winner = new Card();
            foreach (var drawNumber in numbers)
            {
                // Mark Cards
                MarkCards(cards, drawNumber);

                // Check winner
                var winners = GetWinner(cards);

                if (winners.Any())
                {
                    cards = cards.Except(winners).ToList();
                    winner = winners.FirstOrDefault();
                    winner.WinNumber = drawNumber;
                    if (!cards.Any())
                    {
                        break;
                    }
                }
            }

            //Calc Score
            return winner.CalcScore();
        }

        private List<Card> GetWinner(List<Card> cards)
        {
            return cards.Where(c => c.AmIWinner()).ToList();
        }

        private void MarkCards(List<Card> cards, int drawNumber)
        {
            foreach (var card in cards)
            {
                card.Play(drawNumber);
            }
        }

        #endregion DAY 4

        #region DAY 5

        public int Day5Solve()
        {
            var data = File.ReadAllLines(Input);

            // Parse data
            var coordinates = new Dictionary<Coordinates, int>();
            foreach (var line in data)
            {
                var points = line.Split(new string[] { "->" }, StringSplitOptions.RemoveEmptyEntries);
                var left = points.First().Split(',');
                var right = points.Last().Split(',');
                var from = new Coordinates(int.Parse(left.First()), int.Parse(left.Last()));
                var to = new Coordinates(int.Parse(right.First()), int.Parse(right.Last()));

                MarkPoints(from, to, coordinates);
            }

            return coordinates.Count(c => c.Value >= 2);
        }

        internal void MarkPoints(Coordinates from, Coordinates to, Dictionary<Coordinates, int> coordinates)
        {
            var x1 = from.X;
            var y1 = from.Y;

            var x2 = to.X;
            var y2 = to.Y;

            if (x1 == x2)
            {
                for (int i = y1 > y2 ? y2 : y1, counter = 0; counter <= Math.Abs(y1 - y2); i++, counter++)
                {
                    var coordinate = new Coordinates(x1, i);
                    if (!coordinates.ContainsKey(coordinate))
                    {
                        coordinates.Add(coordinate, 0);
                    }
                    coordinates[coordinate]++;
                }
            }
            else if (y1 == y2)
            {
                for (int i = x1 > x2 ? x2 : x1, counter = 0; counter <= Math.Abs(x1 - x2); i++, counter++)
                {
                    var coordinate = new Coordinates(i, y1);
                    if (!coordinates.ContainsKey(coordinate))
                    {
                        coordinates.Add(coordinate, 0);
                    }
                    coordinates[coordinate]++;
                }
            }
            else
            {
                var xs = new List<int>();
                var ys = new List<int>();
                for (int i = x1, counter = 0; counter <= Math.Abs(x1 - x2); i = x1 > x2 ? i - 1 : i + 1, counter++)
                {
                    xs.Add(i);
                }
                for (int i = y1, counter = 0; counter <= Math.Abs(y1 - y2); i = y1 > y2 ? i - 1 : i + 1, counter++)
                {
                    ys.Add(i);
                }
                for (int i = 0; i < xs.Count; i++)
                {
                    var coordinate = new Coordinates(xs[i], ys[i]);
                    if (!coordinates.ContainsKey(coordinate))
                    {
                        coordinates.Add(coordinate, 0);
                    }
                    coordinates[coordinate]++;
                }
            }
        }

        #endregion DAY 5

        #region DAY 6

        public decimal Day6Solve()
        {
            var data = File.ReadAllLines(Input).FirstOrDefault().Split(',').Select(x => int.Parse(x)).GroupBy(x => x).ToList();

            var lanternfishes = new Dictionary<int, decimal>();

            // Parse data
            for (int i = 0; i <= 8; i++)
            {
                var fishes = 0;
                if (data.Any(x => x.Key == i))
                {
                    fishes = data.FirstOrDefault(k => k.Key == i).Count();
                }
                lanternfishes.Add(i, fishes);
            }

            var day = 0;
            while (day++ < 256)
            {
                var initial0 = lanternfishes[0];
                for (int i = 0; i < 8; i++)
                {
                    if (i == 6)
                    {
                        lanternfishes[6] = initial0 + lanternfishes[7];
                        continue;
                    }
                    lanternfishes[i] = lanternfishes[i + 1];
                }
                lanternfishes[8] = initial0;
            }

            return lanternfishes.Sum(f => f.Value);
        }

        #endregion DAY 6

        #region DAY 7

        public decimal Day7Solve()
        {
            var data = File.ReadAllLines(Input).FirstOrDefault().Split(',').Select(x => int.Parse(x));

            var minPosition = data.Min();
            var maxPosition = data.Max();

            decimal fuel = int.MaxValue;
            for (int i = minPosition; i < maxPosition; i++)
            {
                var currFuel = CalcFuel(data, i);
                fuel = currFuel < fuel ? currFuel : fuel;
            }

            return fuel;
        }

        private decimal CalcFuel(IEnumerable<int> crabs, int index)
        {
            decimal fuel = 0;
            foreach (var crab in crabs)
            {
                fuel += CalcRecursive(Math.Abs(crab - index));
            }
            return fuel;
        }

        private decimal CalcRecursive(int index)
        {
            if (index <= 1)
            {
                return index;
            }
            return index + CalcRecursive(index - 1);
        }

        #endregion DAY 7

        #region DAY 8

        public decimal Day8Solve()
        {
            var data = File.ReadAllLines(Input);

            return data.Sum(x => SignalOutputValue(
                x.Split('|').First().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries),
                x.Split('|').Last().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)));
        }

        private decimal SignalOutputValue(string[] ten, string[] four)
        {
            var decoded = new Dictionary<int, string>
            {
                { 1, ten.FirstOrDefault(x => x.Length == 2) },
                { 4, ten.FirstOrDefault(x => x.Length == 4) },
                { 7, ten.FirstOrDefault(x => x.Length == 3) },
                { 8, ten.FirstOrDefault(x => x.Length == 7) }
            };

            var fiveLength = ten.Where(x => x.Length == 5);

            // Contains every chars of 1
            decoded.Add(3, fiveLength.FirstOrDefault(x => decoded[1].All(c => x.Contains(c))));

            foreach (var item in fiveLength.Except(new string[] { decoded[3] }))
            {
                // Common Letters
                var commonLetters = item.Where(c => decoded[4].Contains(c));
                if (commonLetters.Count() == 3)
                {
                    // Contains 3 chars from 4
                    decoded.Add(5, item);
                }
                else
                {
                    // The left one
                    decoded.Add(2, item);
                }
            }

            var sixLength = ten.Where(x => x.Length == 6);
            // Does not contains all from 7
            decoded.Add(6, sixLength.FirstOrDefault(x => !decoded[7].All(c => x.Contains(c))));
            // Middle char is the only char common between 2 - 4 - 5
            var middleChar = decoded[2].Where(c => decoded[4].Contains(c)).FirstOrDefault(c => decoded[5].Contains(c));
            // Contains every chars equals to 8 except the middle one
            decoded.Add(0, sixLength.FirstOrDefault(x => decoded[8].Except(new char[] { middleChar }).All(c => x.Contains(c))));
            // The left one
            decoded.Add(9, sixLength.Except(new string[] { decoded[6], decoded[0] }).First());

            var outputValue = new StringBuilder();
            foreach (var item in four)
            {
                outputValue.Append(decoded.FirstOrDefault(v => v.Value.Length == item.Length && item.All(c => v.Value.Contains(c))).Key);
            }

            return decimal.Parse(outputValue.ToString());
        }

        #endregion DAY 8

        #region DAY 9

        public decimal Day9Solve()
        {
            var data = File.ReadAllLines(Input);

            var lowPoints = AddPoints(data).Where(l => l.IsLowPoint);

            return lowPoints.OrderByDescending(p => p.Basin.Count).Take(3).Select(p => p.Basin.Count).Aggregate((a, x) => a * x);
        }

        private List<Point> AddPoints(string[] data)
        {
            var points = new List<Point>();
            for (int row = 0; row < data.Length; row++)
            {
                var y = row;
                for (int col = 0; col < data[0].Length; col++)
                {
                    var x = col;
                    points.Add(new Point(data, y, x));
                }
            }
            return points;
        }

        #endregion DAY 9

        #region DAY 10

        public decimal Day10Solve()
        {
            var data = File.ReadAllLines(Input);
            var dic = new Dictionary<char, int>
            {
                { '(', 1 },
                { '[', 2 },
                { '{', 3 },
                { '<', 4 }
            };

            var scores = new List<decimal>();
            foreach (var line in data)
            {
                var chars = new Stack<char>();
                var invalidChar = GetInvalidChar(line, chars);
                if (invalidChar == '?')
                {
                    decimal score = 0;
                    while (chars.Any())
                    {
                        score = (score * 5) + dic[chars.Pop()];
                    }
                    scores.Add(score);
                }
            }
            return scores.OrderBy(x => x).ElementAt(scores.Count / 2);
        }

        private char GetInvalidChar(string line, Stack<char> chars)
        {
            var dic = new Dictionary<char, char>
            {
                { '(', ')' },
                { '[', ']' },
                { '<', '>' },
                { '{', '}' }
            };

            foreach (var c in line)
            {
                if (dic.ContainsKey(c))
                {
                    chars.Push(c);
                    continue;
                }

                var open = chars.Pop();
                if (c != dic[open])
                {
                    return c;
                }
            }
            return '?';
        }

        #endregion DAY 10

        #region DAY 11

        public decimal Day11Solve()
        {
            var data = File.ReadAllLines(Input);

            new Sea(data);

            decimal step = 0;

            while (step++ >= 0)
            {
                PerformOctopusSteps();
                if (Sea.OctopusList.All(o => o.HasFlashed))
                {
                    return step;
                }
                Sea.OctopusList.ForEach(o => { o.HasFlashed = false; });
            }
            return 0;
        }

        private decimal PerformOctopusSteps()
        {
            decimal flashes = 0;
            foreach (var octopus in Sea.OctopusList)
            {
                if (!octopus.HasFlashed)
                {
                    flashes += IncreaseEnergyLevel(octopus);
                }
            }
            return flashes;
        }

        private decimal IncreaseEnergyLevel(Octopus octopus)
        {
            decimal flashes = 0;
            octopus.IncreaseEnergyLevel();

            if (octopus.HasFlashed)
            {
                flashes++;
                var neighbours = Sea.OctopusList.ToArray().Where(o =>
                    (o.Coordinate.X == octopus.Coordinate.X || o.Coordinate.X == octopus.Coordinate.X + 1 || o.Coordinate.X == octopus.Coordinate.X - 1)
                 && (o.Coordinate.Y == octopus.Coordinate.Y || o.Coordinate.Y == octopus.Coordinate.Y + 1 || o.Coordinate.Y == octopus.Coordinate.Y - 1)
                 && !o.HasFlashed)
                    .Except(new List<Octopus> { octopus });
                foreach (var neighbour in neighbours)
                {
                    flashes += IncreaseEnergyLevel(neighbour);
                }
            }
            return flashes;
        }

        #endregion DAY 11

        #region DAY 12

        public decimal Day12Solve()
        {
            var data = File.ReadAllLines(Input);

            var caves = new PassagePathing(data);

            var result = caves.GetPaths();

            return result;
        }

        public decimal Day12Solve2()
        {
            var data = File.ReadAllLines(Input);

            var caves = new PassagePathing(data);

            var result = caves.GetPathsPart2();

            return result;
        }

        #endregion DAY 12

        #region DAY 13

        public decimal Day13Solve()
        {
            var data = File.ReadAllLines(Input);

            var index = 0;
            var dic = new Dictionary<Coordinates, bool>();

            // Parse data
            while (!string.IsNullOrEmpty(data[index]))
            {
                var currCoord = data[index].Split(',');
                dic.Add(new Coordinates(int.Parse(currCoord.First()), int.Parse(currCoord.Last())), true);
                index++;
            }

            var folds = new List<(string xy, int number)>();
            for (int i = index + 1; i < data.Length; i++)
            {
                var temp = data[i].Split(new string[] { "fold along" }, StringSplitOptions.RemoveEmptyEntries).Last().Split('=');
                folds.Add((temp.First().Trim(), int.Parse(temp.Last())));
            }

            foreach (var fold in folds)
            {
                FoldPaper(fold, ref dic);
            }

            // print answer
            PrintDic(dic);

            return 0;
        }

        private void PrintDic(Dictionary<Coordinates, bool> dic)
        {
            for (int row = 0; row <= dic.Where(d => d.Value).Max(x => x.Key.Y); row++)
            {
                var line = new StringBuilder();
                for (int col = 0; col <= dic.Where(d => d.Value).Max(x => x.Key.X); col++)
                {
                    var temp = new Coordinates(col, row);
                    line.Append(dic.ContainsKey(temp) && dic[temp] ? "#" : " ");
                }
                Console.WriteLine(line.ToString());
            }
        }

        private void FoldPaper((string xy, int number) fold, ref Dictionary<Coordinates, bool> dic)
        {
            var x = 0;
            var y = 0;
            var isToFoldUp = true;
            if (fold.xy == "x")
            {
                y = -1; x = fold.number;
                isToFoldUp = false;
            }
            else
            {
                x = -1; y = fold.number;
            }

            // Remove dots from fold line
            var keys = dic.Where(d => d.Key.X == x || d.Key.Y == y).Select(d => d.Key).ToList();

            foreach (var key in keys)
            {
                dic[key] = false;
            }

            var activeKeys = dic.Where(d => d.Value && d.Key.X > x && d.Key.Y > y).Select(d => d.Key).ToList();

            foreach (var key in activeKeys)
            {
                var mirrorCoord = new Coordinates(key.X, key.Y);
                if (isToFoldUp)
                {
                    mirrorCoord.Y = y - (key.Y - y);
                }
                else
                {
                    mirrorCoord.X = x - (key.X - x);
                }
                dic[mirrorCoord] = true;
                dic[key] = false;
            }
        }

        #endregion DAY 13

        #region DAY 14

        public decimal Day14Solve()
        {
            var data = File.ReadAllLines(Input);

            // Parse data
            var polymerTemplate = data[0];
            var rules = new Dictionary<string, string>();
            var solution = new Dictionary<string, decimal>();

            foreach (var item in data.Skip(2))
            {
                var temp = item.Split(new string[] { "->" }, StringSplitOptions.RemoveEmptyEntries);
                var key = temp.First().Trim();
                rules.Add(key, temp.Last().Trim());
                solution.Add(key, 0);
            }

            for (int i = 0; i < polymerTemplate.Length - 1; i++)
            {
                var pair = polymerTemplate.Substring(i, 2);
                solution[pair]++;
            }

            // Build string
            for (int i = 0; i < 40; i++)
            {
                var pairs = solution.Where(s => s.Value > 0).ToList();
                ProducePairs(rules, solution, pairs);
            }

            var dic = new Dictionary<char, decimal>();

            foreach (var item in solution)
            {
                for (int i = 0; i < 2; i++)
                {
                    var c = item.Key[i];
                    if (!dic.ContainsKey(c))
                    {
                        dic.Add(c, 0);
                    }
                    dic[c] += item.Value;
                }
            }

            var group = dic.OrderByDescending(x => x.Value);
            return Math.Round((group.First().Value - group.Last().Value) / 2, 0) + 1;
        }

        private void ProducePairs(Dictionary<string, string> rules, Dictionary<string, decimal> solution, List<KeyValuePair<string, decimal>> pairs)
        {
            foreach (var pair in pairs)
            {
                var left = pair.Key[0];
                var rigth = pair.Key[1];
                var middle = rules[pair.Key];

                solution[left + middle] += pair.Value;
                solution[middle + rigth] += pair.Value;
                solution[pair.Key] -= pair.Value;
            }
        }

        #endregion DAY 14

        #region DAY 15

        public decimal Day15Solve()
        {
            var data = File.ReadAllLines(Input);

            var fullMap = new Cave(data, true);

            var startChiton = Cave.Map.FirstOrDefault(c => c.Node.X == 0 && c.Node.Y == 0);

            var dic = new Dictionary<string, Nodes>();
            foreach (var item in Cave.Map)
            {
                dic.Add($"X{item.Node.X}Y{item.Node.Y}", item);
            }
            var minorValue = new Dijkstras().GetMinDistance(startChiton.Node, dic);

            return minorValue;
        }

        private decimal FindBestPath(Nodes startChiton)
        {
            if (Cave.LastPosition.X == startChiton.Node.X
            && Cave.LastPosition.Y == startChiton.Node.Y)
            {
                return startChiton.Weight;
            }

            var rightChiton = Cave.Map.FirstOrDefault(
                    c => c.Node.X == startChiton.Node.X + 1
                    && c.Node.Y == startChiton.Node.Y);

            decimal rightValue = 0;
            if (rightChiton != null)
            {
                rightValue = FindBestPath(rightChiton) + rightChiton.Weight;
            }

            var downChiton = Cave.Map.FirstOrDefault(c =>
                               c.Node.X == startChiton.Node.X
                               && c.Node.Y == startChiton.Node.Y + 1);

            decimal downValue = 0;
            if (downChiton != null)
            {
                downValue = FindBestPath(downChiton) + downChiton.Weight;
            }

            // first line which is minor (left / right)
            if (rightValue < downValue)
            {
                return rightValue;
            }
            return downValue;

            // x / y ... x+1,y(RIGHT) / x,y+1(DOWN),

            // (Maybe optional) / x, y-1 (UP), / x-1,y (LEFT)
        }

        #endregion DAY 15

        #region DAY 16

        public decimal Day16Solve()
        {
            var data = File.ReadAllLines(Input).First();

            var binNumber = string.Join(string.Empty, data.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));

            Package pck = new Package(binNumber);

            return pck.GetSumOfVersions();
        }

        public decimal Day16Solve2()
        {
            var data = File.ReadAllLines(Input).First();

            var binNumber = string.Join(string.Empty, data.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));

            Package pck = new Package(binNumber);

            return pck.Value;
        }

        #endregion DAY 16

        #region DAY 17

        public decimal Day17Solve()
        {
            var data = File.ReadAllLines(Input).First();

            /* TEST */
            var area = new TrickShot(20, 30, -10, -5);
            ///* REAL */ var area = new TrickShot(111,161,-154,-101);

            return area.GetHighestY();
        }

        public decimal Day17Solve2()
        {
            var data = File.ReadAllLines(Input).First();

            ///* TEST */ var area = new TrickShot(20, 30, -10, -5);
            /* REAL */
            var area = new TrickShot(111, 161, -154, -101);

            return area.GetAllInitialVelocityValues();
        }

        #endregion DAY 17


        #region DAY 18

        public decimal Day18Solve()
        {
            var data = File.ReadAllLines(Input);

            var snailfish = new SnailfishHomework(data);

            snailfish.DoHomework();

            return snailfish.GetMagnitude();
        }

        public decimal Day18Solve2()
        {
            var data = File.ReadAllLines(Input);

            var snailfish = new SnailfishHomework(data);

            return snailfish.GetLargestMagnitude();
        }

        #endregion DAY 18

        public Solutions(bool isTest = false)
        {
            Input = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), isTest ? "input-test.txt" : "input.txt");
            Solution = Day18Solve2().ToString();
        }
    }
}