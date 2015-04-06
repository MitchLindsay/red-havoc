using System.Collections.Generic;
using System.Linq;

namespace Assets.Code.Libraries
{
    public class PriorityQueue<TKey, TValue>
    {
        private SortedDictionary<TKey, Queue<TValue>> list = new SortedDictionary<TKey, Queue<TValue>>();

        public void Enqueue(TKey key, TValue value)
        {
            Queue<TValue> queue;

            if (!list.TryGetValue(key, out queue))
            {
                queue = new Queue<TValue>();
                list.Add(key, queue);
            }
            queue.Enqueue(value);
        }

        public TValue Dequeue()
        {
            var pair = list.First();
            var value = pair.Value.Dequeue();

            if (pair.Value.Count == 0)
                list.Remove(pair.Key);

            return value;
        }

        public bool IsEmpty
        {
            get { return !list.Any(); }
        }

        public int Count
        {
            get { return list.Count(); }
        }
    }
}
