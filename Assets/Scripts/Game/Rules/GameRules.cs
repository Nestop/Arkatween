using Game.Level;
using Game.Managers;
using Game.Objects;
using UnityEngine;
using Utils;
using Utils.Pool;

namespace Game.Rules
{
    public class GameRules : MBSingleton<GameRules>
    {
        public CollisionRules CollisionRules => collisionRules;
        
        [SerializeField] private CollisionRules collisionRules;
        
        private static GameManager GM => GameManager.Instance;
        private static PoolManager PM => PoolManager.Instance;

        private void Start()
        {
            PM.BallPool.ObjectCreation += SetBallRules;
            PM.BlockPool.ObjectCreation += SetBlockRules;
        }
        
        
        
        private void SetBlockRules(Block block)
        {
            block.ObjectDeactivation += CheckWin;
        }
        
        private void SetBallRules(BallController ball)
        {
            ball.ObjectDeactivation += CheckLose;
        }

        
        
        private void CheckWin(IDeactivable obj)
        {
            if(PoolManager.Instance.BlockPool.ActiveObjects.Count != 0) return;

            OpenNextLevel();
        }

        private void CheckLose(IDeactivable obj)
        {
            if (PoolManager.Instance.BallPool.ActiveObjects.Count == 0)
                Restart();
        }
        
        
        
        private void OpenNextLevel()
        {
            GM.LvlInspector.OpenLevel(LevelProvider.CreateRandomLevel());
        }

        private void Restart()
        {
            GM.LvlInspector.CloseLevel();
            GM.AnimatorButtonPlay.Rebind();
        }
    }
}