using Game.Bonuses.Animation;
using Game.Bonuses.Base;
using Game.Bonuses.Config;
using Game.Managers;

namespace Game.Bonuses
{
    public class CloneBallBonus : BaseBonus<CloneBallBonusConfig>
    {
        private float _speedMultiplier;

        public CloneBallBonus(CloneBallBonusConfig config) : base(config)
        {
        }

        public override void ActivateBonus()
        {
            var ballPool = PoolManager.Instance.BallPool;
            var activeBalls = ballPool.ActiveObjects.Count;
            
            for (var i = 0; i<activeBalls; i++)
            {
                var ball = PoolManager.Instance.BallPool.GetObject();
                ball.transform.localPosition = ballPool.ActiveObjects[i].transform.localPosition;
                ball.EnableLogic();
            }
        }
    }
}