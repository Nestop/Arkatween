using Game.Bonuses.Base;
using UnityEngine;

namespace Game.Bonuses.Config
{
    public class WidthRacketBonusConfig : BaseBonusConfig
    {
        public readonly Color BonusColor;
        public readonly float ResizingValue;

        public WidthRacketBonusConfig(float resizingValue, Color bonusColor)
        {
            ResizingValue = resizingValue;
            BonusColor = bonusColor;
        }
    }
}