using DG.Tweening;
using Game.Managers;
using UnityEngine;

namespace Game.Bonuses
{
    public class NarrowRacketBonus : BaseBonus
    {
        public override void ActivateBonus()
        {
            var racket = GameManager.Instance.Racket;
            var t = racket.transform;
            t.DOScale(t.localScale - Vector3.right*10f, 1f);
            racket.Image.DOColor(Color.red, 0.33f)
                .SetLoops(4, LoopType.Yoyo)
                .OnComplete((() => racket.Image.color = Color.white));
        }
    }
}