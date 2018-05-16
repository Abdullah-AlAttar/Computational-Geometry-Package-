using CGUtilities;
using CGUtilities.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class Incremental : Algorithm
    {

        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            if (points.Count<=3)
            {
                outPoints.AddRange(points);
                return;
            }
            //get center point
            var cp = (points[0] + points[1]) / 2.0;
            cp = (cp + points[2]) / 2.0;
            var cl = new Vector2D(cp.VectorTo(new Point(cp.X+1000,cp.Y)));
            
            var pts = new List<(double angle, int index)>();

            points.Enumerate((point, index) =>
            {
                var angle = HelperMethods.GetAngle(new Point(cp.X + 100, cp.Y), cp, point);
                pts.Add((angle, index));
            });

            var ss = new OrderedSet<(double angle, int index)>
            {
                pts[0],
                pts[1],
                pts[2]
            };

            //ss.AddMany(pts);

            for (int i = 3; i < pts.Count; ++i)
            {
                var cur = pts[i];

                //var UL = ss.DirectUpperAndLower(cur);
                //var prev = UL.Value;
                //var next = UL.Key;
                var (prev, next) = ss.DUL(cur);

                //  Outside the polygon
                if (HelperMethods.CheckTurn(new Line(points[prev.index], points[next.index]), points[cur.index]) == Enums.TurnType.Right)
                {
                    var newPrev = ss.GetPre(prev);
                    // Left Supporting Line
                    while (HelperMethods.CheckTurn(new Line(points[cur.index], points[prev.index]), points[newPrev.index]) != Enums.TurnType.Right)
                    {
                        ss.Remove(prev);
                        prev = newPrev;
                        newPrev = ss.GetPre(prev);
                    }

                    //  Right Supporting Line
                    var newNext = ss.GetNext(next);
                    while (HelperMethods.CheckTurn(new Line(points[cur.index], points[next.index]), points[newNext.index]) != Enums.TurnType.Left)
                    {
                        ss.Remove(next);
                        next = newNext;
                        newNext = ss.GetNext(next);
                    }
                    ss.Add(cur);
                }
            }
            for (int i = 0; i < ss.Count; ++i)
            {
                outPoints.Add(points[ss[i].index]);
            }
        }

        public override string ToString()
        {
            return "Convex Hull - Incremental";
        }
    }
}
