using System.Collections;
using UnityEngine;
public class BulletShooter : MonoBehaviour
{
    [HideInInspector]
    public int streak;
    [HideInInspector]
    public bool isShooting;
    public static BulletShooter instance;
    private int counting;
    [SerializeField]
    private GameObject bullet;
    private float BulletSpeed = 12f;
    private Coroutine bulletCoroutine;
    private float BulletAngle;
    public void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        counting = 0;
        isShooting = false;
        streak = 0;
    }
    public void Start()
    {
        
        int index = UIElementSetter.Instance.AllBullet.IndexOf(GameContentReaderAndSetter.Instance.GameContentBulletSkinGetterAndSetter);
        BulletAngle = UIElementSetter.Instance.Angle[index];
    }
    public void Update()
    {
        if (!isShooting && streak==3)
        {
            streak = 0;
            isShooting=true;
            bulletCoroutine = StartCoroutine(BulletCoroutine());
            //StartCoroutine(BulletCoroutine());
        }
    }
    //IEnumerator BulletCoroutine()
    //{
    //    if (counting <= 10)
    //    {
    //        GameObject BulletPrrefab=Instantiate(bullet,transform.position,transform.rotation);
    //        Rigidbody2D bulletRigid=BulletPrrefab.GetComponent<Rigidbody2D>();
    //        bulletRigid.AddForce(Vector2.right * BulletSpeed, ForceMode2D.Impulse);
    //        yield return new WaitForSeconds(0.2f);
    //        counting += 1;
    //        StartCoroutine(BulletCoroutine());
    //    }
    //    if (counting >10) 
    //    {
    //        counting = 0;
    //        isShooting=false;
    //        yield break;
    //    }

    //}
    IEnumerator BulletCoroutine()
    {
        while (counting <= 10)
        {
            GameObject BulletPrrefab = Instantiate(bullet, transform.position, Quaternion.Euler(0f,0f,BulletAngle));
            Rigidbody2D bulletRigid = BulletPrrefab.GetComponent<Rigidbody2D>();
            bulletRigid.AddForce(Vector2.right * BulletSpeed, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.2f);
            counting += 1;
        }

        // Reset after firing 10 bullets
        counting = 0;
        isShooting = false;
    }
    public void StopBulletCoroutine()
    {
        if (bulletCoroutine != null)
        {
            StopCoroutine(bulletCoroutine);
            bulletCoroutine = null; // Clear the reference
            isShooting = false; // Reset shooting state
            counting = 0; // Reset counting
        }
    }
}//class
