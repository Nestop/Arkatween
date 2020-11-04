using System;
using System.Collections.Generic;
using DG.Tweening;
using Game.Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class LevelInspector : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;

        private float _startPosX;
        private float _startPosY;
        private Vector3 _originalPos;
        private Vector3 _originalScale;
        
        private void Awake()
        {
            _startPosX = -Screen.width / 2 + Block.Width / 2 + GameManager.Instance.DistanceBetweenBlocks;
            _startPosY = Screen.height / 2 - Block.Height / 2 - GameManager.Instance.DistanceBetweenBlocks;
            _originalPos = rectTransform.position;
            _originalScale = rectTransform.localScale;
        }

        public void CreateLevel()
        {
            var gManager = GameManager.Instance;
        
            var possibleBlockCount = Random.Range(gManager.MinBlocksInLevel, gManager.MaxBlocksInLevel);
            var maxBlockCountInRow = Screen.width / (Block.Width + gManager.DistanceBetweenBlocks);
            var rowCount = possibleBlockCount > maxBlockCountInRow? possibleBlockCount/maxBlockCountInRow : 1;
            
            for (var i = 0; i < rowCount; i++)
            {
                var positions = RandomPositions(1, maxBlockCountInRow);
                
                foreach (var positionNumber in positions)
                {
                    var position = new Vector2(
                        _startPosX + gManager.DistanceBetweenBlocks*positionNumber + Block.Width * positionNumber, 
                         _startPosY - gManager.DistanceBetweenBlocks*i - Block.Height*i);
                    var blockHp = Random.Range(1, gManager.MaxBlockHp + 1);
                    
                    var block = PoolManager.Instance.BlockPool.GetObject();
                    block.Initialize(position, blockHp, gManager.MaxBlockHp, gManager.HpGradient);
                    block.ObjectDeactivation += GameManager.Instance.CheckWin;
                }
            }
            
            rectTransform.localScale = _originalScale;
            rectTransform.position = _originalPos + Vector3.up * 1000f;
            rectTransform.DOMove(_originalPos, 2f);
        }

        private IEnumerable<int> RandomPositions(int minPositions, int maxPositions)
        {
            var positions = new List<int>();
            for (var i = 0; i < maxPositions; i++)
                positions.Add(i);
        
            var posToDeleteCount = maxPositions - Random.Range(minPositions, maxPositions);
            for (var i = 0; i < posToDeleteCount; i++)
                positions.Remove(Random.Range(0, positions.Count));

            return positions;
        }

        public void DestroyLevel()
        {
            rectTransform.DOScaleX(0, 1f).
                OnComplete(PoolManager.Instance.BlockPool.DeactivateAllObjects);
        }
    }
}
