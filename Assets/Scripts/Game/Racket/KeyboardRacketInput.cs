using UnityEngine;

namespace Game.Racket
{
    public class KeyboardRacketInput : BaseRacketInput
    {
        [SerializeField] private float movementSpeed = 50;
        [SerializeField] private float rotationSpeed = 100;
        [SerializeField] private float maxRotationAngle = 35; 
        
        public override Vector2 GetPosition()
        {
            var worldPos = Transform.position;
            var delta = Input.GetAxis("Horizontal");
            var x = Mathf.Clamp(worldPos.x + (delta * movementSpeed), LeftBorder, RightBorder);
            
            var position = new Vector2(x, worldPos.y);

            return Mathf.Abs(delta) > 0f ? position : (Vector2)worldPos;
        }

        public override Quaternion GetRotation()
        {
            var locRot = Transform.localRotation;
            var delta = Input.GetAxis("Horizontal");

            if (Mathf.Abs(delta) > 0f)
            {
                var rotation = Quaternion.Slerp(locRot, Quaternion.Euler(Vector3.forward * (-Mathf.Sign(delta) * rotationSpeed)) * locRot, 0.05f);
                var angle = rotation.eulerAngles.z;
                angle = angle < 180f ? angle : Mathf.Abs(angle - 360f);
                
                if (angle < maxRotationAngle)
                {
                    return rotation;
                }
            }
            else
            {
                var rotation = Quaternion.Slerp(locRot, Quaternion.identity, 0.15f);
                return rotation;
            }

            return locRot;
        }
    }
}