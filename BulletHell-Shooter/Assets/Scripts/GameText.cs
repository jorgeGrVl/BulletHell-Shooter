using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using PixelBattleText;

/// <summary>
/// GameText is responsible of displaying animated text messages on specific game events,
/// based on the in-game time provided by the TimeManager.
/// </summary>
public class GameText : MonoBehaviour
{
    public TextAnimation textAnimation;

    /// <summary>
    /// Subscribes to the OnMinuteChanged event when the object becomes active,
    /// so it can respond to time changes in the game.
    /// </summary>
    public void OnEnable()
    {
        TimeManager.OnMinuteChanged += TimeCheck;
    }


    /// <summary>
    /// Unsubscribes from the OnMinuteChanged event when the object is disabled,
    /// preventing memory leaks or unintended calls.
    /// </summary>
    public void OnDisable()
    {
        TimeManager.OnMinuteChanged -= TimeCheck;
    }

    /// <summary>
    /// Checks the current minute from TimeManager and triggers appropriate
    /// text display coroutines at defined times.
    /// </summary>
    private void TimeCheck()
    {
        if (TimeManager.Minute == 1)
        {
            StartCoroutine(ShowText1());
        }
        if (TimeManager.Minute == 37)
        {
            StartCoroutine(ShowText2());
        }
    }

    /// <summary>
    /// Displays the "BEGIN!" message using PixelBattleTextController.
    /// </summary>
    private IEnumerator ShowText1()
    {
        PixelBattleTextController.DisplayText(
        "BEGIN!", textAnimation, Vector3.one * 0.5f);
        yield return null;
    }

    /// <summary>
    /// Displays the "YOU WIN!" message using PixelBattleTextController.
    /// </summary>
    private IEnumerator ShowText2()
    {
        PixelBattleTextController.DisplayText(
        "YOU WIN!", textAnimation, Vector3.one * 0.5f);
        yield return null;
    }
}