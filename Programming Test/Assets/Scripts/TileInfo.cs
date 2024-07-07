using System.Collections.Generic;
using UnityEngine;

//Tile info script
public class TileInfo : MonoBehaviour
{
    [SerializeField] private string tileInfo;
    [SerializeField] private float weight = int.MaxValue;
    [SerializeField] private Transform parentNode = null; 
    [SerializeField] private List<Transform> neighbourNode;
    
    private bool isWalkable = true;

    public void ResetNode()
    {
        weight = int.MaxValue;
        parentNode = null;
    }
    public string GetTileInfo()
    {
        return tileInfo;
    }

    public void SetWalkable()
    {
        if(isWalkable)
        {
            isWalkable = false;
            tileInfo += " (Blocked)";
        }
        else
        {
            isWalkable = true;
            tileInfo = tileInfo.Split(" ")[0];
        }
    }

    public void SetParentNode(Transform node)
    {
        this.parentNode = node;
    }

    public void SetWeight(float value)
    {
        this.weight = value;
    }

    public void AddNeighbourNode(Transform node)
    {
        this.neighbourNode.Add(node);
    }
    public List<Transform> GetNeighbourNode()
    {
        List<Transform> result = this.neighbourNode;
        return result;
    }
    public float GetWeight()
    {
        float result = this.weight;
        return result;

    }

    public Transform GetParentNode()
    {
        Transform result = this.parentNode;
        return result;
    }

    public bool GetisWalkable()
    {
        return isWalkable;
    }
}
