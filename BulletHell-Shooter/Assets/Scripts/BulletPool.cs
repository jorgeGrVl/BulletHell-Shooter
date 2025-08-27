using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// BulletPool manages bullet instantiation, reuse, and cleanup to improve performance.
/// It uses object pooling to minimize instantiation/destruction during gameplay.
/// This version also displays the number of active bullets visible on screen.
/// </summary>
public class BulletPool : MonoBehaviour
{
    public Bullet bulletPrefab;
    public int poolSize = 10;
    public TextMeshProUGUI bulletCounterText;

    private List<Bullet> bulletPool = new List<Bullet>();
    private Camera mainCamera;

    /// <summary>
    /// Initializes the bullet pool and stores a reference to the main camera.
    /// </summary>
    void Start()
    {
        mainCamera = Camera.main;
        AddBulletsToPool(poolSize);
    }

    /// <summary>
    /// Updates the bullet counter text to display the number of visible bullets.
    /// </summary>
    void Update()
    {
        if (bulletCounterText != null)
            bulletCounterText.text = "Bullets: " + GetVisibleBulletCount();
    }

    /// <summary>
    /// Adds a specified number of bullets to the pool and disables them.
    /// </summary>
    void AddBulletsToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Bullet bullet = Instantiate(bulletPrefab);
            bullet.gameObject.SetActive(false);
            bulletPool.Add(bullet);
            bullet.transform.parent = transform;
        }
    }

    /// <summary>
    /// Returns an inactive bullet from the pool or creates a new one if none are available.
    /// </summary>
    public Bullet RequestBullet()
    {
        for (int i = 0; i < bulletPool.Count; i++)
        {
            if (!bulletPool[i].gameObject.activeSelf)
            {
                bulletPool[i].gameObject.SetActive(true);
                return bulletPool[i];
            }
        }
        AddBulletsToPool(1);
        bulletPool[bulletPool.Count - 1].gameObject.SetActive(true);
        return bulletPool[bulletPool.Count - 1];
    }

    /// <summary>
    /// Returns the number of active bullets that are currently visible within the camera's frustum.
    /// </summary>
    public int GetVisibleBulletCount()
    {
        int count = 0;
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(mainCamera);

        for (int i = 0; i < bulletPool.Count; i++)
        {
            if (bulletPool[i].gameObject.activeSelf)
            {
                Renderer renderer = bulletPool[i].GetComponent<Renderer>();
                if (renderer != null)
                {
                    if (GeometryUtility.TestPlanesAABB(planes, renderer.bounds))
                        count++;
                }
                else
                {
                    if (IsPointVisible(bulletPool[i].transform.position))
                        count++;
                }
            }
        }
        return count;
    }

    /// <summary>
    /// Checks whether a point position is within the camera's viewport.
    /// </summary>
    private bool IsPointVisible(Vector3 worldPosition)
    {
        Vector3 viewportPos = mainCamera.WorldToViewportPoint(worldPosition);
        return viewportPos.x >= 0 && viewportPos.x <= 1 &&
               viewportPos.y >= 0 && viewportPos.y <= 1 &&
               viewportPos.z > 0;
    }
}