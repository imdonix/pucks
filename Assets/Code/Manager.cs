using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { Player1Turn, Player2Turn, Idle, End }

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
    private GameState state;


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

        if (state == GameState.Player1Turn)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                player1.SelectPuck(-1);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                player1.SelectPuck(1);
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                player1.GetSelected().MoveToNextPhase();
            }
        }

        if (state == GameState.Player2Turn && !player2.IsAI())
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                player2.SelectPuck(-1);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                player2.SelectPuck(1);
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
            Endgame.Instance.Show(player2.Type());
            state = GameState.End;
        }

        else if (!player2.IsAlive())
        {
            Endgame.Instance.Show(player1.Type());
            state = GameState.End;
        }
    }

    private void GameStart()
    {
        Tuple<Color, Color, Color> colors = RandomColors();
        topRenderer.material.color = colors.Item1;
        botRenderer.material.color = colors.Item2;
        map = new Map(colors.Item1, colors.Item2, colors.Item3);
        player1 = new Player(map, 0, false);
        player2 = new Player(map, 1, true);

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

        state = GameState.Player1Turn;
        player1.SelectPuck(0);
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

    public GameState GetState()
    {
        return state;
    }

    public GameState Idle()
    {
        GameState tmp = state;
        state = GameState.Idle;
        return tmp;
    }

    public void SetState(GameState state)
    {
        this.state = state;
    }

    public void StartNextTurn()
    {
        if (state == GameState.Player1Turn)
        {
            state = GameState.Player2Turn;
            if (player2.IsAI())
            {
                StartCoroutine(DoAITurn()); 
            }
            else
            {
                player2.SelectPuck(0);
            }
        }
        else if (state == GameState.Player2Turn)
        {
            state = GameState.Player1Turn;
            player1.SelectPuck(0);
        }
        else if (state == GameState.End)
        {
            
        }
    }

    private IEnumerator DoAITurn()
    {
        player2.SelectPuck(0);
        for (int i = 0; i < UnityEngine.Random.Range(0,3); i++)
        {
            player2.SelectPuck(1);
            yield return new WaitForSeconds(1);
        }

        while (state == GameState.Player2Turn)
        {
            player2.GetSelected().MoveToNextPhase();
            yield return new WaitForSeconds(UnityEngine.Random.Range(0, 2F));
        }

    }
}