using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareFloorCollider : MonoBehaviour
{
    [SerializeField]
    private GameObject Parent;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Spike")
        {
            //BoxCollider2D temp = Parent.GetComponent<BoxCollider2D>();
            //temp.enabled = false;
            PlayerMovement.Instance.NumberOfBlock -= 1;
            if (PlayerMovement.Instance.NumberOfBlock < 0)
            {
                PlayerMovement.Instance.NumberOfBlock = 0;
            }
            Parent.transform.parent = null;
            StartCoroutine(DestoryParent(0.5f));  
        }
    }
    IEnumerator DestoryParent(float timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(Parent);
    }
}
