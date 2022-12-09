using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public class OnSecondPassedEventArgs : EventArgs
    {
        public int sec;
    }

    public static EventHandler<OnSecondPassedEventArgs> OnSecondPassed;
    private const float SEC = 1f;

    private int tick;
    private float tickTimer;

    private void Awake()
    {
        tick = 0;
    }

    // Update is called once per frame
    void Update()
    {
        tickTimer += Time.deltaTime;

        if (tickTimer >= SEC)
        {
            tickTimer -= SEC;
            tick++;
            if (OnSecondPassed != null) OnSecondPassed(this, new OnSecondPassedEventArgs { sec = tick });
        }
    }
}
