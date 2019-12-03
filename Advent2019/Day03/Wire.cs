using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2019.Day03
{
    public class Wire
    {
        private static IDictionary<string, Func<XY, XY>> moves;
        private readonly IList<XY> xys = new List<XY>();
        private readonly IDictionary<XY, int> stepNumbers = new Dictionary<XY, int>();
        private int stepNumber = 1;
        private XY current = XY.Origin;

        static Wire()
        {
            moves = new Dictionary<string, Func<XY, XY>>()
            {
                { "U", xy => xy.Up() },
                { "D", xy => xy.Down() },
                { "R", xy => xy.Right() },
                { "L", xy => xy.Left() }
            };
        }

        public Wire Move(string move)
        {
            foreach (var xy in MakeXYs(move))
            {
                xys.Add(xy);

                if (!this.stepNumbers.ContainsKey(xy))
                {
                    this.stepNumbers.Add(xy, this.stepNumber);
                }

                this.stepNumber++;
            }

            return this;
        }

        private IEnumerable<XY> MakeXYs(string move)
        {
            var (direction, distance) = Parse(move);

            foreach (var _ in Enumerable.Range(0, distance))
            {
                this.current = direction(this.current);

                yield return current;
            }
        }

        public IEnumerable<Tuple<XY, int?>> StepNumbers(IEnumerable<XY> xys)
        {
            return xys.Select(xy => Tuple.Create(xy, StepNumber(xy)));
        }

        public int? StepNumber(XY xy)
        {
            return this.stepNumbers.ContainsKey(xy) ? this.stepNumbers[xy] : (int?)null;
        }

        public IEnumerable<XY> XYs { get => this.xys;  }

        public IEnumerable<XY> Intersection(Wire other)
        {
            return this.xys.Intersect(other.xys);
        }

        private Tuple<Func<XY, XY>, int> Parse(string move)
        {
            var direction = move.Substring(0, 1);

            var distance = Int32.Parse(move.Substring(1, move.Length - 1));

            return Tuple.Create(moves[direction], distance);
        }       
        
        public static Wire FromCommaSeparated(string moves)
        {
            var wire = new Wire();

            foreach (var move in moves.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries))
            {
                wire = wire.Move(move);
            }

            return wire;
        }
    }

    public static class WireExtensions
    {
        public static XY FindClosestCrossing(this Wire first, Wire second)
        {            
            var intersection = first.Intersection(second);

            var distances = intersection.Select(xy => new { XY = xy, Distance = xy.Manhattan(XY.Origin) }).ToList();

            var nearToFar = distances.OrderBy(d => d.Distance);

            return nearToFar.First().XY;
        }

        public static int FindShortestSignalDelay(this Wire first, Wire second)
        {            
            var intersection = first.Intersection(second);

            var firstCounted = first.StepNumbers(intersection);
            var secondCounted = second.StepNumbers(intersection);

            var combinedCounts = firstCounted.Join(secondCounted, f => f.Item1, s => s.Item1, (f, s) => new { XY = f.Item1, Total = f.Item2.Value + s.Item2.Value });

            return combinedCounts.OrderBy(c => c.Total).Select(c => c.Total).First();
        }
    }    
}
