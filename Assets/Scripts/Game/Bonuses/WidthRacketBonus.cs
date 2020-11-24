using Game.Bonuses.Animation;
using Game.Bonuses.Base;
using Game.Bonuses.Config;
using Game.Managers;

namespace Game.Bonuses
{
    public class WidthRacketBonus : BaseBonus<WidthRacketBonusConfig>
    {
        public WidthRacketBonus(WidthRacketBonusConfig config) : base(config)
        {
        }

        public override void ActivateBonus()
        {
            var racket = GameManager.Instance.Racket;
            WidthRacketBonusAnimation.Play(racket, Config.ResizingValue, Config.BonusColor);
        }
    }
}