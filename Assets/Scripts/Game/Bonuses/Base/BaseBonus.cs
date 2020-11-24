namespace Game.Bonuses.Base
{
    public abstract class BaseBonus<T> : Bonus where T : BaseBonusConfig
    {
        protected readonly T Config;

        protected BaseBonus(T config)
        {
            Config = config;
        }
    }
}