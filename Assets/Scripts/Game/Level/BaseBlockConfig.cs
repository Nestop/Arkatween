using Game.Bonuses.Base;

namespace Game.Level
{
    [System.Serializable]
    public class BaseBlockConfig
    {
        public int blockHp;
        public Bonus bonus;

        public BaseBlockConfig(int blockHp, Bonus bonus = null)
        {
            this.blockHp = blockHp;
            this.bonus = bonus;
        }
    }
}