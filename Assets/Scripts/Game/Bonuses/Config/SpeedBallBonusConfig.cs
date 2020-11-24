using Game.Bonuses.Base;
using UnityEngine;

namespace Game.Bonuses.Config
{
    public class SpeedBallBonusConfig : BaseBonusConfig
    {
        public readonly Color BonusColor;
        public readonly float Duration;
        public readonly float SpeedMultiplier;

        public SpeedBallBonusConfig(float speedMultiplier, float duration, Color bonusColor)
        {
            SpeedMultiplier = speedMultiplier;
            Duration = duration;
            BonusColor = bonusColor;
        }
    }
}