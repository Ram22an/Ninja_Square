using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
public class GameOverPanelInMainGame : MonoBehaviour
{
    [SerializeField]
    private Image GameOver;
    [SerializeField]
    private Image HomeButton;
    [SerializeField]
    private Image TryAgainButton;
    public void Start()
    {
        if (GameOver == null|| HomeButton == null|| TryAgainButton == null) 
        {
            Debug.LogError("One or more UI elements are null. Check your references.");
            return;
        }
        AnimateComponents();
    }

    private void AnimateComponents()
    {

        // Reset animation states for clean entry
        ResetAnimationStates();

        // Animate GameOver (scaling up with bounce effect)
        GameOver.transform.DOScale(new Vector3(13f, 10f, 1f), 1f)
            .From(Vector3.zero)
            .SetEase(Ease.OutBounce);

        // Animate Home Button (sliding in from the left)
        HomeButton.transform.DOLocalMoveX(386f, 0.5f)
            .From(new Vector3(1200f, HomeButton.transform.localPosition.y, 0f)) // Adjust initial position
            .SetEase(Ease.OutExpo)
            .SetDelay(0.5f);

        // Animate Try Again Button (sliding in from the right)
        TryAgainButton.transform.DOLocalMoveX(-394f, 0.5f)
            .From(new Vector3(-1200f, TryAgainButton.transform.localPosition.y, 0f)) // Adjust initial position
            .SetEase(Ease.OutExpo)
            .SetDelay(0.5f);

        // Fade in animations
        GameOver.DOFade(1f, 1f).From(0f).SetDelay(0.3f);
        HomeButton.DOFade(1f, 0.25f).From(0f).SetDelay(0.5f);
        TryAgainButton.DOFade(1f, 0.25f).From(0f).SetDelay(0.5f);
        if (GameContentReaderAndSetter.Instance.GameVibrationGetterAndSetter)
        {
            Handheld.Vibrate();
        }
    }

    private void ResetAnimationStates()
    {
        // Reset the scale for GameOver
        GameOver.transform.localScale = Vector3.zero;

        // Reset transparency for GameOver, HomeButton, and TryAgainButton
        ResetImageAlpha(GameOver);
        ResetImageAlpha(HomeButton);
        ResetImageAlpha(TryAgainButton);
    }

    private void ResetImageAlpha(Image image)
    {
        Color color = image.color;
        color.a = 0f; // Set alpha to fully transparent
        image.color = color;
    }


    public void ReplayGameButton()
    {
        InterstitialScriptAd.Instance.ShowInterstitialAd("Game");
        //SceneManager.LoadScene("Game");
    }
    public void HomeButtonLoadScene()
    {
        InterstitialScriptAd.Instance.ShowInterstitialAd("MainMenu");
        //SceneManager.LoadScene("MainMenu");
    }
}
