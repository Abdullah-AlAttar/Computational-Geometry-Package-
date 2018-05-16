using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGUtilities;
using CGUtilities.DataStructures;
namespace CGAlgorithms.Algorithms.SegmentIntersection
{
    class SweepLine:Algorithm
    {
        public enum EventType {START,END,INTERS };
        public class Event
        {
            public Point point;
            public Event prev;
            public Event next;
            public int index;
            public EventType eventType;

            public Event(Point p, int index, EventType eventType)
            {
                this.point = p;
                this.index = index;
                this.eventType = eventType;
            }

            public Event(Point p, Event prev, Event next, EventType eventType)
            {
                this.point = p;
                this.prev = prev;
                this.next = next;
                this.eventType = eventType;
            }
        }

        public override void Run(List<CGUtilities.Point> points, List<CGUtilities.Line> lines, List<CGUtilities.Polygon> polygons, ref List<CGUtilities.Point> outPoints, ref List<CGUtilities.Line> outLines, ref List<CGUtilities.Polygon> outPolygons)
        {
            var Q = new OrderedSet<Event>(Comparer<Event>.Create((Event a, Event b) =>
            {
                if (a.point.X != b.point.X)
                    return a.point.X.CompareTo(b.point.X);
                if (a.point.Y != b.point.Y)
                    return a.point.Y.CompareTo(b.point.Y);
                return lines[a.index].End.X.CompareTo(lines[b.index].End.X);

            }));
            var L = new OrderedSet<Event>(Comparer<Event>.Create((Event a, Event b) =>
            {
                if (a.point.Y != b.point.Y)
                    return a.point.Y.CompareTo(b.point.Y);
                if (a.point.X != b.point.X)
                    return a.point.X.CompareTo(b.point.X);
                return lines[a.index].End.Y.CompareTo(lines[b.index].End.Y);
            }));

            lines.Enumerate((l, i) =>
            {
                if (l.Start.X  > l.End.X)
                {
                    var tmp = l.Start;
                    l.Start = l.End;
                    l.Start = tmp;
                    
                }
                Q.Add(new Event(l.Start, i, EventType.START));
                Q.Add(new Event(l.End, i, EventType.END));
            });
            while (Q.Count>0)
            {
                var curEvent = Q.GetFirst();
                Q.Remove(curEvent);

                if (curEvent.eventType == EventType.START)
                {
                    L.Add(curEvent);
                    var (prevEvent, nextEvent) = L.DirectUpperAndLower(curEvent);
                    if (prevEvent !=null)
                    {
                        var (prevLine, curLine) = (lines[prevEvent.index], lines[curEvent.index]);

                        if (HelperMethods.CheckSegmentsIntersections(prevLine,curLine))
                        {
                            var interPoint = HelperMethods.GetIntersectionPoint(prevLine, curLine);
                            Q.Add(new Event(interPoint, prevEvent, curEvent, EventType.INTERS));
                            outPoints.Add(interPoint);
                        }
                    }
                    if (nextEvent != null)
                    {
                        var (nextLine, curLine) = (lines[nextEvent.index], lines[curEvent.index]);
                        if (HelperMethods.CheckSegmentsIntersections(nextLine, curLine))
                        {
                            var interPoint = HelperMethods.GetIntersectionPoint(nextLine, curLine);
                            Q.Add(new Event(interPoint, curEvent, nextEvent, EventType.INTERS));
                            outPoints.Add(interPoint);
                        }
                    }
                }

                if (curEvent.eventType == EventType.END)
                {
                    var (prevEvent, nextEvent) = L.DirectUpperAndLower(curEvent);
                    if ( prevEvent != null && nextEvent !=null)
                    {
                        var (prevLine, nextLine) = (lines[prevEvent.index], lines[nextEvent.index]);

                        if (HelperMethods.CheckSegmentsIntersections(prevLine, nextLine))
                        {
                            var interPoint = HelperMethods.GetIntersectionPoint(prevLine, nextLine);
                            Q.Add(new Event(interPoint, prevEvent, nextEvent, EventType.INTERS));
                            outPoints.Add(interPoint);
                        }
                        L.Remove(curEvent);
                    }
                }
                if (curEvent.eventType == EventType.INTERS)
                {
                    var (pEvent, nEvent) = (curEvent.prev, curEvent.next);
                    var ppEvent = L.DirectUpperAndLower(pEvent).prev;
                    if (ppEvent !=null)
                    {
                        var (prevLine, nextLine) = (lines[ppEvent.index], lines[nEvent.index]);
                        if (HelperMethods.CheckSegmentsIntersections(prevLine,nextLine))
                        {
                            var interPoint = HelperMethods.GetIntersectionPoint(prevLine, nextLine);
                            Q.Add(new Event(interPoint, ppEvent, nEvent, EventType.INTERS));
                            outPoints.Add(interPoint);
                        }
                    }
                    var nnEvent = L.DirectUpperAndLower(nEvent).next;
                    if (nnEvent != null)
                    {
                        var (prevLine, nextLine) = (lines[pEvent.index], lines[nnEvent.index]);
                        if (HelperMethods.CheckSegmentsIntersections(prevLine, nextLine))
                        {
                            var interPoint = HelperMethods.GetIntersectionPoint(prevLine, nextLine);
                            Q.Add(new Event(interPoint, pEvent, nnEvent, EventType.INTERS));
                            outPoints.Add(interPoint);
                        }
                    }
                    pEvent.point = nEvent.point = curEvent.point;
                    L.RemoveAll(p => p.index == pEvent.index || p.index == nEvent.index);
                    L.Add(nEvent);
                    L.Add(pEvent);
                }
            }
        }

        public override string ToString()
        {
            return "Sweep Line";
        }
    }
}
