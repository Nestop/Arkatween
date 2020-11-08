using UnityEngine;

namespace Game.Racket
{
    public class MouseRacketInput : BaseRacketInput
    {
        private float _lastX;

        protected override void OnAwake()
        {
            _lastX = Input.mousePosition.x;
        }

        public override Vector2 GetPosition()
        {
            var x = Input.mousePosition.x;
            x = Mathf.Clamp(x, LeftBorder, RightBorder);

            var worldPos = Transform.position;

            return new Vector2(x, worldPos.y);
        }

        public override Quaternion GetRotation()
        {
            var x = Input.mousePosition.x;
            x = Mathf.Clamp(x, LeftBorder, RightBorder);

            var delta = x - _lastX;

            var locRot = Transform.localRotation;

            var rotation = Mathf.Abs(delta) < 0.01f ? 
                Quaternion.Slerp(locRot, Quaternion.identity, 0.15f) : 
                Quaternion.Slerp(locRot, Quaternion.Euler(Vector3.forward * -delta) * locRot, 0.15f);

            _lastX = x;

            return rotation;
        }
    }
}