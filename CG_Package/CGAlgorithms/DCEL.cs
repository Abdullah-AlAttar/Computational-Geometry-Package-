using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGUtilities;
namespace CGAlgorithms
{
    public class DCEL
    {

        private List<Vertex> vertices;
        private List<Face> faces;
        private List<HalfEdge> halfEdges;
        private int faceID = 0;

        public DCEL(Point p)
        {
            this.faces = new List<Face>();
            this.vertices = new List<Vertex>();
            var v = new Vertex(p);
            v.ID = 0;
            this.vertices.Add(v);
            this.halfEdges = new List<HalfEdge>();
        }
        public DCEL(List<Point> points)
        {
            this.faces = new List<Face>();
            this.vertices = new List<Vertex>();
            this.halfEdges = new List<HalfEdge>();

            HalfEdge prevLeftEdge = null;
            HalfEdge prevRightEdge = null;

            var face = new Face(this.faceID++);
            this.faces.Add(face);
            int id = 0;
            foreach (Point p in points)
            {
                var v = new Vertex(p)
                {
                    ID = id++
                };

                var left = new HalfEdge();

                var right = new HalfEdge();

                left.IncidentFace = face;
                left.Next = null;
                left.Origin = v;
                left.Twin = right;

                right.IncidentFace = null;
                right.Next = prevRightEdge;
                right.Origin = null;
                right.Twin = left;


                v.IncidentEdge = left;
                this.halfEdges.Add(left);
                this.halfEdges.Add(right);
                this.vertices.Add(v);

                if (prevLeftEdge != null)
                {
                    prevLeftEdge.Next = left;
                    left.Previous = prevLeftEdge;
                }
                if (prevRightEdge != null)
                {
                    prevRightEdge.Origin = v;
                    right.Previous = prevRightEdge;
                }
                prevLeftEdge = left;
                prevRightEdge = right;
            }
            prevLeftEdge.Next = this.HalfEdges[0];
            this.HalfEdges[1].Next = prevRightEdge;
            prevRightEdge.Origin = this.Vertices[0];

            this.halfEdges[0].Previous = this.halfEdges[this.halfEdges.Count - 2];
            this.halfEdges[1].Previous = this.halfEdges[this.halfEdges.Count - 1];

            face.edge = this.HalfEdges[0];
        }
        public void AddVertex(Vertex u, Point p)
        {
            var v = new Vertex(p);
            var h = u.IncidentEdge.Previous;
            v.ID = this.vertices.Count;
            this.vertices.Add(v);
            var h1 = new HalfEdge();
            var h2 = new HalfEdge();
            v.IncidentEdge = h2;
            h1.Twin = h2;
            h2.Twin = h1;
            h1.IncidentFace = null;
            h2.IncidentFace = null;
            h1.Next = h2;
            h2.Next = h.Next;
            h1.Previous = h;
            h2.Previous = h1;
            h.Next = h1;
            h2.Next.Previous = h2;
            this.halfEdges.Add(h1);
            this.halfEdges.Add(h2);
        }
        public void AddHalfEdge(Vertex a, Vertex b)
        {

            for (int idx = 0; idx < this.faces.Count; ++idx)
            {
                if (faces[idx].id == a.IncidentEdge.IncidentFace.id)
                {
                    this.faces.RemoveAt(idx);
                    break;
                }
            }

            var h = a.IncidentEdge.Previous;
            var hb = b.IncidentEdge;
            var h1 = new HalfEdge();
            var h2 = new HalfEdge();
            var f1 = new Face(this.faceID++);
            var f2 = new Face(this.faceID++);
            f1.edge = h1;
            f2.edge = h2;
            h1.Origin = a;
            h2.Origin = b;
            h1.Twin = h2;
            h2.Twin = h1;

            h2.Next = h.Next;
            h2.Next.Previous = h2;

            h1.Previous = h;
           
            h.Next = h1;

            var i = h2;
            while (true)
            {
                i.IncidentFace = f2;
                if (i.Next.Origin.Equals(b))
                    break;
                i = i.Next;
            }
            h1.Next = i.Next;
            h1.Next.Previous = h1;
            i.Next = h2;
            h2.Previous = i;
            var j = h1;
            while (true)
            {

                j.IncidentFace = f1;
                if (j.Next.Origin.Equals(a))
                    break;
                j = j.Next;
            }

            this.halfEdges.Add(h1);
            this.halfEdges.Add(h2);
            this.faces.Add(f1);
            this.faces.Add(f2);
        }
        public (Vertex maxY, Vertex minY) GetMaxMinY()
        {
            Vertex maxY = vertices[0];
            Vertex minY = vertices[0];
            vertices.Enumerate((v, i) =>
            {
                if (v.Position.Y > maxY.Position.Y)
                    maxY = v;
                else if (v.Position.Y < minY.Position.Y)
                    minY = v;
            });
            return (maxY, minY);
        }

