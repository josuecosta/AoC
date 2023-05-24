using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aoc22.BL
{
    internal class DistressSignal
    {
        private List<Packet> _packets;
        private Dictionary<int, bool> _packetMap;

        public DistressSignal(string[] data)
        {
            _packets = InitPackets(data);
            _packetMap = new Dictionary<int, bool>();
            for (int i = 0; i < _packets.Count; i++)
            {
                _packetMap.Add(i + 1, _packets[i].IsRightOrder);
            }
        }

        private List<Packet> InitPackets(string[] data)
        {
            var list = new List<Packet>();
            for (int i = 0; i < data.Length - 1; i += 3)
            {
                var l = i;
                var r = i + 1;
                list.Add(new Packet(data[l], data[r]));
            }
            return list;
        }

        internal decimal GetSumOfPacketsInOrder() => _packetMap.Where(k => k.Value).Sum(k => k.Key);

        private class Packet
        {
            public Packet(string left, string right)
            {
                var lTemp = int.TryParse(left, out var lValue) ? $"[{lValue}]" : left;
                var rTemp = int.TryParse(right, out var rValue) ? $"[{rValue}]" : right;

                this.L = lTemp;
                this.R = rTemp;
            }

            public string L { get; set; }
            public string R { get; set; }
            public bool IsRightOrder => IsCorrectOrder() != 0;

            private int IsCorrectOrder()
            {
                var LElements = GetPairs(L);
                var RElements = GetPairs(R);

                if (!LElements.Any() && RElements.Any())
                {
                    return 1;
                }

                for (int i = 0; i < LElements.Count; i++)
                {
                    if (i == RElements.Count) // R side doesn't have more elements
                    {
                        return 0;
                    }

                    // both values are integers
                    var lIsInt = int.TryParse(LElements[i], out var lValue);
                    var rIsInt = int.TryParse(RElements[i], out var rValue);

                    if (lIsInt && rIsInt)
                    {
                        if (lValue == rValue)
                        {
                            continue;
                        }
                        return lValue < rValue ? 1 : 0;
                    }
                    else
                    {
                        // both values are lists
                        // exactly one value is an integer
                        var nestedComparison = new Packet(LElements[i], RElements[i]).IsCorrectOrder();
                        switch (nestedComparison)
                        {
                            case 0:
                                return 0;

                            case 1:
                                return 1;
                        }
                        // case -1, continue the comparison
                    }
                }

                if (LElements.Count < RElements.Count)
                {
                    return 1;
                }

                return -1; // inclonclusive
            }

            private List<string> GetPairs(string packet)
            {
                var level = -1;
                var sb = new StringBuilder();
                var list = new List<string>();
                for (int i = 0; i < packet.Length; i++)
                {
                    var c = packet[i];

                    if (level == 0 && (c == ',' || c == ']'))
                    {
                        if (sb.Length > 0)
                        {
                            list.Add(sb.ToString());
                            sb.Clear();
                        }
                    }
                    else if (level >= 0)
                    {
                        sb.Append(c);
                    }
                    level = UpdateLevel(level, c);
                }
                return list;
            }

            private static int UpdateLevel(int level, char c)
                => c == '[' ? level + 1
                 : c == ']' ? level - 1
                 : level;
        }
    }
}