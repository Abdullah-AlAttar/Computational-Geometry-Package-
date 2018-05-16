using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGUtilities;

namespace CGAlgorithms.Algorithms
{
    class SegmentPolygonIntersection : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {

            if (lines.Count == 0 || polygons.Count ==0)
                return;
            var (p, l) = (polygons[0], lines[0]);

            foreach(var line in p.lines)
            {
                if (HelperMethods.CheckSegmentsIntersections(line,l))
                {
                    var intersectionPoint = HelperMethods.GetIntersectionPoint(line,l);
                    outPoints.Add(intersectionPoint);
                }
            }
        }

        public override string ToString() => "Line Polygon Intersection";
    }
}
