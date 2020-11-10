namespace Game.Level
{
    [System.Serializable]
    public class BaseBlockConfig
    {
        public int BlockHp;

        public BaseBlockConfig(int blockHp)
        {
            BlockHp = blockHp;
        }
    }
}