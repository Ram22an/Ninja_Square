using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivate : MonoBehaviour
{
    public void OnBecameInvisible()
    {
        PlayerMovement.Instance.NumberOfBlock -= 1;
        if (PlayerMovement.Instance.NumberOfBlock < 0)
        {
            PlayerMovement.Instance.NumberOfBlock = 0;
        }
        Destroy(gameObject);
    }
}
