using Game.Bonuses.Animation;
using Game.Bonuses.Base;
using Game.Bonuses.Config;
using Game.Managers;
using Game.Objects;

namespace Game.Bonuses
{
    public class SpeedBallBonus : BaseBonus<SpeedBallBonusConfig>
    {
        private float _speedMultiplier;

        public SpeedBallBonus(SpeedBallBonusConfig config) : base(config)
        {
        }

        public override void ActivateBonus()
        {
            foreach (var ball in PoolManager.Instance.BallPool.ActiveObjects)
            {
                _speedMultiplier = BallController.OrigSpeedMultiplier;

                SpeedBallBonusAnimation.Play(Config.SpeedMultiplier, Config.Duration, ball, Config.BonusColor,
                    () => _speedMultiplier,
                    value =>
                    {
                        _speedMultiplier = value;
                        ball.SetSpeedMultiplier(_speedMultiplier);
                    });
            }
        }
    }
}