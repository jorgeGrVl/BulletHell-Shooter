using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// TimeManager simulates an in-game clock system that tracks minutes and hours.
/// It triggers events when a minute or hour changes, allowing other scripts to react accordingly.
/// </summary>
public class TimeManager : MonoBehaviour
{
    public static Action OnMinuteChanged;
    public static Action OnHourChanged;

    public static int Minute { get; private set; }
    public static int Hour { get; private set; }

    private float minuteToRealTime = 1f;
    private float timer;

    /// <summary>
    /// Initializes the time values and sets the starting timer for minute updates.
    /// </summary>
    void Start()
    {
        Minute = 0;
        Hour = 0;
        timer = minuteToRealTime;
    }

    /// <summary>
    /// Updates the in-game time based on real time passing and triggers events when a minute or hour changes.
    /// </summary>
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Minute++;

            OnMinuteChanged?.Invoke();

            if (Minute >= 60)
            {
                Hour++;
                OnHourChanged?.Invoke();
                Minute = 0;
            }

            timer = minuteToRealTime;
        }
    }
}
