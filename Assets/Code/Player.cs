using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private int lastPuck = -1;

    private int type;
    private List<Puck> pucks;
    

    public float DirectionArrowSpeed { get; private set; } = 1;

    public Player(int type)
    {
        this.type = type;
        this.pucks = new List<Puck>();

        int y = type == 0 ? 5 : -5;

        Puck queen = GameObject.Instantiate(Manager.Instance.PuckPref);
        queen.transform.position = new Vector3(0, y, 0);
        queen.transform.localScale = Vector3.one * 3;
        queen.Set(this, true, 8);
        pucks.Add(queen);

        Puck one = GameObject.Instantiate(Manager.Instance.PuckPref);
        one.transform.position = new Vector3(-5, y, 0);
        one.transform.localScale = Vector3.one * 2;
        one.Set(this, false, 5);
        pucks.Add(one);

        Puck two = GameObject.Instantiate(Manager.Instance.PuckPref);
        two.transform.position = new Vector3(5, y, 0);
        two.transform.localScale = Vector3.one * 2;
        two.Set(this, false, 5);
        
        pucks.Add(two);
    }

    public void StartTurn()
    {
        pucks[++lastPuck].Select();
    }

    public void EndTurn()
    {
        
    }

    public List<Puck> GetPucks()
    {
        return pucks;
    }

    public int Type()
    {
        return type;
    }
}
