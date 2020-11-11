using Game.Bonuses;
using UnityEngine;
using Utils;
using Utils.Pool;

namespace Game.Managers
{
    public class PoolManager : MBSingleton<PoolManager>
    {
        public ObjectPool<Block> BlockPool { get; private set; }
        public ObjectPool<BonusTrigger> BonusPool { get; private set; }

        [SerializeField] private Block blockPrefab;
        [SerializeField] private BonusTrigger bonusPrefab;
        
        protected override void OnSingletonAwake()
        {
            DontDestroyOnLoad(this);

            var levelTransform = GameManager.Instance.LvlInspector.transform;
            BlockPool = new ObjectPool<Block>(levelTransform, blockPrefab, 0, false, true);
            BonusPool = new ObjectPool<BonusTrigger>(levelTransform, bonusPrefab, 0, false, true);
        }

        public void DeactivateGameObjects()
        {
            BlockPool.DeactivateAllObjects();
            BonusPool.DeactivateAllObjects();
        }
    }
}