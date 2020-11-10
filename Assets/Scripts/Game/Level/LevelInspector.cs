using System.Collections.Generic;
using DG.Tweening;
using Game.Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Level
{
    public class LevelInspector : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;

        private GameManager _gManager;
        private Vector2 _blockScale;
        private float _startPosX;
        private float _startPosY;
        private Vector3 _originalPos;
        private Vector3 _originalScale;
        
        private void Awake()
        {
            _gManager = GameManager.Instance;
            
            _blockScale = new Vector2(Block.Width, Block.Height);
            var blockWidth = (float)(Screen.width - _gManager.DistanceBetweenBlocks * (_gManager.MaxBlocksInRow + 1)) / _gManager.MaxBlocksInRow;
            _blockScale *= blockWidth / Block.Width;

            _startPosX = -Screen.width / 2f + _blockScale.x / 2f + _gManager.DistanceBetweenBlocks;
            _startPosY = Screen.height / 2f - _blockScale.y / 2f - _gManager.DistanceBetweenBlocks;
            _originalPos = rectTransform.position;
            _originalScale = rectTransform.localScale;
        }

        public void OpenLevel(LevelData level)
        {
            var rows = level.blockMap.GetLength(0);
            var columns = level.blockMap.GetLength(1);
            
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    if (level.blockMap[i,j] == null) continue;
                    
                    var position = new Vector2(
                        _startPosX + _gManager.DistanceBetweenBlocks*j + _blockScale.x * j, 
                        _startPosY - _gManager.DistanceBetweenBlocks*i - _blockScale.y * i);
                    var blockHp = level.blockMap[i, j].blockHp;
                    var bonus = level.blockMap[i, j].bonus;
                    
                    var block = PoolManager.Instance.BlockPool.GetObject();
                    block.Initialize(position, blockHp, _gManager.MaxBlockHp, _gManager.HpGradient, bonus);
                    block.transform.localScale = _blockScale;
                    block.ObjectDeactivation += GameManager.Instance.CheckWin;
                }
            }
            
            rectTransform.localScale = _originalScale;
            rectTransform.position = _originalPos + Vector3.up * 1000f;
            rectTransform.DOMove(_originalPos, 2f);
        }

        public void CloseLevel()
        {
            rectTransform.DOScaleX(0, 1f).
                OnComplete(PoolManager.Instance.BlockPool.DeactivateAllObjects);
        }
    }
}
