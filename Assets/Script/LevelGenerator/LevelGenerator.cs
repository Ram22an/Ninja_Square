using UnityEngine;
public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private LevelParts gameObjectList;
    [SerializeField]
    private GameObject Startingpoint;
    [SerializeField]
    private DifficultLevelParts difficultLevelParts;
    private GameObject[] prefabList;
    private GameObject[] difficultprefabList;
    private float[] length;
    private float[] DifficultLenght;
    [SerializeField]
    private float difficulty;
    [SerializeField]
    private GameObject PreviousGameObject;
    [SerializeField]
    private GameObject FinalObj;
    [SerializeField]
    private GameObject Castle;

    public void Start()
    {
        float[] CurrentLevelData = LevelManager.Instance.LoadLevelData();
        int currentLevel = (int)CurrentLevelData[0];
        difficulty = CalculateNumberOfBlocks((int)CurrentLevelData[0]);
        prefabList=gameObjectList.Prefabs;
        difficultprefabList=difficultLevelParts.Prefabs;
        length = gameObjectList.lenght;
        DifficultLenght=difficultLevelParts.lenght;
        Instantiate(prefabList[0], Startingpoint.transform.position, Quaternion.identity);
        Startingpoint.transform.position+=new Vector3(length[0],0,0);
        Instantiate(prefabList[0], Startingpoint.transform.position, Quaternion.identity);
        Startingpoint.transform.position += new Vector3(length[0], 0, 0);
        PreviousGameObject=prefabList[0];
        int tempLevelID=0;
        for(int i=0;i< difficulty; i++)
        {
            float finallenght;
            GameObject chosenPrefab;
            int randomIndex;
            float difficultPrefabProbability = Mathf.Clamp01((currentLevel % 10) / 10f);
            if (Random.value < difficultPrefabProbability)
            {
                float finalindex;
                while (true)
                {
                    randomIndex = Random.Range(0, difficultprefabList.Length);
                    chosenPrefab = difficultprefabList[randomIndex];
                    if (PreviousGameObject != chosenPrefab)
                    {
                        finalindex=randomIndex;
                        break;
                    }
                }
                finallenght = DifficultLenght[(int)finalindex];
            }
            else
            { 
                float finalindex;
                while (true)
                {
                    randomIndex = Random.Range(1, prefabList.Length);
                    chosenPrefab = prefabList[randomIndex];
                    if (PreviousGameObject != chosenPrefab)
                    {
                        finalindex = randomIndex;
                        break;
                    }
                }
                finallenght = length[(int)finalindex];
            }
            //int RandomNumber;
            //while (true) 
            //{
            //    RandomNumber=Random.Range(1,prefabList.Length);
            //    if (PreviousGameObject != prefabList[RandomNumber])
            //    {
            //        break;
            //    }
            //}
            //tempLevelID =tempLevelID*10+RandomNumber;
            //Instantiate(prefabList[RandomNumber], Startingpoint.transform.position, Quaternion.identity);
            //Startingpoint.transform.position += new Vector3(length[RandomNumber], 0, 0);
            //PreviousGameObject =prefabList[RandomNumber];
            tempLevelID = tempLevelID * 10 + randomIndex;
            Instantiate(chosenPrefab, Startingpoint.transform.position, Quaternion.identity);
            Startingpoint.transform.position += new Vector3(finallenght, 0, 0);
            PreviousGameObject = chosenPrefab;
        }
        Instantiate(prefabList[0], Startingpoint.transform.position, Quaternion.identity);
        //Vector3 CastlePlace = new Vector3(Startingpoint.transform.position.x + length[0]/2, Startingpoint.transform.position.y,Startingpoint.transform.position.z);
        //Instantiate(Castle,CastlePlace,Quaternion.identity);
        Startingpoint.transform.position += new Vector3(length[0], 0, 0);
        tempLevelID = tempLevelID * 10 + 0;
        Instantiate(FinalObj, Startingpoint.transform.position, Quaternion.identity);
        LevelManager.Instance.SaveLevelData(LevelManager.Instance.GetCurrentLevel(), tempLevelID);
        Instantiate(prefabList[0], Startingpoint.transform.position, Quaternion.identity);
        Vector3 CastlePlace = new Vector3(Startingpoint.transform.position.x + length[0]/4, Startingpoint.transform.position.y+0.5f, Startingpoint.transform.position.z);
        Instantiate(Castle, CastlePlace, Quaternion.identity);
        Startingpoint.transform.position = new Vector3(0, 0, 0);
    }
    private int CalculateNumberOfBlocks(int level)
    {
        int rangeStart = ((level - 1) / 10) * 10 + 1; // Start of the range (1, 11, 21, ...)
        int rangeEnd = rangeStart + 9; // End of the range (10, 20, 30, ...)
        int baseLevel = level - rangeStart; // Level within the range (0 to 9)

        // Interpolate between 7 and 14
        int minBlocks = 7;
        int maxBlocks = 14;
        int numberOfBlocks = Mathf.RoundToInt(Mathf.Lerp(minBlocks, maxBlocks, baseLevel / 9f));

        return numberOfBlocks;
    }
}//class
