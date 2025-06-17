using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseAndPlayPanel : MonoBehaviour
{
    [SerializeField] private Button HomeButton;
    [SerializeField] private Button RestartButton;
    public GameObject htpPanel;
    
    public void Awake()
    {
        HomeButton.onClick.AddListener(ForHomeButton);
        RestartButton.onClick.AddListener(ForRestartButton);
      
    }
    public void Resume() { 
    Time.timeScale = 1.0f;
    }
    void Update()
    {
        Debug.Log("the time scale is " + Time.timeScale);
    }
    public void ForHomeButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void ForRestartButton()
    {
        InterstitialScriptAd.Instance.ShowInterstitialAd("Game");
    }
}
