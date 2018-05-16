using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGUtilities;
namespace CGAlgorithms.Algorithms.PolygonTriangulation
{
    class MonotoneTriangulation  :Algorithm
    {
        public override void Run(System.Collections.Generic.List<CGUtilities.Point> points, System.Collections.Generic.List<CGUtilities.Line> lines, System.Collections.Generic.List<CGUtilities.Polygon> polygons, ref System.Collections.Generic.List<CGUtilities.Point> outPoints, ref System.Collections.Generic.List<CGUtilities.Line> outLines, ref System.Collections.Generic.List<CGUtilities.Polygon> outPolygons)
        {
            //outPoints.Add(new Point(0, 0));
            List<Point> pts = new List<Point>();
            foreach (var line in polygons[0].lines)
                pts.Add(line.Start);

            HelperMethods.CheckMakeCCW(ref pts);

            if (!HelperMethods.IsYMonotone(pts))
            {
                Console.WriteLine("The Polygon is not Y Monoton");
                return;
            }
            DCEL dcel = new DCEL(pts);

            var st = new Stack<Vertex>();

            var sortedVertices = dcel.Sort();

            st.Push(sortedVertices[0]);
            st.Push(sortedVertices[1]);

            for (int i = 2; i < sortedVertices.Count - 1; ++i)
            {
                if (st.Peek().side != sortedVertices[i].side)
                {
                    while (st.Count > 1)
                    {
                        var tmp = st.Pop();
                        outLines.Add(new Line(sortedVertices[i].Position, tmp.Position));
                        dcel.AddHalfEdge(sortedVertices[i], tmp);
                    }
                    st.Pop();
                    st.Push(sortedVertices[i - 1]);
                    st.Push(sortedVertices[i]);
                }
                else
                {
                    if (IsConvex(st.Peek().Position, st.GetSecondTop().Position, sortedVertices[i].Position, st.Peek().side))
                    {
                        st.Push(sortedVertices[i]);
                    }
                    else
                    {

                        Vertex tmp = st.Pop();
                        while (st.Count >= 1 && IsConvex(tmp.Position, st.Peek().Position, sortedVertices[i].Position,
                            sortedVertices[i].side == Chain.LEFT ? Chain.RIGHT : Chain.LEFT))
                        {
                            tmp = st.Pop();
                            outLines.Add(new Line(sortedVertices[i].Position, tmp.Position));

                            dcel.AddHalfEdge(sortedVertices[i], tmp);
                        }
                        st.Push(tmp);
                        st.Push(sortedVertices[i]);

                    }
                }
            }
            //var edge = dcel.Faces[1].edge;
            //var next = edge.Next;
            //var start = edge.Origin;
            //outPoints.Add(edge.Origin.Position);
            //while (start.ID != next.Origin.ID )
            //{
            //    outPoints.Add(next.Origin.Position);
            //    next = next.Next;
            //}
          
            //Console.WriteLine(st.Count);
        }
        private bool IsConvex(Point p,Point prev,Point next,Chain side)
        {
            //Console.WriteLine(p + " " + HelperMethods.GetAngle(prev, p, next));
            //return HelperMethods.GetAngle(prev, p, next) < 180;

            if (side == Chain.LEFT)
            {
                return HelperMethods.CheckTurn(new Line(prev, p), next) == Enums.TurnType.Right;
            }
            else 
            {
                return HelperMethods.CheckTurn(new Line(prev, p), next) == Enums.TurnType.Left;
            }

        }
        private bool IsVisible()
        {
            return true;    
        }
        public override string ToString()
        {
            return "Monotone Triangulation";
        }
    }
}
