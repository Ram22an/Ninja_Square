using UnityEngine;
using UnityEngine.UI;

public class LevelComplete : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem ConfettiSystem;
    [SerializeField]
    private Image Badge;
    [SerializeField]
    private Sprite Bronze;
    [SerializeField]
    private Sprite Sliver;
    [SerializeField]
    private Sprite Golden;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Handheld.Vibrate();
            if (ConfettiSystem != null)
                ConfettiSystem.Play();
            else
                Debug.LogWarning("Confetti System is not assigned!");
            PlayerMovement.Instance.LevelCompleted.SetActive(true);
            Badge = GameObject.Find("BadgeColor").GetComponent<Image>();
            PlayerMovement.Instance.CanDeployBlock = false;
            PlayerParent.Instance.FinalSpeed = 0f;
            PlayerMovement.Instance.CanDeployBlockChecking = false;
            LevelManager.Instance.SaveLevelData(
            LevelManager.Instance.GetCurrentLevel() + 1,
            LevelManager.Instance.GetLevelId());
            int deathCount = GameContentReaderAndSetter.Instance.DeathCountGiver();
            if (deathCount > 7)
            {
                Badge.sprite = Bronze;
                GameContentReaderAndSetter.Instance.GameContentDaimondGetterAndSetter = 1;
            }
            else if (deathCount >= 5 && deathCount <= 7)
            {
                Badge.sprite = Sliver;
                GameContentReaderAndSetter.Instance.GameContentDaimondGetterAndSetter = 2;
            }
            else if (deathCount >= 0 && deathCount <= 4)
            {
                Badge.sprite = Golden;
                GameContentReaderAndSetter.Instance.GameContentDaimondGetterAndSetter = 3;
            }
            GameContentReaderAndSetter.Instance.SetDeadPlayerPosition(Vector3.zero);
        }
    }
}
