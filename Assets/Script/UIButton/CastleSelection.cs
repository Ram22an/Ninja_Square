using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class CastleSelection : MonoBehaviour
{
    [SerializeField]
    private CastleSelectedOrNot CastleSelectedOrNot;
    public List<Button> Children = new List<Button>();
    [SerializeField]
    private SpriteRenderer Castle;
    public static CastleSelection instance;
    public void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        instance = this;
        if (CastleSelectedOrNot.Unlocked.Count <= 0)
        {
            CastleSelectedOrNot.Unlocked.Clear();
            CastleSelectedOrNot.Unlocked.Add(0);
        }
        foreach (Transform Child in transform)
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
        //GameObject PlayerType = Children[0].transform.Find("Bullet")?.gameObject;
        //SpriteRenderer spriteRendererRealPlayer = Player.GetComponent<SpriteRenderer>();
        //Image spriteRendererTypePlayer = PlayerType.GetComponent<Image>();
        //Bullet.sprite = spriteRendererTypePlayer.sprite;
        for (int i = 0; i < Children.Count - 2; i++)
        {
            if (!CastleSelectedOrNot.Unlocked.Contains(i))
            {
                GameObject LockObj = Children[i].transform.Find("Lock").gameObject;
                LockObj.SetActive(true);
                GameObject PlayerObj = Children[i].transform.Find("Castle").gameObject;
                PlayerObj.SetActive(false);
            }
            else
            {
                GameObject LockObj = Children[i].transform.Find("Lock").gameObject;
                LockObj.SetActive(false);
                GameObject PlayerObj = Children[i].transform.Find("Castle").gameObject;
                PlayerObj.SetActive(true);
            }
        }
        if (CastleSelectedOrNot.WatchedAdFor7)
        {
            GameObject temp = Children[6].transform.Find("Lock")?.gameObject;
            temp.SetActive(false);
        }
        else
        {
            GameObject temp = Children[6].transform.Find("Castle")?.gameObject;
            temp.SetActive(false);
        }
        if (CastleSelectedOrNot.WatchedAdFor8)
        {
            GameObject temp = Children[7].transform.Find("Lock")?.gameObject;
            temp.SetActive(false);
        }
        else
        {
            GameObject temp = Children[7].transform.Find("Castle")?.gameObject;
            temp.SetActive(false);
        }
    }
    public void Update()
    {
        for (int i = 0; i < CastleSelectedOrNot.RequiredGems.Count; i++)
        {
            if (!CastleSelectedOrNot.Unlocked.Contains(i) &&
                GameContentReaderAndSetter.Instance.GameContentDaimondGetterAndSetter >= CastleSelectedOrNot.RequiredGems[i])
            {
                GreenLockItem(i);
            }
        }
    }
    public void GreenLockItem(int index)
    {
        //GameObject lockObject = Children[index].transform.Find("Lock")?.gameObject;
        GameObject lockObject = Children[index].transform.Find("Lock")?.gameObject;
        GameObject LockIcon = lockObject.transform.Find("LockIcon")?.gameObject;

        //if (lockObject != null)
        //{
        //    Outline image = lockObject.GetComponent<Outline>();
        //    image.effectColor = Color.green;
        //    image.effectDistance = new Vector2(2f, 2f);
        //}
        if (LockIcon != null)
        {
            //Outline image = lockObject.GetComponent<Outline>();
            //image.effectColor=Color.green;
            //image.effectDistance = new Vector2(2f, 2f);
            Image LockImage = LockIcon.GetComponent<Image>();
            Color32 myYellow = new Color32(255, 255, 0, 255);
            LockImage.color = myYellow;
        }
    }

    public void PlayerSelectionOnClick(Button clickedButton)
    {
        string buttonName = clickedButton.gameObject.name;
        Debug.Log($"Button clicked: {buttonName[buttonName.Length - 1]}");
        int number = int.Parse(buttonName[buttonName.Length - 1].ToString());
        if (!CastleSelectedOrNot.Unlocked.Contains(number) &&
                GameContentReaderAndSetter.Instance.GameContentDaimondGetterAndSetter >= CastleSelectedOrNot.RequiredGems[number])
        {
            GameContentReaderAndSetter.Instance.GameContentDaimondGetterAndSetter = -CastleSelectedOrNot.RequiredGems[number];
            CastleSelectedOrNot.Unlocked.Add(number);
            GameObject lockObject = Children[number].transform.Find("Lock")?.gameObject;
            lockObject.SetActive(false);
            GameObject PlayerType = Children[number].transform.Find("Castle")?.gameObject;
            PlayerType.SetActive(true);
            //SpriteRenderer spriteRendererRealPlayer = Player.GetComponent<SpriteRenderer>();
            Image spriteRendererTypePlayer = PlayerType.GetComponent<Image>();
            Castle.sprite = spriteRendererTypePlayer.sprite;
            GameContentReaderAndSetter.Instance.GameContentCastleSkinGetterAndSetter = spriteRendererTypePlayer.sprite;
        }
        if (CastleSelectedOrNot.Unlocked.Contains(number))
        {
            GameObject PlayerType = Children[number].transform.Find("Castle")?.gameObject;
            PlayerType.SetActive(true);
            Image spriteRendererTypePlayer = PlayerType.GetComponent<Image>();
            Castle.sprite = spriteRendererTypePlayer.sprite;
            GameContentReaderAndSetter.Instance.GameContentCastleSkinGetterAndSetter = spriteRendererTypePlayer.sprite;
        }
        if (GameContentReaderAndSetter.Instance.GameVibrationGetterAndSetter)
        {
            Handheld.Vibrate();
        }
    }
    public void ButtonFor7()
    {
        if (!CastleSelectedOrNot.WatchedAdFor7)
        {
            RewardAd.Instance.ShowRewardedAd();
        }
        CastleSelectedOrNot.WatchedAdFor7 = true;
        GameObject temp = Children[6].transform.Find("Lock")?.gameObject;
        temp.SetActive(false);
        GameObject temp2 = Children[6].transform.Find("Castle")?.gameObject;
        temp2.SetActive(true);
        Image temp2Image = temp2.GetComponent<Image>();
        Castle.sprite = temp2Image.sprite;
        GameContentReaderAndSetter.Instance.GameContentCastleSkinGetterAndSetter = temp2Image.sprite;
        if (GameContentReaderAndSetter.Instance.GameVibrationGetterAndSetter)
        {
            Handheld.Vibrate();
        }
    }
    public void ButtonFor8()
    {
        if (!CastleSelectedOrNot.WatchedAdFor8)
        {
            RewardAd.Instance.ShowRewardedAd();
        }
        CastleSelectedOrNot.WatchedAdFor8 = true;
        GameObject temp = Children[7].transform.Find("Lock")?.gameObject;
        temp.SetActive(false);
        GameObject temp2 = Children[7].transform.Find("Castle")?.gameObject;
        temp2.SetActive(true);
        Image temp2Image = temp2.GetComponent<Image>();
        Castle.sprite = temp2Image.sprite;
        GameContentReaderAndSetter.Instance.GameContentCastleSkinGetterAndSetter = temp2Image.sprite;
        if (GameContentReaderAndSetter.Instance.GameVibrationGetterAndSetter)
        {
            Handheld.Vibrate();
        }
    }
}
