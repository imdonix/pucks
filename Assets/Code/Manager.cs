using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { Start, Player1Turn, Player2Turn, End }

public class Manager : MonoSingleton<Manager>
{
    // World has a 1x1 size
    [SerializeField] private Player player1, player2;

    #region Properties

    public GameState State { get; private set; } = GameState.Start;

    #endregion

    private void Start()
    {
        StartNextTurn();
    }

    public void StartNextTurn()
    {
        if (State == GameState.Start)
        {
            player1.StartTurn();
            State = GameState.Player1Turn;
            return;
        }

        if (State == GameState.Player1Turn)
        {
            player1.EndTurn();
            player2.StartTurn();
            State = GameState.Player2Turn;
        }
        
        else if (State == GameState.Player2Turn)
        {
            player2.EndTurn();
            player1.StartTurn();
            State = GameState.Player1Turn;
        }
    }
}