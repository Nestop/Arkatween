using DG.Tweening;
using Game.Racket;
using UnityEngine;

namespace Game.Bonuses.Animation
{
    public static class WidthRacketBonusAnimation
    {
        private const float ColorLoopDuration = 0.33f;
        private const int ColorYoYoLoops = 2;
        private const float ScaleResizingDuration = 1f;

        public static void Play(RacketController racket, float resizingValue, Color bonusColor)
        {
            var t = racket.transform;
            t.DOScale(t.localScale + Vector3.right * resizingValue, ScaleResizingDuration);
            racket.Image.DOColor(bonusColor, ColorLoopDuration)
                .SetLoops(ColorYoYoLoops * 2, LoopType.Yoyo)
                .OnComplete(() => racket.Image.color = Color.white);
        }
    }
}