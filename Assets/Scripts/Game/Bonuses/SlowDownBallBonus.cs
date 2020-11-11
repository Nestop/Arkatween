using DG.Tweening;
using Game.Managers;
using UnityEngine;

namespace Game.Bonuses
{
    public class SlowDownBallBonus : BaseBonus
    {
        private float _speedMultiplier = 1f;
        
        public override void ActivateBonus()
        {
            var ball = GameManager.Instance.Ball;
            
            var anim = DOTween.Sequence();
            anim.Append(ball.Image.DOColor(Color.cyan, 0.3f));
            anim.Join(
                DOTween.To(
                    () => _speedMultiplier,
                    value =>
                    {
                        _speedMultiplier = value;
                        ball.SetSpeedMultiplier(_speedMultiplier);
                    },
                    0.6f, 0.3f));
            anim.AppendInterval(5f);
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
        }
    }
}