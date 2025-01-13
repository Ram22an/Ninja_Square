using UnityEngine;
public class PositionChecker : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.gameObject.tag == "Player")
        {
            if ((int)collision.gameObject.transform.position.y==(int)transform.position.y)
            {
                if (!BulletShooter.instance.isShooting)  
                {
                    BulletShooter.instance.streak += 1;
                }
            }
        }
    }
}
