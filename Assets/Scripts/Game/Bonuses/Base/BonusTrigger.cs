using System;
using Const;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Utils.Pool;
using Utility = UnityEditorInternal.InternalEditorUtility;

namespace Game.Bonuses.Base
{
    public class BonusTrigger : MonoBehaviour, IDeactivable
    {
        private Bonus _bonus;

        [SerializeField] private Image image;
        public event Action<IDeactivable> ObjectDeactivation;

        public void Initialize(Vector3 position, Bonus bonus)
        {
            transform.position = position;
            _bonus = bonus;

            var origColor = image.color;
            image.color = Color.clear;
            image.DOColor(origColor, 0.4f);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(Utility.tags[TagConst.RacketPlatform]))
            {
                _bonus?.ActivateBonus();
                ObjectDeactivation?.Invoke(this);
            }
            else if (other.gameObject.CompareTag(Utility.tags[TagConst.LoseZoneId]))
            {
                ObjectDeactivation?.Invoke(this);
            }
        }

        private void Update()
        {
            var t = transform;
            var p = t.localPosition;
            p.y -= 100f * Time.deltaTime;
            t.localPosition = p;
        }
    }
}