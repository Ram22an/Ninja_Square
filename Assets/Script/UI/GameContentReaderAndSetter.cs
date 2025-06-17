using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
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
    public GameObject CurrentDeadPlayer;
    public GameObject ReUseDeadPlayer;
    public GameObject htpPanel;
    
    private string jsonFilePath => Application.dataPath + "/Data/GameContent.json";



    private const string SaveKey = "GameContentData";

    private void OnEnable()
    {
        if (File.Exists(jsonFilePath))
        {
            string json = File.ReadAllText(jsonFilePath);
            SerializableGameContent data = JsonUtility.FromJson<SerializableGameContent>(json);
            LoadGameContentFromSerializable(data);
            Debug.Log("Loaded GameContent from JSON file.");
        }
        else
        {
            Debug.Log("No save file found. Using default GameContent.");
        }
    }

    private void OnDisable()
    {
        // Ensure directory exists
        string folder = Path.GetDirectoryName(jsonFilePath);
        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
            Debug.Log("Created missing folder: " + folder);
        }

        SerializableGameContent data = ConvertToSerializableGameContent();
        string json = JsonUtility.ToJson(data, true); // pretty print
        File.WriteAllText(jsonFilePath, json);
        Debug.Log("Saved GameContent to: " + jsonFilePath);
    }



    private SerializableGameContent ConvertToSerializableGameContent()
    {
        return new SerializableGameContent
        {
            death = GameContent.death,
            diamond = GameContent.diamond,
            Sound = GameContent.Sound,
            SoundValue = GameContent.SoundValue,
            Vibrate = GameContent.Vibrate,
            PlayerDeadPositon = ConvertToSerializableVector3(GameContent.PlayerDeadPositon),
        };
    }

    private void LoadGameContentFromSerializable(SerializableGameContent data)
    {
        GameContent.death = data.death;
        GameContent.diamond = data.diamond;
        GameContent.Sound = data.Sound;
        GameContent.SoundValue = data.SoundValue;
        GameContent.Vibrate = data.Vibrate;
        GameContent.PlayerDeadPositon = ConvertToUnityVector3(data.PlayerDeadPositon);
    }








    public void Start()
    {
        if (PlayerPrefs.GetInt("htp") == 0)
        {
            htpPanel.SetActive(true);
            PlayerPrefs.SetInt("htp", 1);
        }
    }
    public void PauseOrPlay(float t) { 
    Time.timeScale = t;
    }
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
        ReUseDeadPlayer = DeadPlayer;
        if(Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;
        if (GameContent.PlayerDeadPositon != Vector3.zero) 
        {
            CurrentDeadPlayer=Instantiate(DeadPlayer,GameContent.PlayerDeadPositon,Quaternion.identity);
        }

    }
    public void Update()
    {
        if (PlayerParent.Instance != null)
        {
            if (PlayerParent.Instance.transform.position.x > GameContent.PlayerDeadPositon.x && Mathf.Abs(PlayerParent.Instance.transform.position.x - GameContent.PlayerDeadPositon.x) < 1f&& GameContent.PlayerDeadPositon!=Vector3.zero)
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



    private SerializableVector3 ConvertToSerializableVector3(Vector3 vec)
    {
        return new SerializableVector3
        {
            x = vec.x,
            y = vec.y,
            z = vec.z
        };
    }

    private Vector3 ConvertToUnityVector3(SerializableVector3 vec)
    {
        return new Vector3(vec.x, vec.y, vec.z);
    }

}//class
