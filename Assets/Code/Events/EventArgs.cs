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
}