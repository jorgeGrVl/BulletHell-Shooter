using System.Collections;
using UnityEngine;

public class SpaceshipBoss : MonoBehaviour
{
    public ShootController shootController;
    private void Start()
    {
        StartCoroutine(PatternRoutine());
    }

    private IEnumerator PatternRoutine()
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
        float rotationDuration = 0.2f; 
        int rotationReps = 5;   

        for (int i = 0; i < corners.Length; i++)
        {
            yield return StartCoroutine(RotateSquare(rotationRanges[i].x - 20, rotationRanges[i].y + 20, rotationReps, rotationDuration));

            if (i < corners.Length - 1)
            {
                yield return StartCoroutine(MoveTo(corners[i + 1], moveDuration));
            }
        }
    }

    private IEnumerator RotateSquare(float startZ, float targetZ, int repetitions, float duration)
    {
        shootController.EnableShooting();
        for (int i = 0; i < repetitions; i++)
        {

            yield return StartCoroutine(RotateTo(startZ, targetZ, duration));
            yield return StartCoroutine(RotateTo(targetZ, startZ, duration));
        }
        shootController.DisableShooting();
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

        transform.rotation = Quaternion.Euler(0f, 0f, targetZ + 20);
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
}
