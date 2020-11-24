using Game.Bonuses.Base;

namespace Game.Bonuses.Config
{
    public class CloneBallBonusConfig : BaseBonusConfig
    {
        public readonly int ClonesAmount;

        public CloneBallBonusConfig(int clonesAmount)
        {
            ClonesAmount = clonesAmount;
        }
    }
}