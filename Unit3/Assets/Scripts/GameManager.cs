using System;
using UnityEngine;
using UnityEngine.XR;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState currentGameState {  get; private set; }
    public Action<GameState> OnStateChanged;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ChangeState(GameState.Breifing);
    }

    public void ChangeState(GameState state)
    {
        if (currentGameState != state)
        {
            currentGameState = state;
            OnStateChanged?.Invoke(currentGameState);
            Debug.Log("Entering a New Game State: " + currentGameState);
        }
    }

    public void FinishIntroCinamtic()
    {
        ChangeState(GameState.Level_1);
    }

}



public enum GameState
{
    Breifing,
    Level_1,
    Level_2,
    Level_3,
    Level_4,
    Level_5,
    GameOver,
    GameWin
}