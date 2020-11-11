using Const;
using Game.Managers;
using UnityEngine;
using UnityEngine.UI;
using Utility = UnityEditorInternal.InternalEditorUtility;
using Random = UnityEngine.Random;

namespace Game
{
    public class BallLogic : MonoBehaviour
    {
        public Image Image => image;
        public Color OrigColor => _origColor;
        
        [SerializeField] private float speed = 200;
        [SerializeField] private float maxSpeed = 600;
        [SerializeField] private Rigidbody2D rigidBody;
        [SerializeField] private Image image;
    
        private Vector2 _direction;
        private float _startSpeed;
        private float _speedMultiplier = 1f;
        private bool _isActive;
        private bool _racketPlatform;
        private Color _origColor;

        private void Awake()
        {
            _startSpeed = speed;
            _origColor = image.color;
        }

        public void EnableLogic()
        {
            _isActive = true;
            rigidBody.simulated = true;
            
            speed = _startSpeed;
            var randomDirectionAngle = Random.Range(45, 135);
            _direction = Quaternion.Euler(Vector3.forward * randomDirectionAngle) * Vector2.right;
            
            rigidBody.AddForce(_direction*speed, ForceMode2D.Impulse);
        }
        
        public void DisableLogic()
        {
            _isActive = false;
            rigidBody.simulated = false;
            rigidBody.velocity = Vector2.zero;
        }

        private void FixedUpdate()
        {
            if(!_isActive) return;

            rigidBody.velocity = rigidBody.velocity.normalized * (speed * _speedMultiplier);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(Utility.tags[TagConst.BlockId]))
            {
                speed = Mathf.Min(speed + 20f, maxSpeed);
                
                other.gameObject.GetComponent<Block>()?.MakeHit();
            }
            else if (other.gameObject.CompareTag(Utility.tags[TagConst.LoseZoneId]))
            {
                GameManager.Instance.Restart();
            }
        }

        public void SetSpeedMultiplier(float multiplier)
        {
            _speedMultiplier = multiplier;
        }
    }
}