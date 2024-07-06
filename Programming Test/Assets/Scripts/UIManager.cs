using UnityEngine;
using TMPro;

//UI Manager for displaying tile info and position
public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI tileInfoText;
    [SerializeField]
    private TextMeshProUGUI tilePositionText;
    // Start is called before the first frame update
    private void OnEnable()
    {
        TileSelectManager.TileMouseOver += TileSelectManager_TileMouseOver;
    }
    private void OnDisable()
    {
        TileSelectManager.TileMouseOver -= TileSelectManager_TileMouseOver; 
    }
    private void TileSelectManager_TileMouseOver(object sender, TileSelectManager.TileMouseOverEventArgs e)
    {
        tileInfoText.text = e.TileInfo;
        tilePositionText.text = $"({e.Position.x},{e.Position.z})";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
