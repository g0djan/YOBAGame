using System.Collections.Generic;
using System.Linq;

namespace YOBAGame.Extensions
{
    public static class LinqExtension
    {
        public static IEnumerable<List<T>> CyclicThreeGramms<T>(this IEnumerable<T> enumerable)
        {
            List<T> firstTwo = new List<T>();
            Queue<T> prevTwo = new Queue<T>();
            foreach (var element in enumerable)
            {
                if (firstTwo.Count < 2)
                    firstTwo.Add(element);
                prevTwo.Enqueue(element);
                if (prevTwo.Count == 3)
                {
                    yield return new List<T>(prevTwo);
                    prevTwo.Dequeue();
                }
            }
            if (firstTwo.Count + prevTwo.Count < 3)
                yield break;
            while (prevTwo.Count > 0)
            {
                yield return new List<T>(prevTwo.Concat(firstTwo.GetRange(0, 3 - prevTwo.Count)));
                prevTwo.Dequeue();
            }
        }
    }
}