using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private int selectedPuckIndex = 0;
    private Map map;
    private int type;
    private List<Puck> pucks;

    public Player(Map map, int type)
    {
        this.map = map;
        this.type = type;
        this.pucks = new List<Puck>();

        int y = type == 0 ? 7 : -7;

        Puck queen = GameObject.Instantiate(Manager.Instance.PuckPref);
        queen.transform.position = new Vector3(0, y, 0);
        queen.transform.localScale = Vector3.one * 3;
        queen.Set(this, true, 8);
        pucks.Add(queen);

        Puck one = GameObject.Instantiate(Manager.Instance.PuckPref);
        one.transform.position = new Vector3(-7, y, 0);
        one.transform.localScale = Vector3.one * 2;
        one.Set(this, false, 5.5F);
        pucks.Add(one);

        Puck two = GameObject.Instantiate(Manager.Instance.PuckPref);
        two.transform.position = new Vector3(7, y, 0);
        two.transform.localScale = Vector3.one * 2;
        two.Set(this, false, 5.5F);
        
        pucks.Add(two);
    }


    public void SelectPuck(int next)
    {
        if(pucks.Count > 0)
        {
            pucks[selectedPuckIndex % pucks.Count].Deselect();
            selectedPuckIndex += next;
            if (selectedPuckIndex < 0)
            {
                selectedPuckIndex = pucks.Count-1;
            }
            pucks[selectedPuckIndex % pucks.Count].Select();
        }
    }

    public Puck GetSelected()
    {
        return pucks[selectedPuckIndex % pucks.Count];
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
                foreach (Vector2Int grid in res.area)
                {
                    if (grid.x % 5 == 0 || grid.y % 5 == 0)
                    {
                        Takeover over = PPool.Instance.Spawn<Takeover>();
                        over.transform.position = new Vector3(
                            (grid.x - Map.SIZE / 2) / 5,
                            (grid.y - Map.SIZE / 2) / 5,
                            -1);
                        over.SetColor(map.GetColor(type));
                    }
                }
                TimeControl.Instance.SetTimeFor(0.5F, 2);

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
