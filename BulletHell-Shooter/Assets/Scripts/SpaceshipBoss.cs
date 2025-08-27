using System.Collections;
using UnityEngine;

/// <summary>
/// SpaceshipBoss controls the behavior of a boss character that performs
/// specific movement and shooting patterns at certain game time intervals.
/// It includes three distinct patterns that combine movement, rotation,
/// and shooting behaviors.
/// </summary>
public class SpaceshipBoss : MonoBehaviour
{
    public ShootController shootController;
    public RadialShootController radialShootController;
    public StarShootController starShootController;

    /// <summary>
    /// Subscribes to the OnMinuteChanged event when enabled to trigger patterns at specific times.
    /// </summary>
    public void OnEnable()
    {
        TimeManager.OnMinuteChanged += TimeCheck;
    }

    /// <summary>
    /// Unsubscribes from the OnMinuteChanged event when disabled to prevent errors or memory leaks.
    /// </summary>
    public void OnDisable()
    {
        TimeManager.OnMinuteChanged -= TimeCheck;
    }

    /// <summary>
    /// Checks the current game minute and starts the corresponding pattern when a specific time is reached.
    /// </summary>
    private void TimeCheck()
    {
        if (TimeManager.Minute == 3)
        {
            StartCoroutine(FirstPattern());
        }
        if (TimeManager.Minute == 12)
        {
            StartCoroutine(SecondPattern());
        }
        if (TimeManager.Minute == 24)
        {
            StartCoroutine(ThirdPattern());
        }
    }

    /// <summary>
    /// First pattern: Moves across the corners of the area, enables shooting, performs repetitive rotations,
    /// and aligns rotation to the nearest right angle after each move.
    /// </summary>
    private IEnumerator FirstPattern()
    {
        Vector3[] corners = {
            new Vector3(6.5f, 3.5f, 0f),
            new Vector3(-6.5f, 3.5f, 0f),
            new Vector3(-6.5f, -3.5f, 0f),
            new Vector3(6.5f, -3.5f, 0f)
        };

        Vector2[] rotationRanges = {
            new Vector2(90f, 180f),
            new Vector2(180f, -90f),
            new Vector2(-90f, 0f),
            new Vector2(0f, 90f)
        };

        float moveDuration = 0.5f;
        float rotationDuration = 0.15f;
        int rotationReps = 5;

        for (int i = 0; i < corners.Length; i++)
        {
            shootController.EnableShooting();
            yield return StartCoroutine(Rotate(rotationRanges[i].x - 20, rotationRanges[i].y + 20, rotationReps, rotationDuration));
            shootController.DisableShooting();

            SnapToRightAngle();

            if (i < corners.Length - 1)
            {
                yield return StartCoroutine(MoveTo(corners[i + 1], moveDuration));
            }
        }
    }

    /// <summary>
    /// Second pattern: Performs centered movements and rotations, then activates radial shooting
    /// while rotating in both directions.
    /// </summary>
    private IEnumerator SecondPattern()
    {
        yield return RotateTo(0f, 40f, 0.1f);
        yield return MoveTo(new Vector3(0f, 0f, 0f), 0.2f);
        yield return RotateTo(60f, -20f, 0.1f);

        yield return RotateTo(0f, 0f, 1f);
        radialShootController.EnableShooting();
        yield return RotateTo(0f, 0f, 3f);
        yield return RotateTo(0f, 180f, 1.5f);
        yield return RotateTo(180f, 360f, 1.5f);
        yield return RotateTo(0f, 0f, 3f);
        yield return RotateTo(360f, 180f, 1.5f);
        yield return RotateTo(180f, 0f, 1.5f);
        radialShootController.DisableShooting();
    }

    /// <summary>
    /// Third pattern: Performs full rotations while activating star-shaped shooting patterns.
    /// </summary>
    private IEnumerator ThirdPattern()
    {
        yield return RotateTo(0f, 0f, 2f);
        starShootController.EnableShooting();
        yield return RotateTo(0f, 90f, 1f);
        yield return RotateTo(90f, 180f, 1f);
        yield return RotateTo(180f, 270f, 1f);
        yield return RotateTo(270f, 360f, 1f);
        yield return RotateTo(0f, 90f, 1f);
        yield return RotateTo(90f, 180f, 1f);
        yield return RotateTo(180f, 270f, 1f);
        yield return RotateTo(270f, 360f, 1f);
        starShootController.DisableShooting();
    }

    /// <summary>
    /// Rotates the object back and forth between two angles a specified number of times over a given duration.
    /// </summary>
    private IEnumerator Rotate(float startZ, float targetZ, int repetitions, float duration)
    {
        for (int i = 0; i < repetitions; i++)
        {

            yield return StartCoroutine(RotateTo(startZ, targetZ, duration));
            yield return StartCoroutine(RotateTo(targetZ, startZ, duration));
        }
    }

    /// <summary>
    /// Smoothly rotates from a starting angle to a target angle over a given duration.
    /// </summary>
    private IEnumerator RotateTo(float startZ, float targetZ, float duration)
    {
        Quaternion startRot = Quaternion.Euler(0f, 0f, startZ);
        Quaternion targetRot = Quaternion.Euler(0f, 0f, targetZ);
        float elapsed = 0f;

        while (elapsed < duration)
        {
            transform.rotation = Quaternion.Lerp(startRot, targetRot, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = Quaternion.Euler(0f, 0f, targetZ);
    }

    /// <summary>
    /// Smoothly moves the object from its current position to a target position over a given duration.
    /// </summary>
    private IEnumerator MoveTo(Vector3 targetPos, float duration)
    {
        Vector3 startPos = transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
    }

    /// <summary>
    /// Snaps the current rotation to the nearest 90-degree angle.
    /// </summary>
    private void SnapToRightAngle()
    {
        float z = transform.eulerAngles.z;
        float snappedZ = Mathf.Round(z / 90f) * 90f;
        transform.rotation = Quaternion.Euler(0f, 0f, snappedZ);
    }
}