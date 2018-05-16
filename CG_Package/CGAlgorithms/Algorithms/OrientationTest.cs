using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGUtilities;

namespace CGAlgorithms.Algorithms
{
    class OrientationTest : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            //var vec1 = (lines[0].End - lines[0].Start);
            //var vec2 = (points[0] - lines[0].End);
            //var sign = HelperMethods.CrossProduct(vec1, vec2);

            //if (sign > 0)
            //    outPoints.Add(points[0]);
            //else
            //    outLines.Add(lines[0]);
            if (lines.Count == 0 || points.Count == 0)
                return;
            var res = HelperMethods.CheckTurn(lines[0], points[0]);

            if(res==Enums.TurnType.Left)
                outPoints.Add(points[0]);
            else
                outLines.Add(lines[0]);


        }

        public override string ToString()
        {
            return "Check Turn";
        }
    }
}
