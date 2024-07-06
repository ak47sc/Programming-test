using UnityEngine;

//Tile info script
public class TileInfo : MonoBehaviour
{
    [SerializeField]
    private string tileInfo;
    private bool isWalkable;

    public string getTileInfo()
    {
        return tileInfo;
    }

    public void SetWalkable()
    {
        isWalkable = true;
        tileInfo += " (Blocked)";
    }
}
