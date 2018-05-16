using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Spatial.Euclidean;
namespace CGUtilities
{
    public class HelperMethods
    {
        public static Enums.PointInPolygon PointInTriangle(Point p, Point a, Point b, Point c)
        {
            if (a.Equals(b) && b.Equals(c))
            {
                if (p.Equals(a) || p.Equals(b) || p.Equals(c))
                    return Enums.PointInPolygon.OnEdge;
                else
                    return Enums.PointInPolygon.Outside;
            }
            
            Line ab = new Line(a, b);
            Line bc = new Line(b, c);
            Line ca = new Line(c, a);

            if (GetVector(ab).Equals(Point.Identity))
                return (PointOnSegment(p, ca.Start, ca.End)) ? Enums.PointInPolygon.OnEdge : Enums.PointInPolygon.Outside;
            if (GetVector(bc).Equals(Point.Identity))
                return (PointOnSegment(p, ca.Start, ca.End)) ? Enums.PointInPolygon.OnEdge : Enums.PointInPolygon.Outside;
            if (GetVector(ca).Equals(Point.Identity))
                return (PointOnSegment(p, ab.Start, ab.End)) ? Enums.PointInPolygon.OnEdge : Enums.PointInPolygon.Outside;

            if (CheckTurn(ab, p) == Enums.TurnType.Colinear)
                return PointOnSegment(p, a, b)? Enums.PointInPolygon.OnEdge : Enums.PointInPolygon.Outside;
            if (CheckTurn(bc, p) == Enums.TurnType.Colinear && PointOnSegment(p, b, c))
                return PointOnSegment(p, b, c) ? Enums.PointInPolygon.OnEdge : Enums.PointInPolygon.Outside;
            if (CheckTurn(ca, p) == Enums.TurnType.Colinear && PointOnSegment(p, c, a))
                return PointOnSegment(p, a, c) ? Enums.PointInPolygon.OnEdge : Enums.PointInPolygon.Outside;

            if (CheckTurn(ab, p) == CheckTurn(bc, p) && CheckTurn(bc, p) == CheckTurn(ca, p))
                return Enums.PointInPolygon.Inside;

            return Enums.PointInPolygon.Outside;
        }

        public static bool IsYMonotone(List<Point> pts)
        {
            var (minPt, minIdx) = GetMinPoint(pts,'y');
            var (maxPt, maxIdx) = GetMaxPoint(pts,'y');

            var curPt = new Point(maxPt.X, maxPt.Y);
            var curIdx = maxIdx;

            while (curIdx != minIdx)
            {
                var (nextPt, nextIdx) = pts.GetNextIdx(curIdx);
                if (nextPt.Y > curPt.Y)
                    return false;
                curPt = new Point(nextPt.X, nextPt.Y);
                curIdx = nextIdx;
            }
            curPt = new Point(maxPt.X, maxPt.Y);
            curIdx = maxIdx;
            while (curIdx != minIdx)
            {
                var (nextPt, nextIdx) = pts.GetPreviousIdx(curIdx);
                if (nextPt.Y > curPt.Y)
                    return false;
                curPt = new Point(nextPt.X, nextPt.Y);
                curIdx = nextIdx;
            }
            return true;    
        }

        public static void CheckMakeCCW(ref List<Point> pts)
        {
            var (minPoint, minIdx) = GetMinPoint(pts, 'x');

            if (CheckTurn(new Line( pts.GetPrevious(minIdx),minPoint),pts.GetNext(minIdx))==Enums.TurnType.Right)
            {
                Console.WriteLine("NOT CCW");
                pts.Reverse();
            }

        }

