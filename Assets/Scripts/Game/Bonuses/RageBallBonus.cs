using System.Collections.Generic;
using DG.Tweening;
using Game.Managers;
using UnityEngine;

namespace Game.Bonuses
{
    public class RageBallBonus : BaseBonus
    {
        private float _speedMultiplier = 1f;
        private readonly List<Block> _blocks;

        public RageBallBonus()
        {
            _blocks = PoolManager.Instance.BlockPool.Objects;
        }
        
        public override void ActivateBonus()
        {
            foreach (var block in _blocks)
                block.Collider2D.isTrigger = true;
            
            var ball = GameManager.Instance.Ball;
            
            var anim = DOTween.Sequence();
            anim.Append(ball.Image.DOColor(Color.red, 0.3f));
            anim.Join(
                DOTween.To(
                    () => _speedMultiplier,
                    value =>
                    {
                        _speedMultiplier = value;
                        ball.SetSpeedMultiplier(_speedMultiplier);
                    },
                    1.5f, 0.3f));
            anim.AppendInterval(6f);
            anim.Append(ball.Image.DOColor(ball.OrigColor, 0.3f));
            anim.Join(
                DOTween.To(
                    () => _speedMultiplier,
                    value =>
                    {
                        _speedMultiplier = value;
                        ball.SetSpeedMultiplier(_speedMultiplier);
                    },
                    1f, 0.3f));
            anim.OnComplete((() =>
            {
                foreach (var block in _blocks)
                    block.Collider2D.isTrigger = false;
            }));
        }
    }
}