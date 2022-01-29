using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CheckResoult
{
    public readonly bool connected;
    public readonly HashSet<Vector2Int> area;

    public CheckResoult(bool connected, HashSet<Vector2Int> area)
    {
        this.connected = connected;
        this.area = area;
    }
}