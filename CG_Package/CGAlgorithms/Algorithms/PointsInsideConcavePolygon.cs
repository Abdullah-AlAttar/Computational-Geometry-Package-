using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGUtilities;

namespace CGAlgorithms.Algorithms
{
    class PointsInsideConcavePolygon : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            if (points.Count == 0 || polygons.Count == 0)
                return;

            //foreach(Point p in points)
            //{
            //    var ray = new Line(p, new Point(10000, p.Y));
            //    var counter = 0;
            //    foreach(Line l in polygons[0].lines)
            //    {
            //        if (HelperMethods.CheckSegmentsIntersections(l, ray))
            //            counter++;
            //    }
            //    if(counter %2 ==1)
            //    {
            //        outPoints.Add(p);
            //    }
            //}
            foreach (Point p in points)
            {
                var ray = new Line(p, new Point(10000, p.Y));
                var counter = 0;
                foreach (Line l in polygons[0].lines)
                {
                    if (HelperMethods.CheckSegmentsIntersections(l, ray))
                        if (HelperMethods.CheckTurn(l, p) == Enums.TurnType.Left)
                            counter++;
                        else
                            counter--;
                }
                if (counter !=0)
                {
                    outPoints.Add(p);
                }
            }
        }

        public override string ToString() => "Points inside Concave Polygon";
    }
}
