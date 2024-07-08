using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//player controller
public class PlayerController : MonoBehaviour
{
    [SerializeField] private int currentTileIndex;
    [SerializeField] private float movementSpeed;

    private Transform currentTile; // current tile player is present
    private bool HasDestination;
    private List<Transform> destinationList;
    public int CurrentDestinationIndex = -1;
    private bool isMoving = false;

    private void Start()
    {
        currentTile = GridManager.Instance.getGrid(currentTileIndex);
        transform.position = currentTile.position + (Vector3.up * 1.5f);
    }
    private void Update()
    {
        //if destination present moves to next destination point and starts coroutine for delay and repeats till destination arrives
        if (HasDestination)
        {
            if(CurrentDestinationIndex >= destinationList.Count)
            {
                HasDestination = false;
                isMoving = false;
                currentTile = destinationList[CurrentDestinationIndex-1];
                destinationList[CurrentDestinationIndex-1].GetComponent<Renderer>().material.SetColor("_Color", new Color32(250, 160, 5, 255));
                CurrentDestinationIndex = -1;
                GameManager.Instance.SetState(GameManager.GameState.ENEMY_TURN);
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
            //transform.position = Vector3.MoveTowards(transform.position, destinationList[CurrentDestinationIndex].position + (Vector3.up * 1.5f),movementSpeed);
        }
    }

    IEnumerator Delay()
    {
        CurrentDestinationIndex++;
        yield return new WaitForSeconds(movementSpeed);
        isMoving = false;
    }

    public void SetHasDestination(bool hasDestination)
    {
        HasDestination = hasDestination;
    }
    
    public void SetDestinationList(List<Transform> destinationList)
    {
        this.destinationList = destinationList;
    }
    public void SetCurrentTile(Transform tile)
    {
        currentTile = tile;
    }
    
    public Transform GetCurrentTile()
    {
        return currentTile;
    }
    public bool GetHasDestination()
    {
        return HasDestination;
    }

}
