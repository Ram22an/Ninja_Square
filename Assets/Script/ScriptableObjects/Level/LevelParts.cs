using UnityEngine;
[CreateAssetMenu(fileName ="LevelParts",menuName = "ScriptableObjects/LevelParts",order =1)]
public class LevelParts : ScriptableObject
{
    public GameObject[] Prefabs;
    public float[] lenght;
    public int[] id;
}
