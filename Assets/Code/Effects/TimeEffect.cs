using System.Collections;
using UnityEngine;


public class TimeEffect : Effect
{
    protected override void Play(int type)
    {
        control.SetTimeFor(2, 5);
    }
}
