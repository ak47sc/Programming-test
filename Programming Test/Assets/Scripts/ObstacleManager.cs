using System.IO;
using UnityEditor;
using UnityEngine;

//Script for spawning obstacle in the grid
public class ObstacleManager : MonoBehaviour
{
    [SerializeField]private GameObject obstaclePreFab; // sphere gameObject

    private ObstacleData obstacleData; // reference for obstacle data stored in a SO 
    private string[] scriptableObjectPath = new string[] { "Assets", "ScriptableObject", "ObstacleData.asset" };
    private void Awake()
    {
        obstacleData = AssetDatabase.LoadAssetAtPath<ObstacleData>(Path.Combine(scriptableObjectPath)); // loading SO
    }
    private void Start()
    {
        //Instantiating obstacles based on the data in SO
        for(int i = 0; i<obstacleData.obstacles.Length;i++)
        {
            if (obstacleData.obstacles[i] == true)
            {
                GridManager.Instance.getGrid(i).GetComponent<TileInfo>().SetWalkable();
                Instantiate(obstaclePreFab,GridManager.Instance.getGrid(i).position + Vector3.up,Quaternion.identity);
            }
        }
    }
}
