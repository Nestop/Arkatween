using UnityEngine;
using Utils;
using Utils.Pool;

namespace Game.Managers
{
    public class PoolManager : MBSingleton<PoolManager>
    {
        public ObjectPool<Block> BlockPool { get; private set; }

        [SerializeField] private Block blockPrefab;
        
        protected override void OnSingletonAwake()
        {
            DontDestroyOnLoad(this);

            BlockPool = new ObjectPool<Block>(GameManager.Instance.LvlInspector.transform, blockPrefab, 0, false, true);
        }
    }
}