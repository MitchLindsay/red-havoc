using System.Collections.Generic;

namespace Assets.Code.Generic
{
    public class PriorityQueue<T>
    {
        private SortedDictionary<int, Queue<T>> dictionary;
        private int count;

        public PriorityQueue()
        {
            this.dictionary = new SortedDictionary<int, Queue<T>>();
            this.count = 0;
        }

        public void Enqueue(T item, int priority)
        {
            if (!dictionary.ContainsKey(priority))
                dictionary.Add(priority, new Queue<T>());

            dictionary[priority].Enqueue(item);
            count++;
        }

        public T Dequeue(int priority)
        {
            count--;
            return dictionary[priority].Dequeue();
        }

        public T Dequeue()
        {
            if (Empty)
                return default(T);

            foreach (Queue<T> queue in dictionary.Values)
            {
                if (queue.Count > 0)
                {
                    count--;
                    return queue.Dequeue();
                }
            }

            return default(T);
        }

        public T Peek()
        {
            if (Empty)
                return default(T);

            foreach (Queue<T> queue in dictionary.Values)
            {
                if (queue.Count > 0)
                    return queue.Peek();
            }

            return default(T);
        }

        public bool Contains(T item)
        {
            foreach (Queue<T> queue in dictionary.Values)
            {
                if (queue.Contains(item))
                    return true;
            }

            return false;
        }

        public int Count
        {
            get { return count; } 
        }

        public bool Empty
        {
            get { return (count == 0); }
        }
    }
}