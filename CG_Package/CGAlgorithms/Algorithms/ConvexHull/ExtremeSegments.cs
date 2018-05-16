using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class ExtremeSegments : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            if (points.Count == 1)
                outPoints.Add(points[0]);
            // no 
            bool[] vis = new bool[points.Count];

            for (int i = 0; i < points.Count; ++i)
            {
                for (int j = 0; j < points.Count; ++j)
                {
                    if (i != j  && !vis[i])
                    {
                        if (HelperMethods.IsSupportLine(new Line(points[i], points[j]),points))
                        {
                            //outPoints.Add(points[i]);
                            vis[i] = true;
                            vis[j] = true;
                            //outPoints.Add(points[j]);
                            
                        }
                    }
                }
            }
            for (int i = 0; i < points.Count; ++i)
                if (vis[i])
                    outPoints.Add(points[i]);
        }

        public override string ToString() => "Convex Hull - Extreme Segments";
    }
}
