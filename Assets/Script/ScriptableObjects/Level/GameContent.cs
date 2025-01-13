using UnityEngine.UI;
using UnityEngine;
[CreateAssetMenu(fileName = "GameContent", menuName = "ScriptableObjects/GameContent", order = 1)]
public class GameContent : ScriptableObject
{
    public Sprite BulletSkin;
    public Sprite CastelSkin;
    public Sprite PlayerSkin;
    public int diamond;
    public int death;
    public bool Vibrate;
    public bool Sound;
    public float SoundValue;
    public int Character;
    public int Castel;
    public int Bullet;
    public Vector3 PlayerDeadPositon;
}
