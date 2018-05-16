using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGUtilities;
//using MathNet.Spatial.Euclidean;
namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class JarvisMarch : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            
            var (minPoint, idx) = HelperMethods.GetMinPoint(points,'x');
            HashSet<Point> res = new HashSet<Point>();
            res.Add(minPoint);
            List<Point> collinearPoints = new List<Point>();
            var cur = minPoint.Clone() as Point;
            while (true)
            {
                var nextPoint = points[0];
                for (int i = 1; i < points.Count; ++i)
                {
                    if (points[i].Equals(cur))
                        continue;
                    var turn = HelperMethods.CheckTurn(new Line(cur, nextPoint), points[i]);
                    if (turn == Enums.TurnType.Right)
                    {
                        nextPoint = points[i].Clone() as Point;
                    }
                    else if (turn == Enums.TurnType.Colinear)
                    {
                        var dst1 = Point.GetSqrDistance(cur, nextPoint);
                        var dst2 = Point.GetSqrDistance(cur, points[i]);
                        if (dst1 < dst2)
                        {
                            nextPoint = points[i];
                        }
                    }
                }
                //foreach (Point p in collinearPoints)
                //    res.Add(p);

                if (nextPoint.Equals(minPoint))
                    break;

                res.Add(nextPoint);
                cur = nextPoint.Clone() as Point;


            }
            outPoints.AddRange(res);
        }

        public override string ToString() => "Convex Hull - Jarvis March";
    } 
}
