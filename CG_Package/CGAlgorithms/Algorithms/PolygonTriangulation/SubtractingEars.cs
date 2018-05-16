using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGUtilities;





namespace CGAlgorithms.Algorithms.PolygonTriangulation
{
    class SubtractingEars : Algorithm
    {
        public override void Run(List<CGUtilities.Point> points, List<CGUtilities.Line> lines, List<CGUtilities.Polygon> polygons, ref List<CGUtilities.Point> outPoints, ref List<CGUtilities.Line> outLines, ref List<CGUtilities.Polygon> outPolygons)
        {
            var pts = new List<Point>();
            foreach (Line l in polygons[0].lines)
                pts.Add(l.Start);
            HelperMethods.CheckMakeCCW(ref pts);
            List<Point> ears = GetEars(pts);

            var tmpPts = new List<Point>();
            foreach (var p in pts)
                tmpPts.Add(new Point(p.X, p.Y));

            while (ears.Count != 0 && pts.Count >3)
            {
                var ear = ears[0];
                //ears.Remove()
                outLines.Add(SubtractEar(ear, ears, pts));
                pts.Remove(ear);
            }
            //outPoints.AddRange(ears);
        }
        private Line SubtractEar(Point p, List<Point> ears, List<Point> pts)
        {
            ears.Remove(p);
            int earIdx = pts.IndexOf(p);
            var (prev, next) = (pts.GetPrevious(earIdx), pts.GetNext(earIdx));
            int prevIdx = pts.IndexOf(prev);
            int nextIdx = pts.IndexOf(next);

            if (IsEar(prevIdx, pts))
            {
                if (!ears.Contains(prev))
                    ears.Add(prev);
            }
            else
            {
                if (ears.Contains(prev))
                {
                    ears.Remove(prev);
                    //Console.WriteLine("please");
                }
            }

            if (IsEar(nextIdx, pts))
            {
                if (!ears.Contains(next))
                    ears.Add(next);
            }
            else
            {
                if (ears.Contains(next))
                {
                    ears.Remove(prev);
                    Console.WriteLine("no please");
                }
            }

            return new Line(prev, next);
        }
        private List<Point> GetEars(List<Point> pts)
        {
            var res = new List<Point>();
            pts.Enumerate((item, idx) =>
            {
                if (IsEar(idx, pts))
                    res.Add(item);
            });
            return res;
        }

        private bool IsEar(int idx, List<Point> pts)
        {
            if (!IsConvex(idx, pts))
                return false;

            var (prev, next) = (pts.GetPrevious(idx), pts.GetNext(idx));

            for (int i = 0; i < pts.Count; ++i)
            {
                if (i != idx && !pts[i].Equals(prev) && !pts[i].Equals(next))
                {
                    var check = HelperMethods.PointInTriangle(pts[i], prev, pts[idx], next);
                    if (check == Enums.PointInPolygon.Inside || check == Enums.PointInPolygon.OnEdge)
                        return false;
                }
            }
            return true;
        }

        private bool IsConvex(int idx, List<Point> pts)
        {
            var (prev, next) = (pts.GetPrevious(idx), pts.GetNext(idx));

            return HelperMethods.CheckTurn(new Line(prev, pts[idx]), next) == Enums.TurnType.Left;
        }

        public override string ToString()
        {
            return "Subtracting Ears";
        }
    }
}
