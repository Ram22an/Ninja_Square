using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseAndPlayPanel : MonoBehaviour
{
    [SerializeField] private Button HomeButton;
    [SerializeField] private Button RestartButton;
    public void Awake()
    {
        HomeButton.onClick.AddListener(ForHomeButton);
        RestartButton.onClick.AddListener(ForRestartButton);
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
