using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aoc23.BL
{
    internal class DistressSignal
    {
        private List<PacketPair> _packets;
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

        private List<PacketPair> InitPackets(string[] data)
        {
            var list = new List<PacketPair>();
            for (int i = 0; i < data.Length - 1; i += 3)
            {
                var l = i;
                var r = i + 1;
                list.Add(new PacketPair(data[l], data[r]));
            }
            return list;
        }

        internal decimal GetSumOfPacketsInOrder() => _packetMap.Where(k => k.Value).Sum(k => k.Key);

        internal decimal GetDecoderKey()
        {
            var packets = new List<Packet>();

            var twoPacket = new Packet("[[2]]");
            var sixPacket = new Packet("[[6]]");

            foreach (var pair in _packets)
            {
                packets.Add(pair.L);
                packets.Add(pair.R);
            }

            // Adding divider packets
            packets.Add(twoPacket);
            packets.Add(sixPacket);

            // Uses the CompareTo overrided method
            packets.Sort();

            return (packets.IndexOf(twoPacket) + 1) * (packets.IndexOf(sixPacket) + 1);
        }

        private class PacketPair
        {
            public PacketPair(string left, string right)
            {
                this.L = new Packet(left);
                this.R = new Packet(right);
            }

            public Packet L { get; set; }
            public Packet R { get; set; }
            public bool IsRightOrder => L.CompareTo(R) != 1;
        }

        private class Packet : IComparable<Packet>
        {
            public Packet(string signal)
            {
                var tempSignal = int.TryParse(signal, out var tempValue) ? $"[{tempValue}]" : signal;
                this.Signal = tempSignal;
            }

            public string Signal { get; set; }

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

            /*
             * IComparable.CompareTo(Object) Method
             *  <0   This instance precedes obj in the sort order.
             *  0    This instance occurs in the same position in the sort order as obj.
             *  >0   This instance follows obj in the sort order.
             */

            public int CompareTo(Packet other)
            {
                var LElements = GetPairs(this.Signal);
                var RElements = GetPairs(other.Signal);

                // Left packet run out of elements first
                if (!LElements.Any() && RElements.Any())
                {
                    return -1;
                }

                for (int i = 0; i < LElements.Count; i++)
                {
                    if (i == RElements.Count) // Right packet run out of elements
                    {
                        return 1;
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
                        return lValue < rValue ? -1 : 1;
                    }
                    else
                    {
                        // both values are lists
                        // exactly one value is an integer
                        var nestedComparison = new Packet(LElements[i]).CompareTo(new Packet(RElements[i]));
                        switch (nestedComparison)
                        {
                            case -1:
                                return -1;

                            case 1:
                                return 1;
                        }
                        // case 0, continue the comparison
                    }
                }

                // After all the compares, Left packet has less elements
                if (LElements.Count < RElements.Count)
                {
                    return -1;
                }

                return 0; // equals
            }
        }
    }
}