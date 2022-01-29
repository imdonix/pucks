using System.Collections;
using UnityEngine;


public class TimeControl : MonoSingleton<TimeControl>
{
    private float countdown;

    private void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown < 0)
        {
            Time.timeScale = 1;
        }
    }

    public void SetTimeFor(float scale, float realTime)
    {
        Time.timeScale = scale;
        countdown = realTime * scale;
    }
}
