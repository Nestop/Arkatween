using System;
using DG.Tweening;
using UnityEngine;

namespace Game.Bonuses.Animation
{
    public static class SpeedBallBonusAnimation
    {
        private const float TransitionTime = 0.3f;

        public static Sequence Play(float speedMultiplier, float duration, BallController ball, Color bonusColor,
            Func<float> speedMultiplierGetter, Action<float> speedMultiplierSetter)
        {
            var anim = DOTween.Sequence();

            anim.Append(ball.Image.DOColor(bonusColor, TransitionTime));
            anim.Join(
                DOTween.To(
                    speedMultiplierGetter.Invoke,
                    value => speedMultiplierSetter?.Invoke(value),
                    speedMultiplier, TransitionTime));

            anim.AppendInterval(duration);

            anim.Append(ball.Image.DOColor(ball.OrigColor, TransitionTime));
            anim.Join(
                DOTween.To(
                    speedMultiplierGetter.Invoke,
                    value => speedMultiplierSetter?.Invoke(value),
                    BallController.OrigSpeedMultiplier, TransitionTime));
            return anim;
        }
    }
}