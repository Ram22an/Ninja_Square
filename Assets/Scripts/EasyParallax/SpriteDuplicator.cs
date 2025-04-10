using DG.Tweening;
using UnityEngine;

namespace EasyParallax
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteDuplicator : MonoBehaviour
    {
        [SerializeField] private int poolSize = 5;
        [SerializeField] private int spriteRepositionIndex = 2;
        [SerializeField] private float spriteRepositionCorrection = 0.03f;

        private Transform[] duplicatesPool;
        private float spriteWidth;

        private void Start()
        {
            duplicatesPool = new Transform[poolSize];
            spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
            duplicatesPool[0] = transform;

            // **Ensure the original object has Z set to 0**
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);

            var startingPos = transform.position;

            for (var i = 1; i < poolSize; i++)
            {
                var position = new Vector3(CalculateX(startingPos), startingPos.y, 0);
                startingPos = position;
                duplicatesPool[i] = Instantiate(gameObject, position, Quaternion.identity, transform.parent).transform;

                // **Ensure duplicate objects have Z set to 0**
                duplicatesPool[i].position = new Vector3(duplicatesPool[i].position.x, duplicatesPool[i].position.y, 0);

                Destroy(duplicatesPool[i].GetComponent<SpriteDuplicator>());
            }
        }

        private void Update()
        {
            foreach (var duplicate in duplicatesPool)
            {
                if (duplicate.position.x < -spriteWidth * spriteRepositionIndex)
                {
                    var rightmostSprite = GetRightMostSprite();
                    var startingPos = rightmostSprite.position;
                    var newPosition = new Vector3(CalculateX(startingPos), startingPos.y, 0);

                    // **Explicitly set Z = 0 again**
                    duplicate.position = new Vector3(newPosition.x, newPosition.y, 0);
                }
                else
                {
                    // **Ensure Z is always 0**
                    duplicate.position = new Vector3(duplicate.position.x, duplicate.position.y, 0);
                }

                if (duplicate.position.z != 0)
                {
                    Debug.LogError($"Duplicate {duplicate.name} has incorrect Z position: {duplicate.position.z}");
                }
            }
        }

        private float CalculateX(Vector3 startingPos)
        {
            return Mathf.FloorToInt(startingPos.x + spriteWidth) -
                   spriteRepositionCorrection * transform.lossyScale.magnitude;
        }

        private Transform GetRightMostSprite()
        {
            var rightmostX = Mathf.NegativeInfinity;
            Transform rightmostSprite = null;
            foreach (var duplicate in duplicatesPool)
            {
                if (duplicate.position.x > rightmostX)
                {
                    rightmostSprite = duplicate;
                    rightmostX = duplicate.position.x;
                }
            }
            return rightmostSprite;
        }
    }
}
