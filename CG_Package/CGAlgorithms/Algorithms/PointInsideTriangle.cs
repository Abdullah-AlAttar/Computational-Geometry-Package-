using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGUtilities;

namespace CGAlgorithms.Algorithms
{
    class PointInsideTriangle : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            bool check = HelperMethods.PointInTriangle(points[0],
                polygons[0].lines[0].Start,
                polygons[0].lines[1].Start,
                polygons[0].lines[2].Start) != Enums.PointInPolygon.Outside;
            if (check)
                outPoints.Add(points[0]);
        }

        public override string ToString()
        {
            return "Point Inside Triangle";
        }
    }
}
