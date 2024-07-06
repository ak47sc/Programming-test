using UnityEngine;

//Script for Tile info UI to follow the mouse cursor when over the tile
public class TileInfoUIMovement : MonoBehaviour
{
    [SerializeField]
    private Canvas mainCanvas;

    void Update()
    {
        Vector2 movePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(mainCanvas.transform as RectTransform, Input.mousePosition, mainCanvas.worldCamera, out movePos);
        transform.position = mainCanvas.transform.TransformPoint(movePos);
    }
}
