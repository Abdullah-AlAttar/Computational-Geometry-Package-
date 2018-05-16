using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGUtilities;

namespace CGAlgorithms.Algorithms
{
    class PointInsideConvexPolygon : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            if (points.Count == 0 || polygons.Count == 0)
                return;
            
            int idx = 0;
            Enums.TurnType prevSide = Enums.TurnType.Left;//dummy value
            bool inside = true;
            foreach (Point p in points)
            {
                idx = 0;
                inside = true;
                foreach (Line l in polygons[0].lines)
                {
                    var curSide = HelperMethods.CheckTurn(l, p);
                    if (idx != 0)
                        if (curSide != prevSide)
                        {
                            inside = false;
                        }
                    prevSide = curSide;
                    idx++;
                }
                if (inside)
                    outPoints.Add(p);
            }
        }

        public override string ToString() => "Points Inside convex Polygon";
    }
}
