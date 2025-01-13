using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameContentReaderAndSetter : MonoBehaviour
{
    [SerializeField]
    private GameContent GameContent;
    [SerializeField]
    private GameObject DeadPlayer;
    public static GameContentReaderAndSetter Instance;
    private bool DeathIstakeAviod;
    private AudioSource BackGroundSong;
    public float GameSoundVolumeGetterAndSetter
    {
        get => GameContent.SoundValue;
        set => GameContent.SoundValue = value;
    }
    public bool GameSoundGetterAndSetter
    {
        get => GameContent.Sound;
        set=> GameContent.Sound = value;
    }
    public bool GameVibrationGetterAndSetter
    {
        get=>GameContent.Vibrate;
        set=> GameContent.Vibrate = value;
    }
    public void Awake()
    {
        BackGroundSong = gameObject.AddComponent<AudioSource>();
        DeathIstakeAviod = true;
        if(Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;
        if (GameContent.PlayerDeadPositon != Vector3.zero) 
        {
            Instantiate(DeadPlayer,GameContent.PlayerDeadPositon,Quaternion.identity);
        }
    }
    public void Update()
    {
        if (PlayerParent.Instance != null)
        {
            if (Mathf.Abs(PlayerParent.Instance.transform.position.x - GameContent.PlayerDeadPositon.x) < 0.5f&& GameContent.PlayerDeadPositon!=Vector3.zero)
            {
                PlayJujustuSoundFromGameContentRNS();
            }
            //else
            //{
            //    PlayerParent.Instance.StopJujustuSound();
            //}
        }

    }
    public void SetDeadPlayerPosition(Vector3 Position)
    {
        GameContent.PlayerDeadPositon=Position;
    }
    public void PlayJujustuSoundFromGameContentRNS()
    {
        PlayerParent.Instance.PlayJujustuSound();
    }
    public void SetDeathCountOfPlayer(int number,float timer=0.5f)
    {
        if (DeathIstakeAviod)
        {
            DeathIstakeAviod = false;
            if (number > 0)
            {
                GameContent.death += 1;
            }
            else if (number == 0)
            {
                GameContent.death = 0;
            }
            StartCoroutine(DeathMistakeAviodCoroutine(timer));
        }
    }
    IEnumerator DeathMistakeAviodCoroutine(float timer)
    {
        yield return new WaitForSeconds(0.5f);
        DeathIstakeAviod=true;
    }
    public int DeathCountGiver()
    {
        return GameContent.death;
    }
    public void AddDiamond(int Number)
    {
        GameContent.diamond += Number;
    }
    public int GameContentDaimondGetterAndSetter
    {
        get
        {
            return GameContent.diamond;
        }
        set
        {
            GameContent.diamond += value;
        }
    }
    public Sprite GameContentPlayerSkinGetterAndSetter
    {
        get 
        {
            return GameContent.PlayerSkin;
        }
        set
        {
            GameContent.PlayerSkin = value;
        }
    }
    public Sprite GameContentBulletSkinGetterAndSetter
    {
        get
        {
            return GameContent.BulletSkin;
        }
        set
        {
            GameContent.BulletSkin = value;
        }
    }
    public Sprite GameContentCastleSkinGetterAndSetter
    {
        get
        {
            return GameContent.CastelSkin;
        }
        set
        {
            GameContent.CastelSkin = value;
        }
    }
}//class
