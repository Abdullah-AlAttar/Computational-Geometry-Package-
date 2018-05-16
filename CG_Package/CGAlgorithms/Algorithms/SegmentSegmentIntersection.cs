using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGUtilities;

namespace CGAlgorithms.Algorithms
{
    class SegmentSegmentIntersection : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {

            if (lines.Count == 0)
                return;
            var (l1, l2) = (lines[0], lines[1]);
            if ( HelperMethods.CheckSegmentsIntersections(l1,l2))
            {
                var p = HelperMethods.GetIntersectionPoint(l1, l2);
                Console.WriteLine(p);
                outPoints.Add(p);
            }
        }
        public override string ToString() => "Check Intersection";
    }
}
