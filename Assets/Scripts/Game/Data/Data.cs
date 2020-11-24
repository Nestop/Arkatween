using System;

namespace Game.Data
{
    public class Data<T>
    {
        public T Value { get; private set; }

        public event Action<T> ChangeEvent;

        public Data(T value)
        {
            Value = value;
        }

        public void Set(T value)
        {
            Value = value;
            ChangeEvent?.Invoke(Value);
        }
    }
}