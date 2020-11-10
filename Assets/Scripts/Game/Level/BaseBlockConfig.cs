using Game.Bonuses;

namespace Game.Level
{
    [System.Serializable]
    public class BaseBlockConfig
    {
        public int blockHp;
        public BaseBonus bonus;

        public BaseBlockConfig(int blockHp, BaseBonus bonus = null)
        {
            this.blockHp = blockHp;
            this.bonus = bonus;
        }
    }
}