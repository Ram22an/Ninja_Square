using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParent : MonoBehaviour
{
    #region TriedButNotGood
    //[SerializeField]
    //private float moveSpeed = 5f;
    //float gravity = -9.8f;
    //float verticalVelocity = 0f;
    //void Update()
    //{
    //    verticalVelocity += gravity * Time.deltaTime;
    //    transform.position += new Vector3(0, verticalVelocity * Time.deltaTime, 0);

    //    // Stop falling when hitting the ground
    //    if (transform.position.y <= 0)
    //    {
    //        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    //        verticalVelocity = 0; // Reset velocity
    //    }
    //}
    #endregion
    [SerializeField]
    public float BasemoveSpeed = 5f;
    [SerializeField]
    public float gravity = -9.8f;
    [SerializeField]
    private float groundLevel = 0f;
    [SerializeField]
    public float FinalSpeed;
    [SerializeField]
    private AudioSource JujustuActivation;
    private float verticalVelocity = 0f;
    public static PlayerParent Instance;
    public void Awake()
    {
        JujustuActivation.ignoreListenerPause = true;
    }
    public void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
            return;
        }
        Instance = this;
        float[] CurrentLevelData=LevelManager.Instance.LoadLevelData();
        FinalSpeed = CalculateSpeed((int)CurrentLevelData[0]);
        if(FinalSpeed == 0f)
        {
            FinalSpeed = 5f;
        }
    }
    public void Update()
    {
        // Horizontal movement
        transform.position += new Vector3(FinalSpeed * Time.deltaTime, 0, 0);

        // Apply gravity
        verticalVelocity += gravity * Time.deltaTime;
        transform.position += new Vector3(0, verticalVelocity * Time.deltaTime, 0);

        // Stop movement at the ground level
        if (transform.position.y <= groundLevel)
        {
            transform.position = new Vector3(transform.position.x, groundLevel, transform.position.z);
            verticalVelocity = 0; // Reset vertical velocity when grounded
        }
    }
    private float CalculateSpeed(int level)
    {
        int baseLevel = ((level - 1) / 10) * 10 + 1;
        int progress = level - baseLevel;
        float speed = Mathf.Lerp(5, 8, progress / 8f);
        return speed;
    }
    public void PlayJujustuSound()
    {
        JujustuActivation.Play();
    }
    public void StopJujustuSound()
    {
        JujustuActivation.Stop();
    }
}//class
