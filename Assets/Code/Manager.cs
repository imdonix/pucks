using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { Start, Player1Turn, Player2Turn, End }

public class Manager : MonoSingleton<Manager>
{

    [SerializeField] public Puck PuckPref;
    [SerializeField] public Effect[] EffectPrefs;

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
                
        //Logic
        mapRender.sprite = map.GetSprite();
        
        player1.Draw();
        player2.Draw();

        CheckGameEnd();

        //Select
        foreach (Puck item in player2.GetPucks())
        {
            map.Pain(item.GetPosition(), item.GetSize(), 1);
        }


        if (State == GameState.Player1Turn)
        {
            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                player1.SelectPuck(false);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                player1.SelectPuck(true);
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                player1.GetSelected().MoveToNextPhase();
            }
        }

        if (State == GameState.Player2Turn)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                player2.SelectPuck(false);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                player2.SelectPuck(true);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                player2.GetSelected().MoveToNextPhase();
            }
        }

        //Effects
        lightRenderer.rotation = Quaternion.Euler(0, Mathf.Sin(time) * 20, 0);
    }

    #endregion

    private void CheckGameEnd()
    {
        if (!player1.IsAlive())
        {
            Debug.Log("Player 2 win");
            State = GameState.End;
        }

        else if (!player2.IsAlive())
        {
            Debug.Log("Player 1 win");
            State = GameState.End;
        }
    }

    private void GameStart()
    {
        Tuple<Color, Color, Color> colors = RandomColors();
        topRenderer.material.color = colors.Item1;
        botRenderer.material.color = colors.Item2;
        map = new Map(colors.Item1, colors.Item2, colors.Item3);
        player1 = new Player(map, 0);
        player2 = new Player(map, 1);

        for (int i = 0; i < UnityEngine.Random.Range(2,5); i++)
        {
            float x = UnityEngine.Random.Range(-8, 8F);
            float y = UnityEngine.Random.Range(-3, 3F);
            Vector2 pos = new Vector2(x, y);


            Puck obsticle = GameObject.Instantiate(PuckPref);
            obsticle.transform.position = pos;
            obsticle.transform.localScale = Vector3.one * 2;
            obsticle.Obsticle(4);

            map.Pain(obsticle.GetPosition(), obsticle.GetSize(), 3);
        }


        for (int i = 0; i < UnityEngine.Random.Range(5, 8); i++)
        {
            Effect pref = EffectPrefs[UnityEngine.Random.Range(0, EffectPrefs.Length)];
            Effect effect = GameObject.Instantiate(pref);

            float x = UnityEngine.Random.Range(-9, 9F);
            float y = UnityEngine.Random.Range(-5, 5F);

            effect.transform.position = new Vector2(x, y);
            effect.transform.localScale = Vector3.one * 2;
            effect.Bind(map, TimeControl.Instance);

        }

        StartNextTurn();
    }

    private Tuple<Color, Color, Color> RandomColors()
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
        Color third = new Color(
            .5F - primary.r,
            .5F - primary.g,
            .5F - primary.b,
            1);
        return new Tuple<Color, Color, Color>(primary, sec, third);
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