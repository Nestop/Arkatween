using UnityEngine;

namespace Game.Racket
{
    public abstract class BaseRacketInput : MonoBehaviour
    {
        [SerializeField] private RacketController racketController;
        
        protected Transform Transform;
        protected float LeftBorder;
        protected float RightBorder;

        private void Awake()
        {
            Transform = racketController.transform;
            var halfScaleX = Transform.localScale.x / 2f;
            LeftBorder = halfScaleX;
            RightBorder = Screen.width - halfScaleX;
            
            OnAwake();
        }

        protected virtual void OnAwake(){}

        public abstract Vector2 GetPosition();
        public abstract Quaternion GetRotation();
    }
}