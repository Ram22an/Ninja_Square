using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using static Unity.Collections.AllocatorManager;
using static UnityEngine.RuleTile.TilingRuleOutput;
using Unity.VisualScripting;
using UnityEngine.UI;

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
    public void Start()
    {
        
        //inputActions = new ActionMapPlayer();
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
            AddingBloack();
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
 
        //if(NumberOfBlock == 0)
        //{
        //    PlayerWalking.Play();
        //}
        //else
        //{
        //    PlayerWalking.Stop();
        //}
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
    public void AddingBloack()
    {
        if (inputActions.Player.enabled && inputActions.Player.Jump.triggered && NumberOfBlock < 3)
        {
            //Debug.Log(GameContentReaderAndSetter.Instance.GameVibrationGetterAndSetter);
            if (GameContentReaderAndSetter.Instance.GameVibrationGetterAndSetter)
            {
                Handheld.Vibrate();
            }
            CanDeployBlock = false;
            playerRb.AddForce(Vector2.zero, ForceMode2D.Impulse);
            instantiateBlockCoroutine = StartCoroutine(InstantiateBlock(0.05f));
            StartCoroutine(CoolDown(0.5f));
            NumberOfBlock += 1;
        }
    }
    private IEnumerator InstantiateBlock(float time)
    {
        yield return new WaitForSeconds(time);
        Instantiate(Block, instantiatePosition.transform.position, instantiatePosition.transform.rotation, transform);
    }
    private IEnumerator CoolDown(float time)
    {
        yield return new WaitForSeconds(time);
        CanDeployBlock=true;
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
