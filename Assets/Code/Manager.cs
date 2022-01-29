using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { Start, Player1Turn, Player2Turn, End }

public class Manager : MonoSingleton<Manager>
{

    [SerializeField] public Puck PuckPref;

    private SpriteRenderer mapRender;

    // World has a 1x1 size
    private Player player1;
    private Player player2;

    private Map map;

    #region Properties

    public GameState State { get; private set; } = GameState.Start;

    #endregion

    #region UNITY

    private void Start()
    {
        mapRender = GetComponent<SpriteRenderer>();
        GameStart();
    }

    private void Update()
    {
        if (ReferenceEquals(map, null)) return;

        mapRender.sprite = map.GetSprite();

        foreach (Puck item in player1.GetPucks())
        {
            map.Pain(item.GetPosition(), item.GetSize(), 0);
        }

        foreach (Puck item in player2.GetPucks())
        {
            map.Pain(item.GetPosition(), item.GetSize(), 1);
        }
    }

    #endregion

    private void GameStart()
    {
        map = new Map(Color.blue, Color.red);
        player1 = new Player(0);
        player2 = new Player(1);
        

        StartNextTurn();
    }

    public Map GetMap()
    {
        return map;
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