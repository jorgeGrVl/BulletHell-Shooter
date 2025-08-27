using UnityEngine;

/// <summary>
/// StarShoot is responsible for firing bullets in a star-shaped pattern at fixed intervals.
/// It uses trigonometric calculations to create the star shape by adjusting bullet directions and speeds.
/// </summary>
public class StarShoot : MonoBehaviour
{
    public int bullets;
    public float cooldown;
    public float speed;
    public BulletPool bulletPool;

    private float cooldownTime = 0f;

    /// <summary>
    /// Updates the cooldown timer and triggers shooting when the cooldown reaches zero.
    /// </summary>
    void Update()
    {
        cooldownTime -= Time.deltaTime;

        if (cooldownTime <= 0f)
        {
            StarShot(transform.position, transform.up, bullets, cooldown, speed);
            cooldownTime += cooldown;
        }
    }

    /// <summary>
    /// Requests a bullet from the pool and initializes its position, velocity, and curve strength.
    /// </summary>
    void Shot(Vector3 origin, Vector3 velocity)
    {
        Bullet bullet = bulletPool.RequestBullet();
        bullet.transform.position = origin;
        bullet.velocity = velocity;
        bullet.curveStrength = 0;
    }

    /// <summary>
    /// Fires multiple bullets arranged in a star pattern by calculating alternating bullet speeds based on a cosine function.
    /// </summary>
    void StarShot(Vector3 origin, Vector3 direction, int bullets, float cooldown, float speed)
    {
        float angleBetweenBullets = 360f / bullets;
        int starPoints = 5;

        for (int i = 0; i < bullets; i++)
        {
            float bulletDirectionAngle = angleBetweenBullets * i;
            Vector2 bulletDirection = Rotate(direction, bulletDirectionAngle);

            float radius = Mathf.Cos(starPoints * Mathf.Deg2Rad * bulletDirectionAngle);
            float normalized = (radius + 1f) / 2f;
            float bulletSpeed = Mathf.Lerp(0.5f, 1f, normalized) * speed;

            Shot(origin, bulletDirection * bulletSpeed);
        }
    }

    /// <summary>
    /// Rotates a vector by a given angle in degrees around the Z-axis.
    /// </summary>
    Vector2 Rotate(Vector2 originalVector, float rotateAngle)
    {
        Quaternion rotation = Quaternion.AngleAxis(rotateAngle, Vector3.forward);
        return rotation * originalVector;
    }
}
