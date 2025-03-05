using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerSelectedOrNot", menuName = "ScriptableObjects/PlayerSelectedOrNot", order = 1)]
public class PlayerSelectedOrNot : ScriptableObject
{
    [SerializeField]
    public List<int> Unlocked;
    [SerializeField]
    public List<int> RequiredGems = new List<int> { 0, 10, 20, 30, 40, 50 };
    [SerializeField]
    public bool WatchedAdFor7;
    [SerializeField]
    public bool WatchedAdFor8;
    [SerializeField]
    public Sprite Selected;
}
