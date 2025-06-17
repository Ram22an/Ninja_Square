using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UIElementSetter : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer Player;
    [SerializeField]
    private SpriteRenderer Bullet;
    [SerializeField]
    private SpriteRenderer Castle;
    [SerializeField]
    private SpriteRenderer PlayerDead;
    [SerializeField]
    public List<Sprite> AllBullet;
    [SerializeField]
    public List<float> Angle;
    [SerializeField]
    private GameObject BulletObj;
    public static UIElementSetter Instance;
    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;
    }
    public void Start()
    {
        Time.timeScale = 1f;
        Player.sprite = GameContentReaderAndSetter.Instance.GameContentPlayerSkinGetterAndSetter;
        PlayerDead.sprite = GameContentReaderAndSetter.Instance.GameContentPlayerSkinGetterAndSetter;
        int index = AllBullet.IndexOf(GameContentReaderAndSetter.Instance.GameContentBulletSkinGetterAndSetter);
        float angle = Angle[index];
        BulletObj.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        Bullet.sprite = GameContentReaderAndSetter.Instance.GameContentBulletSkinGetterAndSetter;
        Castle.sprite = GameContentReaderAndSetter.Instance.GameContentCastleSkinGetterAndSetter;
    }
}//class
