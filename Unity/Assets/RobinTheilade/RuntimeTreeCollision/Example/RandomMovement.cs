using UnityEngine;
using Random = UnityEngine.Random;

namespace RobinTheilade.RuntimeTreeCollision.Example
{
    /// <summary>
    /// Moves the player around and changes direction upon collision.
    /// </summary>
    [AddComponentMenu("Physics/Runtime Tree Colliders - Example - Random Movement")]
    [RequireComponent(typeof(Rigidbody))]
    public class RandomMovement : MonoBehaviour
    {
        /// <summary>
        /// The speed of which the player moves (default 1.0).
        /// </summary>
        [Tooltip("The speed of which the player moves (default: 1.0).")]
        public float speed = 1.0f;

        /// <summary>
        /// The direction the player moves in.
        /// </summary>
        private Vector3 direction;

        /// <summary>
        /// Reference to the player's rigidbody component.
        /// </summary>
        private new Rigidbody rigidbody;

        /// <summary>
        /// Assigns a start direction and caches the reference to the player's rigidbody.
        /// </summary>
        private void Start()
        {
            this.direction = RandomDirection();
            this.rigidbody = this.GetComponent<Rigidbody>();
        }

        /// <summary>
        /// Moves the player.
        /// </summary>
        private void FixedUpdate()
        {
            this.rigidbody.MovePosition(this.transform.position + this.direction * this.speed * Time.deltaTime);
        }

        /// <summary>
        /// Changes direction upon collision.
        /// </summary>
        /// <param name="collision">
        /// Information about the collision.
        /// </param>
        private void OnCollisionEnter(Collision collision)
        {
            this.direction = RandomDirection();
        }

        /// <summary>
        /// Returns a new normalized random direction.
        /// The Y component is always 0.0.
        /// </summary>
        /// <returns>
        /// A random direction.
        /// </returns>
        private static Vector3 RandomDirection()
        {
            return new Vector3(
                Random.value * 2.0f - 1.0f,
                0.0f,
                Random.value * 2.0f - 1.0f
                ).normalized;
        }
    }
}
