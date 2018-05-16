using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGUtilities;
namespace CGUtilities
{
    public static class LinqUtil
    {
        // c
        public static IList<T> Swap<T>(this IList<T> list, int indexA, int indexB)
        {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
            return list;
        }
        public static T GetNext<T>(this IList<T> list, int index)
        {   
            return list[(index+1)%list.Count];
        }
        public static (T,int) GetNextIdx<T>(this IList<T> list, int index)
        {
            return (list[(index + 1) % list.Count], (index + 1) % list.Count);
        }
        public static T GetPrevious<T>(this IList<T> list, int index)
        {
            return list[(list.Count + index - 1) % list.Count];
        }
        public static (T,int) GetPreviousIdx<T>(this IList<T> list, int index)
        {
            return (list[(list.Count + index - 1) % list.Count], (list.Count + index - 1) % list.Count);
        }
        public static T GetSecondTop<T>(this Stack<T> stack)
        {
            var tmp = stack.Pop();
            var res = stack.Peek();
            stack.Push(tmp);
            return res;
        }
        public static void PushRange<T>(this Stack<T> stack,List<T> items)
        {
            foreach (var i in items)
                stack.Push(i);
        }
        public static void Enumerate<T>(this IEnumerable<T> ie, Action<T, int> action)
        {
            //https://stackoverflow.com/questions/521687/foreach-with-index
            int i = 0;
            foreach (var e in ie) action(e, i++);
        }
    }
}
