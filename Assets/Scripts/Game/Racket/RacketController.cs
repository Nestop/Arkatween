using UnityEngine;
using UnityEngine.UI;

namespace Game.Racket
{
    public class RacketController : MonoBehaviour
    {
        public Image Image => image;
        
        [SerializeField] private Rigidbody2D rigidBody2D;
        [SerializeField] private BaseRacketInput racketInput;
        [SerializeField] private Image image;

        private void FixedUpdate()
        {
            rigidBody2D.MovePosition(racketInput.GetPosition());
            rigidBody2D.MoveRotation(racketInput.GetRotation());
        }
    }
}
