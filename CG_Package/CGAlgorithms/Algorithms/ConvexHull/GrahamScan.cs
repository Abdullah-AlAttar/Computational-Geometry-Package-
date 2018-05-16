using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using MathNet.Spatial.Euclidean;
using CGUtilities.DataStructures;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class GrahamScan : Algorithm
    {

        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            if (points.Count == 1) { outPoints.Add(points[0]); return; }
            var (minPoint, idx) = HelperMethods.GetMinPoint(points, 'y');
            var minVec = new Vector2D(new Point(minPoint.X + 1234, minPoint.Y));
            points.Sort((a, b) =>
            {
                if (HelperMethods.CheckTurn(new Line(minPoint, a), b) == Enums.TurnType.Colinear )
                    return Point.GetSqrDistance(a, minPoint).CompareTo(Point.GetSqrDistance(b, minPoint));


                return minVec.AngleTo(new Vector2D(minPoint.VectorTo(a))).Degrees
                .CompareTo(minVec.AngleTo(new Vector2D(minPoint.VectorTo(b))).Degrees);
            });
            for (int i = 1; i < points.Count - 1; ++i)
            {
                if (HelperMethods.CheckTurn(new Line(points[0], points[i]), points[i + 1]) == Enums.TurnType.Colinear)
                {
                    points.RemoveAt(i--);
                }
            }
            if (points.Count <= 3) { outPoints.AddRange(points); return; }

            Stack<Point> st = new Stack<Point>();
            st.PushRange(points.GetRange(0, 3));
            for (int i = 3; i < points.Count; ++i)
            {
                while (HelperMethods.CheckTurn(new Line(st.GetSecondTop(), st.Peek()), points[i])
                    != Enums.TurnType.Left)
                    st.Pop();
                st.Push(points[i]);
            }
            outPoints.AddRange(st);
        }

        public override string ToString()
        {
            return "Convex Hull - Graham Scan";
        }
    }
}
