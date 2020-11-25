using Game.Managers;
using Game.Objects;
using UnityEngine;

namespace Game.Rules
{
    public class CollisionRules : MonoBehaviour
    {
        private static GameManager GM => GameManager.Instance;
        private static PoolManager PM => PoolManager.Instance;
        
        public void GetCollisionHit(object from, IHitable to)
        {
            if (from is BallController ball)
            {
                switch (to)
                {
                    case Block b:
                        if(b.HP > 0) b.Damage(1);
                        IncreaseScore(1);
                        ball.IncreaseSpeedBy(20f);
                        break;
                    
                    case LoseZone lz:
                        ball.DisableLogic();
                        break;
                }
            }
        }
        
        private void IncreaseScore(int value)
        {
            GM.GameData.Score.Set(GM.GameData.Score.Value + value);
        }
    }
}