using UnityEngine;

namespace Game
{
    public class RacketController : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private Rigidbody2D rigidBody2D;
        
        private float _leftBorder;
        private float _rightBorder;
        private float _lastX;
    
        private void Awake()
        {
            var halfScaleX = transform.localScale.x / 2f;
            _leftBorder = halfScaleX;
            _rightBorder = Screen.width - halfScaleX;
            _lastX = Input.mousePosition.x;
        }
        
        private void FixedUpdate()
        {
            var x = Input.mousePosition.x;
            x = Mathf.Clamp(x, _leftBorder, _rightBorder);

            var delta = x - _lastX;

            var locRot = rectTransform.localRotation;
            var worldPos = rectTransform.position;
            
            var rotation = Mathf.Abs(delta) < 0.01f ? 
                Quaternion.Slerp(locRot, Quaternion.identity, 0.15f) : 
                Quaternion.Slerp(locRot, Quaternion.Euler(Vector3.forward * -delta) * locRot, 0.15f);
            var position = Vector2.Lerp(worldPos, new Vector2(x, worldPos.y), 0.1f);

            rigidBody2D.MovePosition(position);
            rigidBody2D.MoveRotation(rotation);

            _lastX = x;
        }
    }
}
