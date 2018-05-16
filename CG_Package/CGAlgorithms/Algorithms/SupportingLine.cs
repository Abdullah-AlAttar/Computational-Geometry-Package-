using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGUtilities;

namespace CGAlgorithms.Algorithms
{
    class SupportingLine : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            if (lines.Count == 0 || polygons.Count == 0)
                return;
            int idx = 0;
            Enums.TurnType prevSide = Enums.TurnType.Left;//dummy value
            bool check;
            foreach (Line l in lines)
            {
                check = true;
                foreach(Line pl in polygons[0].lines)
                {
                    var t1 = HelperMethods.CheckTurn(l, pl.Start);
                    var t2 = HelperMethods.CheckTurn(l, pl.End);
                    if (t1 != t2)
                        check = false;
                    if (idx != 0)
                        if (prevSide != t1)
                            check = false;
                    prevSide = t1;
                    idx++;
                }
                if (check)
                    outLines.Add(l);
            }
        }

        public override string ToString() => "Support Lines";
    }
}
