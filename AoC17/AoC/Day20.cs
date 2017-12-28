namespace AoC17
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Day20 : Master
    {
        public void Run()
        {
            var dic = this.GetAllParticles(this.Input);

            while (!AreAllInSameDirection(dic))
            {
                this.Tick(ref dic);
            }

            // Part 1
            var minDistance = dic.Values.Min(p => p.Position.DistanceToZero);
            this.Output1 = dic.First(d => d.Value.Position.DistanceToZero == minDistance).Key;

            // Part 2
            dic = this.GetAllParticles(this.Input);

            while (!AreAllInSameDirection(dic))
            {
                this.Tick(ref dic);

                this.RemoveCollisions(ref dic);
            }

            this.Output2 = dic.Count;
        }

        private void RemoveCollisions(ref Dictionary<int, Particle> dic)
        {
            var collisions = dic.GroupBy(d => new { d.Value.Position.X, d.Value.Position.Y, d.Value.Position.Z }).Where(k => k.Count() > 1).SelectMany(g => g.Select(k => k.Key)).Distinct();
            foreach (var key in collisions)
            {
                dic.Remove(key);
            }
        }

        private bool AreAllInSameDirection(Dictionary<int, Particle> dic)
        {
            return dic.All(p => (p.Value.Position.X >= 0 && p.Value.Velocity.X >= 0 && p.Value.Acceleration.X >= 0)
                             || (p.Value.Position.X <= 0 && p.Value.Velocity.X <= 0 && p.Value.Acceleration.X <= 0));
        }

        private void Tick(ref Dictionary<int, Particle> dic)
        {
            foreach (var item in dic)
            {
                item.Value.Velocity.X += item.Value.Acceleration.X;
                item.Value.Velocity.Y += item.Value.Acceleration.Y;
                item.Value.Velocity.Z += item.Value.Acceleration.Z;
                item.Value.Position.X += item.Value.Velocity.X;
                item.Value.Position.Y += item.Value.Velocity.Y;
                item.Value.Position.Z += item.Value.Velocity.Z;
            }
        }

        private Dictionary<int, Particle> GetAllParticles(List<string> buffer)
        {
            var dic = new Dictionary<int, Particle>();
            for (int i = 0; i < buffer.Count; i++)
            {
                var particleProperties = buffer[i].Split(", ", StringSplitOptions.None);
                var p = this.GetCoordinateValues(particleProperties, 0);
                var v = this.GetCoordinateValues(particleProperties, 1);
                var a = this.GetCoordinateValues(particleProperties, 2);

                dic.Add(i, new Particle(p, v, a));
            }
            return dic;
        }

        private long[] GetCoordinateValues(string[] particleProperties, int position)
        {
            var indexLeft = particleProperties[position].IndexOf('<');
            var indexRight = particleProperties[position].IndexOf('>');
            return particleProperties[position].Substring(indexLeft + 1, indexRight - indexLeft - 1).Split(',').Select(e => long.Parse(e)).ToArray();
        }
    }

    public class Coordinates
    {
        public Coordinates(long[] xyz)
        {
            this.X = xyz[0];
            this.Y = xyz[1];
            this.Z = xyz[2];
        }

        public long X { get; set; }
        public long Y { get; set; }
        public long Z { get; set; }

        public long DistanceToZero { get { return Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z); } }
    }

    public class Particle
    {
        public Particle(long[] p, long[] v, long[] a)
        {
            this.Position = new Coordinates(p);
            this.Velocity = new Coordinates(v);
            this.Acceleration = new Coordinates(a);
        }

        public Coordinates Velocity { get; set; }
        public Coordinates Position { get; set; }
        public Coordinates Acceleration { get; set; }
    }
}