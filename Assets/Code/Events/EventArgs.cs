using System;

namespace Assets.Code.Events
{
    public class EventArgs<T> : EventArgs
    {
        public T Value { get; private set; }

        public EventArgs(T value)
        {
            this.Value = value;
        }
    }

    public class EventArgs<T, K> : EventArgs<T>
    {
        public K Value2 { get; private set; }

        public EventArgs(T value, K value2) : base(value)
        {
            this.Value2 = value2;
        }
    }

    public class EventArgs<T, K, U> : EventArgs<T, K>
    {
        public U Value3 { get; private set; }

        public EventArgs(T value, K value2, U value3) : base(value, value2)
        {
            this.Value3 = value3;
        }
    }
}