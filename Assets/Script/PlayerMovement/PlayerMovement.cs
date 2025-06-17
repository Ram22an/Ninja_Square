using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    public ActionMapPlayer inputActions;
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private SpriteRenderer PlayerSprite;
    [SerializeField]
    private GameObject Block;
    [SerializeField]
    private GameObject instantiatePosition;
    [SerializeField]
    public bool CanDeployBlock = true;
    [SerializeField]
    public GameObject GameOver;
    [SerializeField]
    public GameObject LevelCompleted;
    [SerializeField]
    private SpriteRenderer PlayerFace;
    public bool CanDeployBlockChecking;
    public int NumberOfBlock;
    private Rigidbody2D playerRb;
    public static PlayerMovement Instance;
    private float initialRelativeX;
    private Coroutine instantiateBlockCoroutine;
    public AudioClip popSound;
    private float lastBlockTime = 0f;
    private const float BLOCK_COOLDOWN = 0.2f;

    public void Start()
    {
        playerRb = Player.GetComponent<Rigidbody2D>();
        initialRelativeX = transform.position.x - transform.parent.position.x;
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        GameOver.SetActive(false);
        LevelCompleted.SetActive(false);
        CanDeployBlockChecking = true;
        NumberOfBlock = 0;
        PlayerFace.sprite = PlayerSprite.sprite;
    }

    public void OnEnable()
    {
        if (inputActions == null)
        {
            inputActions = new ActionMapPlayer(); 
        }
        inputActions.Player.Enable();
    }

    public void OnDisable()
    {
        inputActions.Player.Disable();
    }

    public void Update()
    {
        if (CanDeployBlockChecking)
        {
            HandleBlockInput();
            CheckingXAxis();
        }
        else
        {
            StopAllCoroutines();
        }

        if (inputActions.Player.Restart.triggered)
        {
            SceneManager.LoadScene("Game");
        }
        if (transform.position.y <= -2)
        {
            SceneManager.LoadScene("Game");
        }
    }

    private void HandleBlockInput()
    {
        if (inputActions.Player.Jump.triggered && NumberOfBlock < 8)
        {
            float currentTime = Time.time;
            if (currentTime - lastBlockTime >= BLOCK_COOLDOWN)
            {
                TryAddBlock();
                lastBlockTime = currentTime;
            }
        }
    }

    private void TryAddBlock()
    {
        if (!CanDeployBlockChecking || NumberOfBlock >= 8) return;

        if (GameContentReaderAndSetter.Instance.GameVibrationGetterAndSetter)
        {
            Handheld.Vibrate();
        }

        CanDeployBlock = false;
        playerRb.AddForce(Vector2.zero, ForceMode2D.Impulse);
        instantiateBlockCoroutine = StartCoroutine(InstantiateBlock(0.05f));
        StartCoroutine(CoolDown(0.5f));
        NumberOfBlock++;
    }

    public void CheckingXAxis()
    {
        if (transform.parent != null)
        {
            float currentRelativeX = transform.position.x - transform.parent.position.x;
            if (!Mathf.Approximately(initialRelativeX, currentRelativeX))
            {
                transform.position = new Vector2(transform.parent.position.x + initialRelativeX, transform.position.y);
            }
        }
    }

    private IEnumerator InstantiateBlock(float time)
    {
        yield return new WaitForSeconds(time);
        Instantiate(Block, instantiatePosition.transform.position, instantiatePosition.transform.rotation, transform);
        PlayerSound.instance.GetComponentInParent<AudioSource>().PlayOneShot(popSound);
    }

    private IEnumerator CoolDown(float time)
    {
        yield return new WaitForSeconds(time);
        CanDeployBlock = true;
    }

    public void StopBlockCoroutine()
    {
        if (instantiateBlockCoroutine != null)
        {
            StopCoroutine(instantiateBlockCoroutine);
            instantiateBlockCoroutine = null;
        }
    }
}//class
