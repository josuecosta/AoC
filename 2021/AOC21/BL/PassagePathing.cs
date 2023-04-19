using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC21.BL
{
    public class PassagePathing
    {
        public HashSet<string> Paths { get; set; }

        public Dictionary<string, CaveConnection> Caves { get; set; }

        public PassagePathing(string[] data)
        {
            Caves = new Dictionary<string, CaveConnection>();
            Paths = new HashSet<string>();
            ParseData(data);
        }

        private void ParseData(string[] data)
        {
            foreach (var item in data)
            {
                var caves = item.Split('-');
                var caveL = new CaveConnection(caves[0]);
                var caveR = new CaveConnection(caves[1]);
                Caves.TryAdd(caves[0], caveL);
                Caves.TryAdd(caves[1], caveR);
                Caves[caves[0]].AddConnection(Caves[caves[1]]);
                Caves[caves[1]].AddConnection(Caves[caves[0]]);
            }
        }

        internal decimal GetPaths()
        {
            var startConnection = Caves.Values.Single(c => c.IsStart);
            foreach (var node in startConnection.Connections)
            {
                FindPathsSmallCavesVisitedOnce(node, new List<CaveConnection>() { startConnection });
            }
            return Paths.Count;
        }

        internal decimal GetPathsPart2()
        {
            var startConnection = Caves.Values.Single(c => c.IsStart);
            foreach (var node in startConnection.Connections)
            {
                FindPathsSmallCavesVisitedTwice(node, new List<CaveConnection>() { startConnection });
            }
            return Paths.Count;
        }

        private void FindPathsSmallCavesVisitedTwice(CaveConnection node, List<CaveConnection> currentPath)
        {
            currentPath.Add(node);

            if (node.IsEnd)
            {
                Paths.Add(PathToString(currentPath));
                return;
            }

            var connections = node.Connections
                .Where(c =>
                    // Is big 
                    c.IsBig
                 || (
                    // OR Is small
                    !c.IsBig
                    // AND does not exist in the path
                    && (
                        !currentPath.Contains(c)
                        // OR exist but there is no duplicated small
                        || !currentPath.Where(s => !s.IsBig)
                                       .GroupBy(s => s.Name)
                                       .Any(s => s.Count() > 1)
                    )
                 )
             );

            foreach (var connection in connections)
            {
                FindPathsSmallCavesVisitedTwice(connection, currentPath.ToList());
            }
        }

        private void FindPathsSmallCavesVisitedOnce(CaveConnection node, List<CaveConnection> currentPath)
        {
            currentPath.Add(node);

            if (node.IsEnd)
            {
                Paths.Add(PathToString(currentPath));
                return;
            }

            var connections = node.Connections.Where(c => !currentPath.Contains(c) || c.IsBig);

            foreach (var connection in connections)
            {
                FindPathsSmallCavesVisitedOnce(connection, currentPath.ToList());
            }
        }

        private string PathToString(List<CaveConnection> currentPath) => string.Join(",", currentPath.Select(c => c.Name));
    }

    public class CaveConnection
    {
        public string Name { get; set; }
        public bool IsStart => Name.Equals("start");
        public bool IsEnd => Name.Equals("end");
        public bool IsBig => Regex.IsMatch(Name, "^[A-Z]+$");
        public List<CaveConnection> Connections { get; set; }

        public CaveConnection(string name)
        {
            Name = name;
            Connections = new List<CaveConnection>();
        }

        public CaveConnection this[string name]
        {
            get
            {
                return Connections.Find(c => c.Name == name);
            }
            set
            {
                Connections.Add(value);
            }
        }

        internal void AddConnection(CaveConnection neighbor)
        {
            if (IsEnd || neighbor.IsStart)
            {
                return;
            }
            Connections.Add(neighbor);
        }

        public override bool Equals(object obj)
        {
            var item = obj as CaveConnection;

            if (item == null)
            {
                return false;
            }

            return this.Name == item.Name && this.IsBig == item.IsBig;
        }

        public override int GetHashCode()
        {
            return (Name, IsBig).GetHashCode();
        }
    }
}
