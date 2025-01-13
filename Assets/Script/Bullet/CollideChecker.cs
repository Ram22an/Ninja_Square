using UnityEngine;

public class CollideChecker : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacles")
        {
            collision.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }    
    }
    public void OnBecameInvisible()
    {
        
        Destroy(gameObject);
    }
}
