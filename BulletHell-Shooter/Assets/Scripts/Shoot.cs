using UnityEngine;

/// <summary>
/// Shoot handles firing bullets at a constant rate in a specified direction.
/// It uses a cooldown system to control the firing rate and retrieves bullets from a BulletPool for performance.
/// </summary>
public class Shoot : MonoBehaviour
{
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
            Shot(transform.position, transform.up * speed);
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
}
