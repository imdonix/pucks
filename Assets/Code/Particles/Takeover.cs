using System.Collections;
using UnityEngine;


public class Takeover : Particle
{
    public override float Alive()
    {
        return 3F;
    }

    public void SetColor(Color color)
    {
        system.startColor = color;
    }
}
