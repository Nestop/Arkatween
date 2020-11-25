using System.Collections.Generic;
using DG.Tweening;
using Game.Bonuses.Animation;
using Game.Bonuses.Base;
using Game.Bonuses.Config;
using Game.Managers;
using Game.Objects;

namespace Game.Bonuses
{
    public class RageBallBonus : BaseBonus<SpeedBallBonusConfig>
    {
        private readonly List<Block> _blocks;
        private float _speedMultiplier;

        public RageBallBonus(SpeedBallBonusConfig config) : base(config)
        {
            _blocks = PoolManager.Instance.BlockPool.Objects;
        }

        public override void ActivateBonus()
        {
            SetBlocksGhostBody(true);

            foreach (var ball in PoolManager.Instance.BallPool.ActiveObjects)
            {
                _speedMultiplier = BallController.OrigSpeedMultiplier;
                SpeedBallBonusAnimation.Play(Config.SpeedMultiplier, Config.Duration, ball, Config.BonusColor,
                        () => _speedMultiplier,
                        value =>
                        {
                            _speedMultiplier = value;
                            ball.SetSpeedMultiplier(_speedMultiplier);
                        })
                    .OnComplete(() => SetBlocksGhostBody(false));
            }
        }

        private void SetBlocksGhostBody(bool isActive)
        {
            foreach (var block in _blocks)
                block.Collider2D.isTrigger = isActive;
        }
    }
}