using System;
using UnityEngine;

namespace Game
{
    public class LoseZone : MonoBehaviour, IHitable
    {
        public event Action<IHitable, object> WasHit;
        
        public void MakeHit(object from)
        {
            WasHit?.Invoke(this, from);
        }
    }
}