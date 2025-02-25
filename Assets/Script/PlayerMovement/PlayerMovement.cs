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
    private float jump =0;
    [SerializeField]
    private GameObject Player;
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
        if (transform.position.y <= -3)
        {
            SceneManager.LoadScene("Game");
        }
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
            Debug.Log(GameContentReaderAndSetter.Instance.GameVibrationGetterAndSetter);
            if (GameContentReaderAndSetter.Instance.GameVibrationGetterAndSetter)
            {
                Handheld.Vibrate();
            }
            CanDeployBlock = false;
            playerRb.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
            instantiateBlockCoroutine = StartCoroutine(InstantiateBlock(0.05f));
            StartCoroutine(CoolDown(0.2f));
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
