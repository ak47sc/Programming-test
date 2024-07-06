using System.Collections.Generic;
using UnityEngine;

//Script for creating the Grid
public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    [SerializeField]
    private GameObject gridCube;
    [SerializeField]
    private int gridX , gridY;
    [SerializeField]
    private List<Transform> gridCubeList;

    public Vector3 gridPos;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        float gridXinc = gridCube.transform.localScale.x;
        float gridZinc = gridCube.transform.localScale.z;
        for (int i = 0; i < gridX; i++)
        {
            gridPos.x = 0;
            for (int j = 0; j < gridY; j++)
            {
                GameObject gridInstance = Instantiate(gridCube, gridPos, Quaternion.identity);
                gridCubeList.Add(gridInstance.transform);
                gridPos.x += gridXinc;

            }
            gridPos.z += gridZinc;
        }
    }

    public Transform getGrid(int index)
    {
        return gridCubeList[index];
    }
}
