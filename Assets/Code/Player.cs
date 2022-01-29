using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private int lastPuck = -1;

    private Map map;
    private int type;
    private List<Puck> pucks;
    

    public float DirectionArrowSpeed { get; private set; } = 1;

    public Player(Map map, int type)
    {
        this.map = map;
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


    public void Draw()
    {
        List<Puck> dead = new List<Puck>();
        foreach (Puck item in GetPucks())
        {
            map.Pain(item.GetPosition(), item.GetSize(), type);
            CheckResoult res = map.CheckConnected(item.GetPosition(), type);
            if (!res.connected)
            {
                map.Pain(res.area, type == 1 ? 0 : 1);
                dead.Add(item);
            }
        }

        foreach (Puck item in dead)
        {
            GameObject.Destroy(item.gameObject);
            pucks.Remove(item);
        }
    }

    public bool IsAlive()
    {
        foreach (Puck puck in pucks)
        {
            if (puck.IsQueen())
            {
                return true;
            }
        }

        return false;
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
