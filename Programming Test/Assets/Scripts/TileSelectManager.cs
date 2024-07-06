using System;
using UnityEngine;

//Managing of raycasting and get info of tiles
public class TileSelectManager : MonoBehaviour
{
    [SerializeField]
    private LayerMask tileLayerMask;
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private Transform selectionCube;
    [SerializeField]
    private GameObject tileSelectionUIHolder;

    
    public class TileMouseOverEventArgs : EventArgs
    {
        public string TileInfo { get; set; }
        public Vector3 Position { get; set; }
    }
    public static event EventHandler<TileMouseOverEventArgs> TileMouseOver; // event to tigger mouse over on tile of UI Manager

    void Update()
    {
        if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, tileLayerMask))
        {
            selectionCube.gameObject.SetActive(true);
            tileSelectionUIHolder.SetActive(true);
            selectionCube.transform.position = hit.transform.position;
            TileMouseOver?.Invoke(gameObject, new TileMouseOverEventArgs() { TileInfo = hit.transform.GetComponent<TileInfo>().getTileInfo(), Position = hit.transform.position });
        }
        else
        {
            tileSelectionUIHolder.SetActive(false);
            selectionCube.gameObject.SetActive(false);
        }
    }
}
