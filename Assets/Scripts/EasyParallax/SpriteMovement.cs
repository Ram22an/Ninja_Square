using UnityEngine;
using UnityEngine.UIElements;

namespace EasyParallax
{
    /**
     * Moves a sprite along the X axis using a predefined speed
     */
    public class SpriteMovement : MonoBehaviour
    {
        public MovementSpeedType movementSpeedType;

        [Tooltip("Used only if no movement speed type is specified")]
        public float speed = 1f;

        private void Awake()
        {
            if (movementSpeedType)
                speed = movementSpeedType.speed;
            Vector3 temp=new Vector3(transform.position.x, transform.position.y, 0);
            transform.position=temp;
            Debug.Log(transform.position);
        }

        private void Update()
        {
            // Save the current position, so we can edit it
            var newPosition = transform.position;

            // Move the position along the x axis by an amount that depends on the
            // defined speed and the deltaTime, so we can get a framerate-independent movement
            newPosition.x -= speed * Time.deltaTime;

            // Ensure Z position remains at 0
            newPosition.z = 0;

            // Update our position
            transform.position = new Vector3(newPosition.x, newPosition.y, 0);
        }
    }
}
