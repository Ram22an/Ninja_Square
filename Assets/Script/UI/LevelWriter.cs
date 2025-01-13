using UnityEngine;
using TMPro;
public class LevelWriter : MonoBehaviour
{
    public void Start()
    {
        int level=(int)LevelManager.Instance.GetCurrentLevel();
        GameObject LevelChild= transform.Find("Level")?.gameObject;
        TextMeshProUGUI Text=LevelChild.GetComponent<TextMeshProUGUI>();
        Text.text = "Level "+level.ToString();
    }
}
