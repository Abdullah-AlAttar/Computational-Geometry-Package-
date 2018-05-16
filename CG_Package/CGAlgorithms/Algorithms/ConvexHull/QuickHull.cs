using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C5;
namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class QuickHull : Algorithm
    {
        public static int cmp(Point a, Point b)
        {
            return a.X.CompareTo(b.X);
        }
        public List<Line> DrawCircle(Point c, double r,double step = 0.2)
        {
            var pts = new List<Point>();

            for (double angle = 0 ; angle < 2*Math.PI ; angle += step)
            {
                var pX = c.X + r * Math.Cos(angle);
                var pY = c.Y + r * Math.Sin(angle);
                pts.Add(new Point(pX, pY));
            }
            var lines = new List<Line>();
            for(int i=0;i<pts.Count-1;++i)
            {
                lines.Add(new Line(pts[i], pts[i+1]));
            }
            lines.Add(new Line(pts[pts.Count - 1], pts[0]));
            return lines;
        }
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            foreach (var p in points)
            {
                var tmp = DrawCircle(p, 50);
                outLines.AddRange(tmp);
            }
      
            //var set = new SortedSet<Point>();

            //var x = new C5.TreeSet<Point>(Comparer<Point>.Create((Point a, Point b) =>
            //{
            //    return a.X.CompareTo(b.X);
            //}));



            //var dcel = new DCEL();


            //List<Point> pts = new List<Point>();
            //foreach (var line in polygons[0].lines)
            //    pts.Add(line.Start);
            ////var (minPt, minIdx) =HelperMethods.GetMinPoint(pts, 'y');
            ////var (maxPt, maxIdx) = HelperMethods.GetMaxPoint(pts, 'y');
            ////outPoints.Add(minPt);
            ////outPoints.Add(maxPt);
            //DCEL dcel = new DCEL(pts);

            //var (maxY, minY) = dcel.GetMaxMinY();

            //outPoints.Add(maxY.Position);
            //outPoints.Add(minY.Position);

            //var leftChain = dcel.GetLeftChain();

            //for (int i = 0; i < leftChain.Count - 1; ++i)
            //{
            //    outLines.Add(new Line(leftChain[i].Position, leftChain[i + 1].Position));
            //}

            //var sorted = dcel.Sort();
            //for(int i =0;i<sorted.Count-1;++i)
            //{
            //    outLines.Add(new Line(sorted[i].Position, sorted[i + 1].Position));
            //}
            //foreach (var hf in dcel.HalfEdges)
            //{
            //    Console.WriteLine(hf.Origin + " " + hf.Next.Origin.ID );
            //}

            //var curHalfEdge = dcel.HalfEdges[0];
            //var start = curHalfEdge.Origin.Position;
            //var next = curHalfEdge.Previous.Origin.Position;

            //while (!start.Equals(next))
            //{
            //    outLines.Add(new Line(start, next));
            //    curHalfEdge = curHalfEdge.Previous;
            //    next = curHalfEdge.Origin.Position;
            //}



        }

        public override string ToString()
        {
            return "Convex Hull - Quick Hull";
        }
    }
}
