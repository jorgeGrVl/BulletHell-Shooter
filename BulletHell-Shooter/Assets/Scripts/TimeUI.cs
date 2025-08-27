using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// TimeUI updates a TextMeshProUGUI element to display the current in-game time (hours and minutes).
/// It listens to time change events from TimeManager to update the UI dynamically.
/// </summary>
public class TimeUI : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    /// <summary>
    /// Subscribes to the TimeManager events when the object is enabled to update the UI whenever time changes.
    /// </summary>
    private void OnEnable()
    {
        TimeManager.OnMinuteChanged += UpdateTime;
        TimeManager.OnHourChanged += UpdateTime;
    }

    /// <summary>
    /// Unsubscribes from the TimeManager events when the object is disabled to avoid memory leaks or errors.
    /// </summary>
    private void OnDisable()
    {
        TimeManager.OnMinuteChanged -= UpdateTime;
        TimeManager.OnHourChanged -= UpdateTime;
    }

    /// <summary>
    /// Updates the timeText UI element with the current hour and minute in HH:MM format.
    /// </summary>
    private void UpdateTime()
    {
        timeText.text = $"{TimeManager.Hour.ToString("00")}:{TimeManager.Minute - 1:00}";
    }
}