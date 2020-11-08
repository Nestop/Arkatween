using UnityEngine;

namespace Game.Racket
{
    public class RacketController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rigidBody2D;
        [SerializeField] private BaseRacketInput racketInput;

        private void FixedUpdate()
        {
            rigidBody2D.MovePosition(racketInput.GetPosition());
            rigidBody2D.MoveRotation(racketInput.GetRotation());
        }
    }
}
