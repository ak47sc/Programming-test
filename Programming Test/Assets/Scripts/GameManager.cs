using UnityEngine;

//Script managing the game state
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum GameState { PLAYER_TURN,ENEMY_TURN}

    private GameState state = GameState.PLAYER_TURN;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void SetState(GameState state)
    {
        this.state = state;
    }
    public GameState GetState()
    {
        return state;
    }
}
