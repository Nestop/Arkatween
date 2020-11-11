using System;
using Const;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Utils.Pool;
using Utility = UnityEditorInternal.InternalEditorUtility;

namespace Game.Bonuses
{
    public class BonusTrigger : MonoBehaviour, IDeactivable
    {
        public event Action<IDeactivable> ObjectDeactivation;
        
        [SerializeField] private Image _image;
        
        private BaseBonus _bonus;


        public void Initialize(Vector3 position, BaseBonus bonus)
        {
            transform.position = position;
            _bonus = bonus;

            var origColor = _image.color;
            _image.color = Color.clear;
            _image.DOColor(origColor, 0.4f);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(Utility.tags[TagConst.RacketPlatform]))
            {
                _bonus?.ActivateBonus();
                ObjectDeactivation?.Invoke(this);
            }
            else
            if (other.gameObject.CompareTag(Utility.tags[TagConst.LoseZoneId]))
            {
                ObjectDeactivation?.Invoke(this);
            }
        }

        private void Update()
        {
            var t = transform;
            var p = t.localPosition;
            p.y -= 100f*Time.deltaTime;
            t.localPosition = p;
        }
    }
}