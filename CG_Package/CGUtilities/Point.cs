using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGUtilities
{
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
                              /// <summary>
                              /// The primary Point structure to be used in the CG project.
                              /// </summary>
    public class Point : ICloneable,IComparable<Point>
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        /// <summary>
        /// Creates a point structure with the given coordinates.
        /// </summary>
        /// <param name="x">The X value/</param>
        /// <param name="y">The Y value.</param>
        public Point(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Gets or sets the X coordinate.
        /// </summary>
        public double X
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Y coordinate.
        /// </summary>
        public double Y
        {
            get;
            set;
        }

        public static Point Identity { get { return new Point(0, 0); } }
       
        public override bool Equals(object obj)
        {
            if (obj is Point)
            {
                Point p = (Point)obj;
                return Math.Abs(this.X - p.X) < Constants.Epsilon && Math.Abs(this.Y - p.Y) < Constants.Epsilon;
            }
            return false;
        }
        public static double GetSqrDistance(Point a, Point b) => Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2);

        public static Point operator /(Point p, double d) => new Point(p.X / d, p.Y / d);

        public static Point operator -(Point p, Point d) => new Point(p.X - d.X, p.Y - d.Y);
        public static Point operator +(Point p, Point d) => new Point(p.X + d.X, p.Y + d.Y);

        public Point VectorTo(Point to) => new Point(to.X - this.X, to.Y - this.Y);

        public double Magnitude => Math.Sqrt(this.X * this.X + this.Y * this.Y);

        public Point Normalize()
        {
            double mag = this.Magnitude;
            Point ans = this / mag;
            return ans;
        }

        /// <summary>
        /// Make a new instance of Point
        /// </summary>
        /// <returns>The new instance of Point</returns>
        public object Clone() => new Point(X, Y);
        public override string ToString()
        {
            return "(" + this.X + "," + this.Y +")";
        }
        public int CompareTo(Point other) => this.Y.CompareTo(other.Y);
    }
}
