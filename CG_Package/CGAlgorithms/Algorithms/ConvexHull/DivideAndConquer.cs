using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class DivideAndConquer : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {



            points.Sort((aa, bb) => aa.X.CompareTo(bb.X));
            //points = HelperMethods.ConvexHullGrahams(points);
            outPoints.AddRange(new HashSet<Point>(Solve(points)));
            return;
            //var res = Solve(points);
            //outPoints.AddRange(HelperMethods.ConvexHullGrahams(points));
            //return;
            List<Point> p1 = new List<Point>();
            List<Point> p2 = new List<Point>();

            foreach (var i in polygons[0].lines)
                p1.Add(i.Start);
            foreach (var i in polygons[1].lines)
                p2.Add(i.Start);

            p1 = HelperMethods.ConvexHullGrahams(p1);
            p2 = HelperMethods.ConvexHullGrahams(p2);
            int rmi = 0;
            for (int i = 1; i < p1.Count; ++i)
                if (p1[i].X > p1[rmi].X)
                    rmi = i;
            // Leftmost point in right points
            int lmi = 0;
            for (int i = 1; i < p2.Count; ++i)
                if (p2[i].X < p2[lmi].X)
                    lmi = i;
            //outPoints.AddRange(p2);
            //outPoints.Add(p2[lmi]);
            //outPoints.Add(p2[(lmi + 1) % p2.Count]);
            //return;
            var (upperLeft, upperRight) = FindUpperTangent(p1, p2, lmi, rmi);
            var (lowerLeft, lowerRight) = FindLowerTangent(p1, p2, lmi, rmi);
            List<Point> res = new List<Point>();

            outLines.Add(new Line(p1[upperLeft], p2[upperRight]));
            outLines.Add(new Line(p1[lowerLeft], p2[lowerRight]));
            var tmp = upperLeft;
            res.Add(p1[tmp]);
            while (tmp != lowerLeft)
            {
                tmp = ( tmp + 1) % p1.Count;
                res.Add(p1[tmp]);
            }
            tmp = upperRight;
            res.Add(p2[tmp]);
            while (tmp != lowerRight)
            {
                tmp = (p2.Count + tmp - 1) % p2.Count;
                res.Add(p2[tmp]);
            }

            outPoints.AddRange(res);
        }

        public List<Point> Solve(List<Point> points)
        {

            if (points.Count <= 8)
                return HelperMethods.ConvexHullGrahams(points);
            var mid = points.Count / 2;
            var left = Solve(points.GetRange(0, mid));
            var right = Solve(points.GetRange(mid, (points.Count + 1) / 2));
            //Console.WriteLine('l');
            //foreach (Point p in left)
            //    Console.Write(p + " ");
            //Console.WriteLine();
            //Console.WriteLine('R');
            //foreach (Point p in right)
            //    Console.Write(p + " ");
            return Merge(left, right);

        }

        public List<Point> Merge(List<Point> left, List<Point> right)
        {
            //Rightmost point in left points
            int rmi = 0;
            for (int i = 1; i < left.Count; ++i)
                if (left[i].X > left[rmi].X)
                    rmi = i;
            // Leftmost point in right points
            int lmi = 0;
            for (int i = 1; i < right.Count; ++i)
                if (right[i].X < right[lmi].X)
                    lmi = i;

            var (upperLeft, upperRight) = FindUpperTangent(left, right, lmi, rmi);
            var (lowerLeft, lowerRight) = FindLowerTangent(left, right, lmi, rmi);
            List<Point> res = new List<Point>();

            var tmp = upperLeft;
            res.Add(left[tmp]);
            while (tmp != lowerLeft)
            {
                tmp = (tmp + 1) % left.Count;
                res.Add(left[tmp]);
            }
            tmp = upperRight;
            res.Add(right[tmp]);
            while (tmp != lowerRight)
            {
                tmp = (right.Count + tmp - 1) % right.Count;
                res.Add(right[tmp]);
            }
            return res;
        }
        private (int, int) FindLowerTangent(List<Point> left, List<Point> right, int lmi, int rmi)
        {
            bool done = false;
            int i = 0;
            while (done == false)
            {
                done = true;
                var turn = HelperMethods.CheckTurn(new Line(left[rmi], right[lmi]),right.GetNext(lmi));
                while (turn == Enums.TurnType.Right)
                {
                    lmi = (lmi + 1) % right.Count;
                    turn = HelperMethods.CheckTurn(new Line(left[rmi], right[lmi]), right.GetNext(lmi));
                    done = false;   
                }
                turn = HelperMethods.CheckTurn(new Line(right[lmi], left[rmi]),left.GetPrevious(rmi));
                while (turn == Enums.TurnType.Left)
                {
                    rmi = (left.Count + rmi - 1) % left.Count;
                    turn = HelperMethods.CheckTurn(new Line(right[lmi], left[rmi]), left.GetPrevious(rmi));
                    done = false;
                }
               
            }
            return (rmi, lmi);
        }
        private (int, int) FindUpperTangent(List<Point> left, List<Point> right, int lmi, int rmi)
        {
            bool done = false;

            while (done == false)
            {
                done = true;
                var turn = HelperMethods.CheckTurn(new Line(right[lmi], left[rmi]), left.GetNext(rmi));
                while (turn == Enums.TurnType.Right)
                {
                    rmi = (rmi + 1) % left.Count;

                    turn = HelperMethods.CheckTurn(new Line(right[lmi], left[rmi]), left.GetNext(rmi));
                    done = false;
                }

                turn = HelperMethods.CheckTurn(new Line(left[rmi], right[lmi]), right.GetPrevious(lmi));
                while (turn == Enums.TurnType.Left)
                {
                    lmi = (right.Count + lmi - 1) % right.Count;
                    turn = HelperMethods.CheckTurn(new Line(left[rmi], right[lmi]), right.GetPrevious(lmi));
                    done = false;
                }

            }
            return (rmi, lmi);


        }
        public override string ToString() => "Convex Hull - Divide & Conquer";

    }
}
