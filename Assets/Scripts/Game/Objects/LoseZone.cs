using Game.Rules;
using UnityEngine;

namespace Game.Objects
{
    public class LoseZone : MonoBehaviour, IHitable
    {
        public void MakeHit(object from)
        {
            GameRules.Instance.CollisionRules.GetCollisionHit(from, this);
        }
    }
}