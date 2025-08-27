using System.Collections;
using UnityEngine;

public class SpaceshipBoss : MonoBehaviour
{
    public ShootController shootController;
    public RadialShootController radialShootController;
    public StarShootController starShootController;

    public void OnEnable()
    {
        TimeManager.OnMinuteChanged += TimeCheck;
    }

    public void OnDisable()
    {
        TimeManager.OnMinuteChanged -= TimeCheck;
    }

    private void TimeCheck()
    {
        if (TimeManager.Minute == 3)
        {
            StartCoroutine(FirstPattern());
        }
        if (TimeManager.Minute == 11)
        {
            StartCoroutine(SecondPattern());
        }
        if (TimeManager.Minute == 21)
        {
            StartCoroutine(ThirdPattern());
        }
    }

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

    private IEnumerator SecondPattern()
    {
        yield return RotateTo(0f, 40f, 0.1f);
        yield return MoveTo(new Vector3(0f, 0f, 0f), 0.2f);
        yield return RotateTo(60f, -20f, 0.1f);

        yield return RotateTo(0f, 0f, 1f);
        radialShootController.EnableShooting();
        yield return RotateTo(0f, 0f, 2f);
        yield return RotateTo(0f, 180f, 1.5f);
        yield return RotateTo(180f, 360f, 1.5f);
        yield return RotateTo(0f, 0f, 2f);
        yield return RotateTo(360f, 180f, 1.5f);
        yield return RotateTo(180f, 0f, 1.5f);
        radialShootController.DisableShooting();
    }

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

    private IEnumerator Rotate(float startZ, float targetZ, int repetitions, float duration)
    {
        for (int i = 0; i < repetitions; i++)
        {

            yield return StartCoroutine(RotateTo(startZ, targetZ, duration));
            yield return StartCoroutine(RotateTo(targetZ, startZ, duration));
        }
    }

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
    
    private void SnapToRightAngle()
    {
        float z = transform.eulerAngles.z;
        float snappedZ = Mathf.Round(z / 90f) * 90f;
        transform.rotation = Quaternion.Euler(0f, 0f, snappedZ);
    }
}