        public static Enums.TurnType CheckTurn(Point vector1, Point vector2)
        {
            double result = CrossProduct(vector1, vector2);
            if (result < 0) return Enums.TurnType.Right;
            else if (result > 0) return Enums.TurnType.Left;
            else return Enums.TurnType.Colinear;
        }
        public static List<Point> SortPoints(List<Point> points)
        {
            var (minPoint, idx) = HelperMethods.GetMinPoint(points, 'y');
            var minVec = new Vector2D(new Point(minPoint.X + 1234, minPoint.Y));

            points.Sort((a, b) =>
            {
                if (HelperMethods.CheckTurn(new Line(minPoint, a), b) == Enums.TurnType.Colinear)
                    return Point.GetSqrDistance(a, minPoint).CompareTo(Point.GetSqrDistance(b, minPoint));

                return minVec.AngleTo(new Vector2D(minPoint.VectorTo(a))).Degrees
                .CompareTo(minVec.AngleTo(new Vector2D(minPoint.VectorTo(b))).Degrees);

            });
            return points;
        }
        public static List<Point> ConvexHullGrahams(List<Point> points)
        {

            if (points.Count == 1) { return points; }

            List<Point> outPoints = new List<Point>();
            var (minPoint, idx) = HelperMethods.GetMinPoint(points, 'y');
            var minVec = new Vector2D(new Point(minPoint.X + 1234, minPoint.Y));

            points.Sort((a, b) =>
            {
                if (HelperMethods.CheckTurn(new Line(minPoint, a), b) == Enums.TurnType.Colinear)
                    return Point.GetSqrDistance(a, minPoint).CompareTo(Point.GetSqrDistance(b, minPoint));

                return minVec.AngleTo(new Vector2D(minPoint.VectorTo(a))).Degrees
                .CompareTo(minVec.AngleTo(new Vector2D(minPoint.VectorTo(b))).Degrees);

            });

            for (int i = 1; i < points.Count - 1; ++i)
            {
                if (HelperMethods.CheckTurn(new Line(points[0], points[i]), points[i + 1]) == Enums.TurnType.Colinear)
                {
                    points.RemoveAt(i--);
                }
            }
            if (points.Count <= 3) { outPoints.AddRange(points); return outPoints; }

            Stack<Point> st = new Stack<Point>();
            st.PushRange(points.GetRange(0, 3));

            for (int i = 3; i < points.Count; ++i)
            {
                while (HelperMethods.CheckTurn(new Line(st.GetSecondTop(), st.Peek()), points[i])
                    != Enums.TurnType.Left)
                    st.Pop();
                st.Push(points[i]);
            }
            outPoints.AddRange(st);
            outPoints.Reverse();
            return outPoints;
        }
        public static double CrossProduct(Point a, Point b)
        {
            return a.X * b.Y - a.Y * b.X;
            
        }
        public static double DotProduct(Point a, Point b)
        {
            return a.X * b.X + a.Y * b.Y;
        }
        public static double GetAngle(Point a, Point b, Point c)
        {
            Point vecBA = b.VectorTo(a);
            Point vecBC = b.VectorTo(c);
            double CP = CrossProduct(vecBA, vecBC);
            double DP = DotProduct(vecBA, vecBC);
            double radAngle = Math.Atan2(CP, DP);
            if (radAngle < 0)
                radAngle += Math.PI * 2;

            double degAngle = radAngle * 180 / Math.PI;

            return degAngle;
        }
        public static bool PointOnRay(Point p, Point a, Point b)
        {
            if (a.Equals(b)) return true;
            if (a.Equals(p)) return true;
            var q = a.VectorTo(p).Normalize();
            var w = a.VectorTo(b).Normalize();
            return q.Equals(w);
        }
        public static bool PointOnSegment(Point p, Point a, Point b)
        {
            if (a.Equals(b))
                return p.Equals(a);

            if (b.X == a.X)
                return p.X == a.X && (p.Y >= Math.Min(a.Y, b.Y) && p.Y <= Math.Max(a.Y, b.Y));
            if (b.Y == a.Y)
                return p.Y == a.Y && (p.X >= Math.Min(a.X, b.X) && p.X <= Math.Max(a.X, b.X));
            double tx = (p.X - a.X) / (b.X - a.X);
            double ty = (p.Y - a.Y) / (b.Y - a.Y);

            return (Math.Abs(tx - ty) <= Constants.Epsilon && tx <= 1 && tx >= 0);
        }
        /// <summary>
        /// Get turn type from cross product between two vectors (l.start -> l.end) and (l.end -> p)
        /// </summary>
        /// <param name="l"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Enums.TurnType CheckTurn(Line l, Point p)
        {
            Point a = l.Start.VectorTo(l.End);
            Point b = l.End.VectorTo(p);
            return HelperMethods.CheckTurn(a, b);
        }
        /// <summary>
        /// Check wether two segments intersects 
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns></returns>
        public static bool CheckSegmentsIntersections(Line l1,Line l2)
        {
            var (t1, t2) = (CheckTurn(l1, l2.Start), CheckTurn(l1, l2.End));
            var (t3, t4) = (CheckTurn(l2, l1.Start), CheckTurn(l2, l1.End));
            return ((t1 != t2) && (t3 != t4));
        }

