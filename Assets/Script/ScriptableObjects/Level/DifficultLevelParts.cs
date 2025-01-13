using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DifficultLevelParts", menuName = "ScriptableObjects/DifficultLevelParts", order = 1)]
public class DifficultLevelParts : ScriptableObject
{
    public GameObject[] Prefabs;
    public float[] lenght;
    public int[] id;
}