        public List<Vertex> GetLeftChain()
        {
            var res = new List<Vertex>();
            var (maxY, minY) = this.GetMaxMinY();

            var curHalfEdge = maxY.IncidentEdge;

            var start = curHalfEdge.Origin.Position;
            var target = minY.Position;

            while (!start.Equals(target))
            {
                curHalfEdge.Origin.side = Chain.LEFT;
                res.Add(curHalfEdge.Origin);
                curHalfEdge = curHalfEdge.Next;
                start = curHalfEdge.Origin.Position;
            }

            return res;
        }
        
        public List<Vertex> GetRightChain()
        {
            var res = new List<Vertex>();
            var (maxY, minY) = this.GetMaxMinY();

            var curHalfEdge = maxY.IncidentEdge;
            curHalfEdge = curHalfEdge.Previous;
            var start = curHalfEdge.Origin.Position;
            var target = minY.Position;

            while (!start.Equals(target))
            {
                curHalfEdge.Origin.side = Chain.RIGHT;
                res.Add(curHalfEdge.Origin);
                curHalfEdge = curHalfEdge.Previous;
                start = curHalfEdge.Origin.Position;
            }
            minY.side = Chain.RIGHT;
            res.Add(minY);
            return res;
        }
        public List<Vertex> Sort()
        {
            var leftChain = GetLeftChain();
            var rightChain = GetRightChain();
            var res = new List<Vertex>();

            int li = 0, ri = 0;

            while (li < leftChain.Count && ri < rightChain.Count)
            {
                if (leftChain[li].Position.Y > rightChain[ri].Position.Y)
                    res.Add(leftChain[li++]);
                else if (leftChain[li].Position.Y < rightChain[ri].Position.Y)
                    res.Add(rightChain[ri++]);
                else
                {
                    if (leftChain[li].Position.X < rightChain[ri].Position.X)
                        res.Add(leftChain[li++]);
                    else
                        res.Add(rightChain[ri++]);
                }
            }

            while (li < leftChain.Count)
                res.Add(leftChain[li++]);
            while (ri < rightChain.Count)
                res.Add(rightChain[ri++]);

            return res;
        }
        public List<Vertex> Vertices { get => this.vertices; }
        public List<Face> Faces { get => this.faces; }
        public List<HalfEdge> HalfEdges { get => this.halfEdges; }
    }
    public class HalfEdge
    {
        public HalfEdge(Point origin, HalfEdge twin, HalfEdge next, HalfEdge previous, Face incidentFace)
        {
            this.Origin = new Vertex(origin);
            this.Twin = twin;
            this.Next = next;
            this.Previous = previous;
            this.IncidentFace = incidentFace;
        }
        public HalfEdge(HalfEdge h)
        {
            this.Origin = new Vertex(h.Origin.Position);
            this.Twin = h.Twin;
            this.Next = h.Next;
            this.Previous = h.Previous;
            this.IncidentFace = h.IncidentFace;
        }
        public HalfEdge()
        {

        }
        public Vertex Origin { get; set; }
        public HalfEdge Twin { get; set; }

        public HalfEdge Next { get; set; }
        public HalfEdge Previous { get; set; }

        public Face IncidentFace { get; set; }
        public override string ToString()
        {
            return this.Origin.Position.ToString();
        }
    }
    public enum Chain { LEFT, RIGHT }
    public class Vertex
    {
        public Point Position { get; set; }
        public int ID { get; set; }
        public Chain side;
        public HalfEdge IncidentEdge { get; set; }

        public Vertex(Point p)
        {
            this.Position = new Point(p.X, p.Y);
        }
        public override string ToString()
        {
            return this.ID + ": "+ this.Position.ToString();
        }
    }

    public class Face
    {
        public HalfEdge edge;
        public int id;
        public Face(int id)
        {
            this.id = id;
            this.edge = null;
        }
        public Face(int id ,HalfEdge edge)
        {
            this.id = id;
            this.edge = edge;
        }
        public override string ToString()
        {
            return this.id.ToString();
        }
        public override bool Equals(object obj)
        {
            if (obj is Face f)
            {
                return f.edge.Origin.Equals(this.edge.Origin);
            }
            return false;
        }
    }
}
