using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VerticalBlow : Effect
{
    protected override void Play(int type)
    {
        HashSet<Vector2Int> effected = map.PainLine(transform.position, type, 4, false);
        ParticleCall(effected, type);
    }

}