        public static bool IsSupportLine(Line l, List<Point> points)
        {

            int idx = 0;
            Enums.TurnType prevSide = Enums.TurnType.Left;//dummy value
            bool inside = true;

            foreach (Point p in points)
            {

                if (p.Equals(l.Start) || p.Equals(l.End))
                    continue;

                var curSide = HelperMethods.CheckTurn(l, p);

                if (curSide == Enums.TurnType.Colinear)
                {
                    var dst1 = Point.GetSqrDistance(l.Start, l.End);
                    var dst2 = Point.GetSqrDistance(l.Start, p);
                    if (dst1 < dst2)
                    {
                        inside = false;
                        break;
                    }
                }
                if (idx != 0)
                {
                    if (curSide != prevSide)
                    {
                        inside = false;
                        break;
                    }

                }
                prevSide = curSide;
                idx++;
            }
            return inside;
        }
        public static Point GetVector(Line l)
        {
            return l.Start.VectorTo(l.End);
        }
        public static double GetSqrDistance(Point a, Point b) => (a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y);
        public static double GetAngle(Point vec1, Point vec2)
        {
            var v1 = new Vector2D(vec1.X, vec1.Y);
            var v2 = new Vector2D(vec2.X, vec2.Y);
            return v1.AngleTo(v2).Degrees;
        }

        public static (Point,int) GetMinPoint(List<Point> points,char axis)
        {
            int minIdx = 0;
            if (axis == 'y')
                for (int i = 0; i < points.Count; i++)
                {
                    if (points[i].Y < points[minIdx].Y || (points[i].Y == points[minIdx].Y && points[i].X < points[minIdx].X))
                        minIdx = i;
                }
            else
                for (int i = 0; i < points.Count; i++)
                {
                    if (points[i].X < points[minIdx].X || (points[i].X == points[minIdx].X && points[i].Y < points[minIdx].Y))
                        minIdx = i;
                }
            return (points[minIdx], minIdx);

        }
        public static (Point, int) GetMaxPoint(List<Point> points, char axis)
        {
            int minIdx = 0;
            if (axis == 'y')
                for (int i = 0; i < points.Count; i++)
                {
                    if (points[i].Y > points[minIdx].Y || (points[i].Y == points[minIdx].Y && points[i].X < points[minIdx].X))
                        minIdx = i;
                }
            else
                for (int i = 0; i < points.Count; i++)
                {
                    if (points[i].X > points[minIdx].X || (points[i].X == points[minIdx].X && points[i].Y < points[minIdx].Y))
                        minIdx = i;
                }
            return (points[minIdx], minIdx);

        }
        public static Point GetIntersectionPoint(Line l1,Line l2)
        {

            //Refrence
            //https://www.topcoder.com/community/data-science/data-science-tutorials/geometry-concepts-line-intersection-and-its-applications/
            var a1 = l1.End.Y - l1.Start.Y;
            var b1 = l1.Start.X - l1.End.X;
            var c1 = a1 * (l1.Start.X) + b1 * (l1.Start.Y);

            //second line equation 
            var a2 = l2.End.Y - l2.Start.Y;
            var b2 = l2.Start.X - l2.End.X;

            var c2 = a2 * (l2.Start.X) + b2 * (l2.Start.Y);

            var det = a1 * b2 - a2 * b1;

            var xCoord = (b2 * c1 - b1 * c2) / det;
            var yCoord = (a1 * c2 - a2 * c1) / det;
            return new Point(xCoord, yCoord);
        }
    }
}
