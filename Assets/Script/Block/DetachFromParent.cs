using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DetachFromParent : MonoBehaviour
{
    [SerializeField]
    private GameObject Parent;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacles")
        {
            //BoxCollider2D temp=Parent.GetComponent<BoxCollider2D>();
            //temp.enabled=false;
            PlayerMovement.Instance.NumberOfBlock -= 1;
            if (PlayerMovement.Instance.NumberOfBlock < 0)
            {
                PlayerMovement.Instance.NumberOfBlock = 0;
            }
            Parent.transform.parent = null;
        }
    }

}
