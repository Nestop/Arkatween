using Const;
using Game.Managers;
using UnityEngine;
using Utility = UnityEditorInternal.InternalEditorUtility;
using Random = UnityEngine.Random;

namespace Game
{
    public class BallLogic : MonoBehaviour
    {
        [SerializeField] private float speed = 200;
        [SerializeField] private float maxSpeed = 600;
        [SerializeField] private Rigidbody2D rigidBody;
    
        private Vector2 _direction;
        private float _startSpeed;
        private bool _isActive;
        private bool _racketPlatform;

        private void Awake()
        {
            _startSpeed = speed;
        }

        public void EnableLogic()
        {
            _isActive = true;
            rigidBody.simulated = true;
            
            speed = _startSpeed;
            var randomDirectionAngle = Random.Range(45, 135);
            _direction = Quaternion.Euler(Vector3.forward * randomDirectionAngle) * Vector2.right;
            rigidBody.velocity = _direction*speed;
            
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
            
            rigidBody.velocity = _direction*speed;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(Utility.tags[TagConst.RacketPlatform]))
            {
                _direction = Vector2.Reflect(_direction, other.GetContact(0).normal).normalized;
                _racketPlatform = true;
            }
            else _racketPlatform = false;

            _direction = !_racketPlatform? Vector2.Reflect(_direction, other.GetContact(0).normal).normalized : _direction;

            var hit = Physics2D.Raycast(transform.position, _direction);
            if (hit.collider != null && hit.collider.CompareTag(Utility.tags[TagConst.BlockId]))
            {
                hit.collider.gameObject.GetComponent<Block>()?.PlayPanicAnimation();
            }

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
    }
}