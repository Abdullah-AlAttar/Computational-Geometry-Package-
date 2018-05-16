using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class ExtremePoints : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            bool[] vis = new bool[points.Count];
            for (int i = 0; i < points.Count; ++i)
            {
                for (int j = 0; j < points.Count; ++j)
                {
                    for (int k = 0; k < points.Count; ++k)
                    {
                        for (int l = 0; l < points.Count; ++l)
                        {
                            if (i != j && i != k && i != l && j!=k && j!=l && k!=l && !vis[i])
                            {
                                var ret = HelperMethods.PointInTriangle(points[i], points[j], points[k], points[l]);
                                if (ret == Enums.PointInPolygon.Inside ||ret == Enums.PointInPolygon.OnEdge)
                                {
                                    //outPoints.Add(points[i]);
                                    vis[i] = true;
                                }
                            }

                        }
                    }

                }
            }
            for(int i=0;i<points.Count;++i)
            {
                if (!vis[i])
                    outPoints.Add(points[i]);
            }
        }

        public override string ToString()
        {
            return "Convex Hull - Extreme Points";
        }
    }
}
