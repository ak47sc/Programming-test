using System;
using System.Collections.Generic;
using UnityEngine;

//Managing of raycasting and get info of tiles
public class TileSelectManager : MonoBehaviour
{
    [SerializeField] private LayerMask tileLayerMask;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform selectionCube;
    [SerializeField] private Transform player;

    private Transform tileOnMouseCursor;
    private bool HasPath = true;
    public class TileMouseOverEventArgs : EventArgs
    {
        public string TileInfo { get; set; }
        public Vector3 Position { get; set; }
    }
    public static event EventHandler<TileMouseOverEventArgs> TileMouseOver; // event to tigger mouse over on tile for UI Manager
    public static event Action TileNotMouseOver; // event to tigger mouse not on tile for UI Manager

    void Update()
    {
        tileOnMouseCursor = MouseOverHandler();
        PlayerDestinationSetHandler();
    }

    //Fuction to show tileInfo on mouse over
    private Transform MouseOverHandler()
    {
        if (!player.GetComponent<PlayerController>().GetHasDestination() && GameManager.Instance.GetState() == GameManager.GameState.PLAYER_TURN && Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, tileLayerMask))
        {
            selectionCube.gameObject.SetActive(true);
            selectionCube.transform.position = hit.transform.position;
            TileMouseOver?.Invoke(gameObject, new TileMouseOverEventArgs() { TileInfo = hit.transform.GetComponent<TileInfo>().GetTileInfo(), Position = hit.transform.position });
            return hit.transform;
        }
        else
        {
            selectionCube.gameObject.SetActive(false);
            TileNotMouseOver?.Invoke();
            return null;
        }
    }

    //Function to select the destination tile when it's player turn
    private void PlayerDestinationSetHandler()
    {
        if (tileOnMouseCursor != null && GameManager.Instance.GetState() == GameManager.GameState.PLAYER_TURN && !player.GetComponent<PlayerController>().GetHasDestination() && tileOnMouseCursor.GetComponent<TileInfo>().GetisWalkable())
        {
            if (Input.GetMouseButtonDown(0))
            {
                PathFinding finder = new PathFinding();
                List<Transform> paths = finder.FindShortestPath(player.GetComponent<PlayerController>().GetCurrentTile(), tileOnMouseCursor);

                if(paths == null)
                {
                    Debug.Log("NO PATH");
                    HasPath = false;
                    return;
                }
                HasPath = true;
                // Colour the node red.
                foreach (Transform path in paths)
                {
                    Renderer rend = path.GetComponent<Renderer>();
                    rend.material.SetColor("_Color", Color.cyan);
                }
                player.GetComponent<PlayerController>().SetHasDestination(true);
                player.GetComponent<PlayerController>().SetDestinationList(paths);
            }
        }
    }

    public bool getHasPath()
    {
        return HasPath;
    }
}
