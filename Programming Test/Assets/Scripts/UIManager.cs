using UnityEngine;
using TMPro;
using System;

//UI Manager for displaying tile info and position
public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tileInfoText;
    [SerializeField] private TextMeshProUGUI tilePositionText;
    [SerializeField] private TextMeshProUGUI GameStateTextUI;
    [SerializeField] private TextMeshProUGUI playerStateTextUI;
    [SerializeField] private GameObject tileSelectionUIHolder;
    [SerializeField] private GameObject tileSelectionManager;
    [SerializeField] private Transform playerTransform;
    // Start is called before the first frame update
    private void OnEnable()
    {
        TileSelectManager.TileMouseOver += TileSelectManager_TileMouseOver;
        TileSelectManager.TileNotMouseOver += TileSelectManager_TileNotMouseOver;
    }


    private void OnDisable()
    {
        TileSelectManager.TileMouseOver -= TileSelectManager_TileMouseOver;
        TileSelectManager.TileNotMouseOver -= TileSelectManager_TileNotMouseOver;
    }
    private void TileSelectManager_TileNotMouseOver()
    {
        tileSelectionUIHolder.SetActive(false);
    }
    private void TileSelectManager_TileMouseOver(object sender, TileSelectManager.TileMouseOverEventArgs e)
    {
        tileInfoText.text = e.TileInfo;
        tilePositionText.text = $"({e.Position.x},{e.Position.z})";
        tileSelectionUIHolder.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.GetState() == GameManager.GameState.PLAYER_TURN)
        {
            GameStateTextUI.text = "Player Turn";
        }
        else
        {
            GameStateTextUI.text = "Enemy Turn";
        }
        if (tileSelectionManager.GetComponent<TileSelectManager>().getHasPath())
        {
            if (playerTransform.GetComponent<PlayerController>().GetHasDestination())
            {
                playerStateTextUI.text = "Moving";
            }
            else
            {
                playerStateTextUI.text = "Idle";
            }
        }
        else
        {
            playerStateTextUI.text = "No Path";
        }
    }
}
