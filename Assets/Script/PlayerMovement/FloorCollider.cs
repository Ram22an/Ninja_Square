using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorCollider : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private GameObject DeadPlayer;
    private Vector3 Temp;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Spike")
        {
            Destroy(GameContentReaderAndSetter.Instance.CurrentDeadPlayer);
            GameContentReaderAndSetter.Instance.CurrentDeadPlayer = Instantiate(GameContentReaderAndSetter.Instance.ReUseDeadPlayer, Player.transform.position, Quaternion.identity);
            GameContentReaderAndSetter.Instance.SetDeathCountOfPlayer(1);
            Temp =PlayerMovement.Instance.transform.position;
            PlayerParent.Instance.FinalSpeed = -5;
            StartCoroutine(SentBack(0.5f));
            PlayerParent.Instance.gravity = 0;
            PlayerMovement.Instance.CanDeployBlockChecking = false;
            PlayerMovement.Instance.GameOver.SetActive(true);          
            BulletShooter.instance.StopAllCoroutines();
        }
    }
    IEnumerator SentBack(float timer)
    {
        yield return new WaitForSeconds(timer);
        PlayerParent.Instance.FinalSpeed = 0;
        Player.transform.parent = null;
        StartCoroutine(SettingDeadPlayerPosition(0.5f));
    }
    IEnumerator SettingDeadPlayerPosition(float timer)
    {
        yield return new WaitForSeconds(timer);
        GameContentReaderAndSetter.Instance.SetDeadPlayerPosition(Temp);
    }
}
