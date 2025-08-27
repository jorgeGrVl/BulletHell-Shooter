using UnityEngine;

/// <summary>
/// RadialShoot handles the logic for shooting multiple bullets in a circular pattern (radial shot).
/// It uses a BulletPool to spawn bullets, applies speed, direction, and curve behavior.
/// </summary>
public class RadialShoot : MonoBehaviour
{
    public int bullets;
    public float cooldown;
    public float speed;
    public BulletPool bulletPool;

    public float curveStrength = 1f;
    private float cooldownTime = 0f;

    /// <summary>
    /// Updates the cooldown timer and triggers a radial shot when the cooldown reaches zero.
    /// </summary>
    void Update()
    {
        cooldownTime -= Time.deltaTime;

        if (cooldownTime <= 0f)
        {
            RadialShot(transform.position, transform.up, bullets, cooldown, speed, curveStrength);
            cooldownTime += cooldown;
        }
    }

    /// <summary>
    /// Requests a bullet from the pool and initializes its position, velocity, and curve strength.
    /// </summary>
    void Shot(Vector3 origin, Vector3 velocity, float curveStrength)
    {
        Bullet bullet = bulletPool.RequestBullet();
        bullet.transform.position = origin;
        bullet.velocity = velocity;
        bullet.curveStrength = curveStrength;
    }

    /// <summary>
    /// Shoots multiple bullets in a radial pattern by dividing 360° into equal angles.
    /// Alternates curve strength direction for variation.
    /// </summary>
    void RadialShot(Vector3 origin, Vector3 direction, int bullets, float cooldown, float speed, float curveStrength)
    {
        float angleBetweenBullets = 360f / bullets;

        for (int i = 0; i < bullets; i++)
        {
            float bulletDirectionAngle = angleBetweenBullets * i;
            Vector2 bulletDirection = Rotate(direction, bulletDirectionAngle);
            float finalCurve = (i % 2 == 0) ? curveStrength : -curveStrength;

            Shot(origin, bulletDirection * speed, finalCurve);
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
