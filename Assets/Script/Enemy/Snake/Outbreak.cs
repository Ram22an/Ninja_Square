using DG.Tweening;
using UnityEngine;

public class Outbreak : MonoBehaviour
{
    [SerializeField]
    private Transform Snake;
    public void Awake()
    {
        Snake = transform.Find("Snake");
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Snake.transform.DOLocalMove(new Vector3(5.29f, -2f, 0f), 0.5f).SetEase(Ease.InOutQuad); ;
        }
    }
}
