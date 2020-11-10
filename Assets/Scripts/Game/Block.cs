﻿using System;
using DG.Tweening;
using Game.Bonuses;
using Game.Managers;
using UnityEngine;
using UnityEngine.UI;
using Utils.Pool;

namespace Game
{
    public class Block : MonoBehaviour, IDeactivable
    {
        public const int Width = 80;
        public const int Height = 30;
        
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private Image image;
        
        private int _hp;
        private int _maxHp;
        private Gradient _hpGradient;
        private BaseBonus _bonus;

        public event Action<IDeactivable> ObjectDeactivation;

        public void Initialize(Vector2 position, int hp, int maxHp, Gradient hpGradient, BaseBonus bonus = null)
        {
            transform.localScale = new Vector3(Width, Height, 0);
            rectTransform.anchoredPosition = position;
            _hp = hp;
            _maxHp = maxHp;
            _hpGradient = hpGradient;
            image.color = _hpGradient.Evaluate((float)_hp/_maxHp);
            _bonus = bonus;
        }

        public void MakeHit()
        {
            _hp--;
            if (_hp > 0)
            {
                var color = _hpGradient.Evaluate((float) _hp / _maxHp);
                image.DOColor(color, 0.3f).SetLoops(3,LoopType.Yoyo);
            }
            else
            {
                transform.DOScaleX(0,1f)
                    .OnComplete( () =>
                    {
                        if(_bonus != null)
                            PoolManager.Instance.BonusPool.GetObject().Initialize(transform.position, _bonus);
                        ObjectDeactivation?.Invoke(this);
                    });
            }
        }

        public void PlayPanicAnimation()
        {
            var anim = DOTween.Sequence();
            anim.Append(transform.DORotate(Vector3.forward * 10f, 0.5f));
            anim.Append(transform.DORotate(Vector3.down * 10f, 1f));
            anim.SetLoops(-1, LoopType.Yoyo);
        }
    }
}