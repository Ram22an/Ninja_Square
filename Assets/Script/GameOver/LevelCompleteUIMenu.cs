using DG.Tweening;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.UI;
public class LevelCompleteUIMenu : MonoBehaviour
{
    [SerializeField]
    private Image HomeImage;
    [SerializeField]
    private Image NextImage;
    [SerializeField]
    private Image LevelCompleteImage;
    [SerializeField]
    private Image BadgeImage;
   
    private Image PanelImage;
    public void Awake()
    {
        PanelImage = GetComponent<Image>();
    }

    public void Start()
    {
        Debug.Log("From level complete ui");
        // there are error in this script please check again it is related to the on enable
        
        if (PanelImage == null || HomeImage == null || NextImage == null ||
            LevelCompleteImage == null || BadgeImage == null)
        {
            Debug.LogError("One or more UI elements are null. Check your references.");
            return;
        }
        AnimateComponents();
    }
    private void AnimateComponents()
    {
        ResetAnimationStates();
        PanelImage.DOFade(0.196078431372549f, 1f).SetDelay(0.5f);
        LevelCompleteImage.transform.DOScale(new Vector3(18.1025f, 8.043412f, 0f), 1f).SetEase(Ease.OutBounce);

        BadgeImage.DOFade(1f, 1f).SetDelay(0.5f);

        HomeImage.transform.DOLocalMoveX(386f, 1f).From().SetEase(Ease.OutExpo).SetDelay(0.5f);

        NextImage.transform.DOLocalMoveX(-394f, 1f).From().SetEase(Ease.OutExpo).SetDelay(0.5f);
        if (GameContentReaderAndSetter.Instance.GameVibrationGetterAndSetter)
        {
            Handheld.Vibrate();
        }

    }
    private void ResetAnimationStates()
    {
        Color temp = PanelImage.color;
        temp.a = 0f;
        PanelImage.color = temp;
        LevelCompleteImage.transform.localScale = Vector3.zero;
        Color badgeColor = BadgeImage.color;
        badgeColor.a = 0f;
        BadgeImage.color = badgeColor;
    }
    public void HomeButton()
    {
        InterstitialScriptAd.Instance.ShowInterstitialAd("MainMenu");
        //SceneManager.LoadScene("MainMenu");
    }
    public void NextGameButton()
    {
        GameContentReaderAndSetter.Instance.SetDeathCountOfPlayer(0, 0);
        Destroy( GameObject.Find("DeadPlayer(Clone)"));

        if (PlayerPrefs.GetInt("LevelAd") == 0)
        {
            PlayerPrefs.SetInt("LevelAd", 1);
        }
        else { 
            InterstitialScriptAd.Instance.ShowInterstitialAd("Game");
            PlayerPrefs.SetInt("LevelAd", 0);
        }
        //SceneManager.LoadScene("Game");
    }

}//class
