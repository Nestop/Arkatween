using Game.Level;
using Game.Managers;
using UnityEngine;
using Utils.Pool;

namespace Game
{
    public class GameRuler : MonoBehaviour
    {
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
            block.OnBlockHit += IncreaseScore;
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
            PoolManager.Instance.BonusPool.DeactivateAllObjects();
            GM.LvlInspector.OpenLevel(LevelProvider.CreateRandomLevel());
        }

        private void Restart()
        {
            GM.LvlInspector.CloseLevel();
            GM.AnimatorButtonPlay.Rebind();
        }
        
        
        public void IncreaseScore()
        {
            GM.GameData.Score.Set(GM.GameData.Score.Value + 1);
        }
    }
}