using DG.Tweening;
using UnityEngine;

namespace Animations
{
    public class AnimScale : MonoBehaviour
    {
        [SerializeField] private bool onStart = false;
        [SerializeField] private float scaleMultiplier = 1.1f;
        [SerializeField] private float duration = 1f;
        [SerializeField] private int loops = -1;
        [SerializeField] private LoopType loopType = LoopType.Yoyo;

        private Tween _anim;
    
        void Start()
        {
            if(onStart) Play();
        }

        public void Play()
        {
            Play(scaleMultiplier, duration, loops, loopType);
        }
    
        public void Play(float scaleMultiplier, float duration, int loops,LoopType loopType)
        {
            if (_anim.IsActive()) Stop();
        
            _anim = transform.DOScale(transform.localScale * scaleMultiplier,duration).SetLoops(loops,loopType);
        }

        public void Stop()
        {
            _anim.Kill();
        }
    }
}
