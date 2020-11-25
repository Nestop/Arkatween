using DG.Tweening;
using Game.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class Score : MonoBehaviour
    {
        [SerializeField] private Text scoreLabel;

        private Vector3 _originalScale;
        private Color _originalColor;
        private Sequence _animBigScore;
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
            _originalScale = _transform.localScale;
            _originalColor = scoreLabel.color;
        }

        public void Show(int score)
        {
            scoreLabel.text = $"Score: {score}";
            PlayAnimation(score);
        }

        private void PlayAnimation(int score)
        {
            if (score % 10 == 0)
            {
                transform.SetParent(GameManager.Instance.LayerFront);
            
                _animBigScore = DOTween.Sequence();
                _animBigScore.Append(_transform.DOScale(_originalScale * 1.6f, 0.1f));
                _animBigScore.Join(scoreLabel.DOColor(Color.white, 0.1f));
                _animBigScore.Append(_transform.DOScale(_originalScale, 1.4f));
                _animBigScore.Join(scoreLabel.DOColor(_originalColor, 1.4f));
            }
            else if(!_animBigScore.IsActive())
            {
                _transform.SetParent(GameManager.Instance.LayerBack);
                _transform.localScale = _originalScale;
            
                var anim = DOTween.Sequence();
                anim.Append(_transform.DOScale(_originalScale * 0.9f, 0.1f));
                anim.Append(_transform.DOScale(_originalScale, 0.6f));
            }
        }
    }
}
