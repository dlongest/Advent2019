using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2019.Day03
{
    public class XY
    {
        public XY(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; private set; }

        public int Y { get; set; }

        public XY Up()
        {
            return new XY(this.X, this.Y + 1);
        }

        public XY Down()
        {
            return new XY(this.X, this.Y - 1);
        }

        public XY Left()
        {
            return new XY(this.X - 1, this.Y);
        }

        public XY Right()
        {
            return new XY(this.X + 1, this.Y);
        }

        public override string ToString()
        {
            return $"({this.X}, {this.Y})";
        }

        public override bool Equals(object obj)
        {
            var w = obj as XY;

            if (w == null)
            {
                return false;
            }

            return this.X == w.X && this.Y == w.Y;
        }

        public int Manhattan(XY other)
        {
            return Math.Abs(this.X - other.X) + Math.Abs(this.Y - other.Y);
        }

        public override int GetHashCode()
        {
            return 17 * X.GetHashCode() + 11 * Y.GetHashCode();
        }

        public static XY Origin => new XY(0, 0);
    }
}
