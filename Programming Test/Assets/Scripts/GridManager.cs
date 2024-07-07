using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

//Script for creating the Grid
public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    [SerializeField]
    private GameObject gridCube;
    [SerializeField]
    private int row , column;
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

        GenerateGrid();
        GenerateNeighbours();
    }

    public Transform getGrid(int index)
    {
        return gridCubeList[index];
    }

    private void GenerateGrid()
    {
        float gridXinc = gridCube.transform.localScale.x;
        float gridZinc = gridCube.transform.localScale.z;
        for (int i = 0; i < row; i++)
        {
            gridPos.x = 0;
            for (int j = 0; j < column; j++)
            {
                GameObject gridInstance = Instantiate(gridCube, gridPos, Quaternion.identity);
                gridCubeList.Add(gridInstance.transform);
                gridPos.x += gridXinc;

            }
            gridPos.z += gridZinc;
        }
    }
    private void GenerateNeighbours()
    {
        for (int i = 0; i < gridCubeList.Count; i++)
        {
            TileInfo currentNode = gridCubeList[i].GetComponent<TileInfo>();
            int index = i + 1;

            // For those on the left, with no left neighbours
            if (index % column == 1)
            {
                // We want the node at the top as long as there is a node.
                if (i + column < column * row)
                {
                    currentNode.AddNeighbourNode(gridCubeList[i + column]);   // North node
                }

                if (i - column >= 0)
                {
                    currentNode.AddNeighbourNode(gridCubeList[i - column]);   // South node
                }
                currentNode.AddNeighbourNode(gridCubeList[i + 1]);     // East node
            }

            // For those on the right, with no right neighbours
            else if (index % column == 0)
            {
                // We want the node at the top as long as there is a node.
                if (i + column < column * row)
                {
                    currentNode.AddNeighbourNode(gridCubeList[i + column]);   // North node
                }

                if (i - column >= 0)
                {
                    currentNode.AddNeighbourNode(gridCubeList[i - column]);   // South node
                }
                currentNode.AddNeighbourNode(gridCubeList[i - 1]);     // West node
            }

            else
            {
                // We want the node at the top as long as there is a node.
                if (i + column < column * row)
                {
                    currentNode.AddNeighbourNode(gridCubeList[i + column]);   // North node
                }

                if (i - column >= 0)
                {
                    currentNode.AddNeighbourNode(gridCubeList[i - column]);   // South node
                }
                currentNode.AddNeighbourNode(gridCubeList[i + 1]);     // East node
                currentNode.AddNeighbourNode(gridCubeList[i - 1]);     // West node
            }

        }
    }
}
