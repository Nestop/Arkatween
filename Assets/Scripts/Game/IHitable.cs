using System;

namespace Game
{
    public interface IHitable
    {
        event Action<IHitable, object> WasHit;
        void MakeHit(object from);
    }
}