using Game.Bonuses.Base;
using Game.Objects;
using UnityEngine;
using Utils;
using Utils.Pool;

namespace Game.Managers
{
    public class PoolManager : MBSingleton<PoolManager>
    {
        public ObjectPool<BallController> BallPool { get; private set; }
        public ObjectPool<Block> BlockPool { get; private set; }
        public ObjectPool<BonusTrigger> BonusPool { get; private set; }

        [SerializeField] private BallController ballPrefab;
        [SerializeField] private Block blockPrefab;
        [SerializeField] private BonusTrigger bonusPrefab;
        
        protected override void OnSingletonAwake()
        {
            DontDestroyOnLoad(this);

            var levelTransform = GameManager.Instance.LvlInspector.transform;
            BallPool = new ObjectPool<BallController>(GameManager.Instance.LayerMid, ballPrefab);
            BlockPool = new ObjectPool<Block>(levelTransform, blockPrefab);
            BonusPool = new ObjectPool<BonusTrigger>(levelTransform, bonusPrefab);
        }

        public void DeactivateGameObjects()
        {
            BlockPool.DeactivateAllObjects();
            BonusPool.DeactivateAllObjects();
        }
    }
}