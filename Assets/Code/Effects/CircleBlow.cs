using System.Collections;
using UnityEngine;


public class CircleBlow : Effect
{
    protected override void Play(int type)
    {
        map.Pain(transform.position, 20, type);
    }
}
