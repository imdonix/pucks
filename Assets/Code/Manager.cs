using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { Start, Player1Turn, Player2Turn, End }

public class Manager : MonoSingleton<Manager>
{

    [SerializeField] public Puck PuckPref;

    [SerializeField] private Transform lightRenderer;
    [SerializeField] private SpriteRenderer topRenderer;
    [SerializeField] private SpriteRenderer botRenderer;

    private SpriteRenderer mapRender;

    // World has a 1x1 size
    private Player player1;
    private Player player2;

    private Map map;

    private float time;

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
        time += Time.deltaTime;

        if (ReferenceEquals(map, null)) return;

        mapRender.sprite = map.GetSprite();
        lightRenderer.rotation = Quaternion.Euler(0, Mathf.Sin(time) * 20, 0);

        player1.Draw();
        player2.Draw(); 

    }

    #endregion

    private void GameStart()
    {
        Tuple<Color, Color> colors = RandomColors();
        topRenderer.material.color = colors.Item1;
        botRenderer.material.color = colors.Item2;
        map = new Map(colors.Item1, colors.Item2);
        player1 = new Player(map, 0);
        player2 = new Player(map, 1);
        

        StartNextTurn();
    }

    private Tuple<Color, Color> RandomColors()
    {

        Color primary = new Color(
            UnityEngine.Random.Range(0, 1F), 
            UnityEngine.Random.Range(0, 1F), 
            UnityEngine.Random.Range(0, 1F),
            1);
        Color sec = new Color(
            1 - primary.r,
            1 - primary.g,
            1 - primary.b,
            1);
        return new Tuple<Color, Color>(primary, sec);
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