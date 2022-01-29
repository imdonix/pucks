using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private int lastPuck = -1;

    private int type;
    private List<Puck> pucks = new List<Puck>();
    

    public float DirectionArrowSpeed { get; private set; } = 1;

    public Player(int type)
    {
        this.type = type;

        int y = type == 0 ? 5 : -5;
        Puck queen = GameObject.Instantiate(Manager.Instance.PuckPref);
        queen.transform.position = new Vector3(0, y, 0);
        queen.Set(this, true);
        pucks.Add(queen);

        Puck one = GameObject.Instantiate(Manager.Instance.PuckPref);
        one.transform.position = new Vector3(-5, y, 0);
        one.Set(this, false);
        pucks.Add(one);

        Puck two = GameObject.Instantiate(Manager.Instance.PuckPref);
        two.transform.position = new Vector3(5, y, 0);
        two.Set(this, false);
        pucks.Add(two);
    }

    public void StartTurn()
    {
        pucks[lastPuck++].Select();
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
