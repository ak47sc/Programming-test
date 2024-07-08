using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//Enemy script implements AI interface
public class EnemyAi : MonoBehaviour,IAi
{
    [SerializeField] private Transform Player;
    [SerializeField] private int currentTileIndex;
    [SerializeField] private float movementSpeed;

    private Transform currentTile;
    public bool HasDestination = false;
    private bool isMoving = false;
    private List<Transform> destinationList;
    private int CurrentDestinationIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        currentTile = GridManager.Instance.getGrid(currentTileIndex);
        currentTile.GetComponent<TileInfo>().SetWalkable();
        transform.position = currentTile.position + (Vector3.up * 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        //Checks if destination present and it's enemy turn
        if(!HasDestination && GameManager.Instance.GetState() == GameManager.GameState.ENEMY_TURN)
        {
            PathFinding finder = new PathFinding();
            currentTile.GetComponent<TileInfo>().SetWalkable();
            destinationList = finder.FindShortestPath(currentTile, Player.GetComponent<PlayerController>().GetCurrentTile());
            if(destinationList == null)
            {
                Debug.Log("NO PATH");
                GameManager.Instance.SetState(GameManager.GameState.PLAYER_TURN);
                return;
            }
            // Colour the node red.
            foreach (Transform path in destinationList)
            {
                Renderer rend = path.GetComponent<Renderer>();
                rend.material.SetColor("_Color", Color.cyan);
            }

            HasDestination = true;
        }
        //if destination present moves to next destination point and starts coroutine for delay and repeats till destination arrives
        if (HasDestination)
        {
            if (CurrentDestinationIndex >= destinationList.Count-1)
            {
                HasDestination = false;
                isMoving = false;
                currentTile = destinationList[CurrentDestinationIndex - 1];
                currentTile.GetComponent<TileInfo>().SetWalkable();
                destinationList[CurrentDestinationIndex].GetComponent<Renderer>().material.SetColor("_Color", new Color32(250, 160, 5, 255));
                CurrentDestinationIndex = -1;
                GameManager.Instance.SetState(GameManager.GameState.PLAYER_TURN);
                return;
            }
            if (isMoving)
            {
                destinationList[CurrentDestinationIndex].GetComponent<Renderer>().material.SetColor("_Color", new Color32(250, 160, 5, 255));
                transform.position = Vector3.Lerp(transform.position, destinationList[CurrentDestinationIndex].position + (Vector3.up * 1.5f), 0.1f);
            }
            else
            {
                StartCoroutine(nameof(Delay));
                isMoving = true;
            }
        }
    }
    //coroutine for delay
    IEnumerator Delay()
    {
        CurrentDestinationIndex++;
        yield return new WaitForSeconds(movementSpeed);
        isMoving = false;
    }
}

