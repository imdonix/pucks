using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int lastPuck = -1;

    private List<Puck> pucks = new List<Puck>();

    public float DirectionArrowSpeed { get; private set; } = 1;

    public void StartTurn()
    {
        pucks[lastPuck++].Select();
    }

    public void EndTurn()
    {
        
    }
}
