using System.Collections;
using UnityEngine;


public class Takeover : Particle
{
    public override float Alive()
    {
        return 1.25F;
    }

    public void SetColor(Color color)
    {
        system.startColor = color;
    }
}
