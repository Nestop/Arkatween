using System;
using UnityEngine;
using UnityEngine.UI;
using Utils.Pool;
using Random = UnityEngine.Random;

namespace Game.Objects
{
    public class BallController : MonoBehaviour, IDeactivable
    {
        public event Action<IDeactivable> ObjectDeactivation;
        public Image Image => image;
        public Color OrigColor { get; private set; }
        public const float OrigSpeedMultiplier = 1f;

        [SerializeField] private float speed = 200;
        [SerializeField] private float maxSpeed = 600;
        [SerializeField] private Rigidbody2D rigidBody;
        [SerializeField] private Image image;
    
        private Vector2 _direction;
        private float _startSpeed;
        private float _speedMultiplier = OrigSpeedMultiplier;
        private bool _isActive;
        private bool _racketPlatform;

        private void Awake()
        {
            _startSpeed = speed;
            OrigColor = image.color;
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
            ObjectDeactivation?.Invoke(this);
        }

        private void FixedUpdate()
        {
            if(!_isActive) return;

            rigidBody.velocity = rigidBody.velocity.normalized * (Mathf.Clamp(speed,200, maxSpeed) * _speedMultiplier);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            other.gameObject.GetComponent<IHitable>()?.MakeHit(this);
        }

        public void IncreaseSpeedBy(float value)
        {
            speed = Mathf.Min(speed + value, maxSpeed);
        }

        public void SetSpeedMultiplier(float multiplier)
        {
            _speedMultiplier = multiplier;
        }
    }
}