using UnityEngine;
using UnityEngine.UI;
public class PauseAndPlay : MonoBehaviour
{
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private GameObject TouchPanel; 
    private bool isPaused = false;
    private Button PauseButton;
    public void Awake()
    {
        PauseButton = GetComponent<Button>();
        PauseButton.onClick.AddListener(TogglePause);
        PausePanel.SetActive(false);
    }
    public void TogglePause()
    {
        TouchPanel.SetActive(isPaused);
        isPaused = !isPaused;
        PausePanel.SetActive(isPaused);
        Time.timeScale=isPaused ? 0.0f : 1.0f;
        Debug.Log("Game " + (isPaused ? "Paused" : "Resumed"));
    }
}
