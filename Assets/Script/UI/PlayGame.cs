using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;
public class PlayGame : MonoBehaviour
{
    [SerializeField]
    private GameObject CastelPanel;
    [SerializeField]
    private GameObject BulletPanel;
    [SerializeField]
    private GameObject PlayerPanel;
    [SerializeField]
    private GameObject SettingPanel;
    [SerializeField]
    private GameObject AllSkinButton;
    [SerializeField]
    private GameObject SettingButton;
    [SerializeField] 
    private GameObject AddDiamondAdPanel;
    [SerializeField] 
    private GameObject AddDiamondButton;
    [SerializeField]
    private TextMeshProUGUI DiamondText;
    [SerializeField]
    private GameObject CustomizePanel;
    [SerializeField]
    private GameObject LevelRenderer;
    [SerializeField]
    private GameObject TapToPlay;
    public void Awake()
    {
        CastelPanel.SetActive(false);
        BulletPanel.SetActive(false);
        PlayerPanel.SetActive(false);
        SettingPanel.SetActive(false);
        AddDiamondAdPanel.SetActive(false);
        CustomizePanel.SetActive(false);
    }
    public void Update()
    {
        int temp = GameContentReaderAndSetter.Instance.GameContentDaimondGetterAndSetter;
        DiamondText.text =temp.ToString();
    }
    //tap to play function
    public void OnClickPlay()
    {
        if (GameContentReaderAndSetter.Instance.GameVibrationGetterAndSetter)
        {
            Handheld.Vibrate();
        }
        SceneManager.LoadScene("Game");
    }
    //all skin button
    public void OpenCustomizePanel()
    {
        AnimateOpenPanel(CustomizePanel,0.9f,0.5f);
        LevelRenderer.SetActive(false);
        AllSkinButton.SetActive(false);
        SettingButton.SetActive(false);
        AddDiamondButton.SetActive(false);
        TapToPlay.SetActive(false);
    }
    public void CloseCustomizePanel()
    {
        AnimateClosePanel(CustomizePanel);
        LevelRenderer.SetActive(true);
        AllSkinButton.SetActive(true);
        SettingButton.SetActive(true);
        AddDiamondButton.SetActive(true);
        TapToPlay.SetActive(true);
    }
    //setting button
    public void OpenSettingPanel()
    {
        AnimateOpenPanel(SettingPanel,0.8f,0.7f);
        AllSkinButton.SetActive(false);
        SettingButton.SetActive(false);
        AddDiamondButton.SetActive(false);
        TapToPlay.SetActive(false);
        LevelRenderer.SetActive(false);
    }
    //exit the setting panel
    public void ExitTheSettingPanel()
    {
        AnimateClosePanel(SettingPanel);
        AllSkinButton.SetActive(true);
        SettingButton.SetActive(true);
        AddDiamondButton.SetActive(true);
        TapToPlay.SetActive(true);
        LevelRenderer.SetActive(true);
    }
    //exit the player panel
    public void OpenThePlayerPanel()
    {
        AnimateOpenPanel(PlayerPanel);
        CustomizePanel.SetActive(false);
    }
    public void ExitThePlayerPanel()
    {
        NewAnimateClosePanel(PlayerPanel, () => AnimateOpenPanel(CustomizePanel,0.9f,0.5f));
    }
    //exit the bullet panel 
    public void OpenTheBulletPanel()
    {
        AnimateOpenPanel(BulletPanel);
        CustomizePanel.SetActive(false);
    }
    public void ExitTheBulletPanel()
    {
        NewAnimateClosePanel(BulletPanel, () => AnimateOpenPanel(CustomizePanel, 0.9f, 0.5f));
    }
    //exit the castel panel 
    public void OpenTheCastelPanel()
    {
        AnimateOpenPanel(CastelPanel);
        CustomizePanel.SetActive(false);
    }
    public void ExitTheCastelPanel()
    {
        NewAnimateClosePanel(CastelPanel, () => AnimateOpenPanel(CustomizePanel, 0.9f, 0.5f));
    }



    //these two are for animation do not change them
    public void AnimateOpenPanel(GameObject panel,float x=0.9f,float y=0.9f)
    {
        panel.SetActive(true);
        panel.transform.localScale = Vector3.zero;
        panel.transform.DOScale(new Vector3(x,y,1f), 0.5f).SetEase(Ease.OutBack);
    }
    public void AnimateClosePanel(GameObject panel)
    {
        panel.transform.DOScale(Vector3.zero, 0.5f)
            .SetEase(Ease.InBack)
            .OnComplete(() => panel.SetActive(false));
    }
    //public void NewAnimateClosePanel(GameObject panel, System.Action onComplete = null)
    //{
    //    panel.transform.DOScale(Vector3.zero, 0.5f)
    //        .SetEase(Ease.InBack)
    //        .OnComplete(() => {
    //            panel.SetActive(false);
    //            onComplete?.Invoke();
    //        });
    //}
    public void NewAnimateClosePanel(GameObject panel, System.Action onComplete = null)
    {
        panel.transform.DOScale(Vector3.zero, 0.5f)
            .SetEase(Ease.InBack)
            .OnComplete(() => {
                panel.SetActive(false);
                panel.transform.localScale = Vector3.one; // Reset scale
                onComplete?.Invoke();
            });
    }

    //Diamond set active
    public void OpenAddDiamondPanel()
    {
        AnimateOpenPanel(AddDiamondAdPanel,0.8f,0.4f);
        AllSkinButton.SetActive(false);
        SettingButton.SetActive(false);
        AddDiamondButton.SetActive(false);
        TapToPlay.SetActive(false);
        LevelRenderer.SetActive(false);
    }
    public void CloseAddDiamondPanel()
    {
        AnimateClosePanel(AddDiamondAdPanel);
        AllSkinButton.SetActive(true);
        SettingButton.SetActive(true);
        AddDiamondButton.SetActive(true);
        TapToPlay.SetActive(true);
        LevelRenderer.SetActive(true);
    }
    public void ShowAdFromAddDiamond()
    {
        if (GameContentReaderAndSetter.Instance.GameVibrationGetterAndSetter)
        {
            Handheld.Vibrate();
        }
        RewardAd.Instance.ShowRewardedAd();
        GameContentReaderAndSetter.Instance.GameContentDaimondGetterAndSetter = 3;
        int temp = GameContentReaderAndSetter.Instance.GameContentDaimondGetterAndSetter;
        DiamondText.text = temp.ToString();
        CloseAddDiamondPanel();
    }

}//class
