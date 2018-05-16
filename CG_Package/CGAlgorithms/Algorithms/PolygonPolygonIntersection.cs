using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGUtilities;

namespace CGAlgorithms.Algorithms
{
    class PolygonPolygonIntersection : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            foreach(Line l1 in polygons[0].lines)
                foreach(Line l2 in polygons[1].lines)
                    if (HelperMethods.CheckSegmentsIntersections(l1,l2))
                        outPoints.Add(HelperMethods.GetIntersectionPoint(l1, l2));
                           
                    
        }

        public override string ToString()
        {
            return "PolygonPolygonIntersection";
        }
    }
}
