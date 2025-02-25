using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelection : MonoBehaviour
{
    [SerializeField]
    private PlayerSelectedOrNot playerSelectedOrNot;
    public List<Button> Children = new List<Button>();
    [SerializeField]
    private SpriteRenderer Player;
    public static PlayerSelection instance;
    public void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        instance = this;
        if (playerSelectedOrNot.Unlocked.Count<=0) 
        {
            playerSelectedOrNot.Unlocked.Clear();
            playerSelectedOrNot.Unlocked.Add(0);
        }
        foreach(Transform Child in transform)
        {
            if (!Child.CompareTag("ExitButton"))
            {
                Button childButton = Child.GetComponent<Button>();
                Children.Add(childButton);
                //childButton.onClick.AddListener(() => OnButtonClicked(childButton));
            }
        }
        GameObject LockOf0 = Children[0].transform.Find("Lock")?.gameObject;
        LockOf0.SetActive(false);
        //GameObject PlayerType = Children[0].transform.Find("Player")?.gameObject;
        //SpriteRenderer spriteRendererRealPlayer = Player.GetComponent<SpriteRenderer>();
        //Image spriteRendererTypePlayer = PlayerType.GetComponent<Image>();
        //Player.sprite = spriteRendererTypePlayer.sprite;
        for (int i = 0; i < Children.Count-2; i++)
        {
            if (!playerSelectedOrNot.Unlocked.Contains(i))
            {
                GameObject LockObj= Children[i].transform.Find("Lock").gameObject;
                LockObj.SetActive(true);
                GameObject PlayerObj = Children[i].transform.Find("Player").gameObject;
                PlayerObj.SetActive(false);
            }
            else
            {
                GameObject LockObj = Children[i].transform.Find("Lock").gameObject;
                LockObj.SetActive(false);
                GameObject PlayerObj = Children[i].transform.Find("Player").gameObject;
                PlayerObj.SetActive(true);
            }
        }
        if (playerSelectedOrNot.WatchedAdFor7)
        {
            GameObject temp = Children[6].transform.Find("Lock")?.gameObject;
            temp.SetActive(false);
        }
        else
        {
            GameObject temp = Children[6].transform.Find("Player")?.gameObject;
            temp.SetActive(false);
        }
        if (playerSelectedOrNot.WatchedAdFor8)
        {
            GameObject temp = Children[7].transform.Find("Lock")?.gameObject;
            temp.SetActive(false);
        }
        else
        {
            GameObject temp = Children[7].transform.Find("Player")?.gameObject;
            temp.SetActive(false);
        }
    }
    public void Update()
    {
        for (int i = 0; i < playerSelectedOrNot.RequiredGems.Count; i++)
        {
            if (!playerSelectedOrNot.Unlocked.Contains(i) && 
                GameContentReaderAndSetter.Instance.GameContentDaimondGetterAndSetter >= playerSelectedOrNot.RequiredGems[i])
            {
                GoldLockItem(i);
            }
        }
    }
    public void GoldLockItem(int index)
    {
        GameObject lockObject = Children[index].transform.Find("Lock")?.gameObject;
        GameObject LockIcon = lockObject.transform.Find("LockIcon")?.gameObject;

        if (LockIcon != null)
        {
            //Outline image = lockObject.GetComponent<Outline>();
            //image.effectColor=Color.green;
            //image.effectDistance = new Vector2(2f, 2f);
            Image LockImage=LockIcon.GetComponent<Image>();
            Color32 myYellow = new Color32(255, 255, 0, 255);
            LockImage.color = myYellow;
        }
    }

    public void PlayerSelectionOnClick(Button clickedButton)
    {
        string buttonName = clickedButton.gameObject.name;
        Debug.Log($"Button clicked: {buttonName[buttonName.Length - 1]}");
        int number = int.Parse(buttonName[buttonName.Length - 1].ToString());
        if (!playerSelectedOrNot.Unlocked.Contains(number) &&
                GameContentReaderAndSetter.Instance.GameContentDaimondGetterAndSetter >= playerSelectedOrNot.RequiredGems[number])
        {
            GameContentReaderAndSetter.Instance.GameContentDaimondGetterAndSetter = -playerSelectedOrNot.RequiredGems[number];
            playerSelectedOrNot.Unlocked.Add(number);
            GameObject lockObject = Children[number].transform.Find("Lock")?.gameObject;
            lockObject.SetActive(false);
            GameObject PlayerType = Children[number].transform.Find("Player")?.gameObject;
            PlayerType.SetActive(true);
            //SpriteRenderer spriteRendererRealPlayer = Player.GetComponent<SpriteRenderer>();
            Image spriteRendererTypePlayer=PlayerType.GetComponent<Image>();
            Player.sprite = spriteRendererTypePlayer.sprite;
            GameContentReaderAndSetter.Instance.GameContentPlayerSkinGetterAndSetter = spriteRendererTypePlayer.sprite;
        }
        if (playerSelectedOrNot.Unlocked.Contains(number))
        {
            GameObject PlayerType = Children[number].transform.Find("Player")?.gameObject;
            PlayerType.SetActive(true);
            Image spriteRendererTypePlayer = PlayerType.GetComponent<Image>();
            Player.sprite = spriteRendererTypePlayer.sprite;
            GameContentReaderAndSetter.Instance.GameContentPlayerSkinGetterAndSetter = spriteRendererTypePlayer.sprite;
        }
        if (GameContentReaderAndSetter.Instance.GameVibrationGetterAndSetter)
        {
            Handheld.Vibrate();
        }
    }
    public void ButtonFor7()
    {
        if (!playerSelectedOrNot.WatchedAdFor7)
        {
            RewardAd.Instance.ShowRewardedAd();
        }
        playerSelectedOrNot.WatchedAdFor7 = true;
        GameObject temp = Children[6].transform.Find("Lock")?.gameObject;
        temp.SetActive(false);
        GameObject temp2 = Children[6].transform.Find("Player")?.gameObject;
        temp2.SetActive(true);
        Image temp2Image = temp2.GetComponent<Image>();
        Player.sprite = temp2Image.sprite;
        GameContentReaderAndSetter.Instance.GameContentPlayerSkinGetterAndSetter = temp2Image.sprite;
        if (GameContentReaderAndSetter.Instance.GameVibrationGetterAndSetter)
        {
            Handheld.Vibrate();
        }
    }
    public void ButtonFor8()
    {
        if (!playerSelectedOrNot.WatchedAdFor8)
        {
            RewardAd.Instance.ShowRewardedAd();
        }
        GameObject temp = Children[7].transform.Find("Lock")?.gameObject;
        temp.SetActive(false);
        GameObject temp2 = Children[7].transform.Find("Player")?.gameObject;
        temp2.SetActive(true);
        Image temp2Image = temp2.GetComponent<Image>();
        Player.sprite = temp2Image.sprite;
        GameContentReaderAndSetter.Instance.GameContentPlayerSkinGetterAndSetter = temp2Image.sprite;
        playerSelectedOrNot.WatchedAdFor8 = true;
        if (GameContentReaderAndSetter.Instance.GameVibrationGetterAndSetter)
        {
            Handheld.Vibrate();
        }
    }


}//class
