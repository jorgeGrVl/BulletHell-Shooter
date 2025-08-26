using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float cooldown;
    public float speed;
    public BulletPool bulletPool;

    private float cooldownTime = 0f;

    void Update()
    {
        cooldownTime -= Time.deltaTime;

        if (cooldownTime <= 0f)
        {
            Shot(transform.position, transform.up * speed);
            cooldownTime += cooldown;
        }
    }

    void Shot(Vector3 origin, Vector3 velocity)
    {
        Bullet bullet = bulletPool.RequestBullet();
        bullet.transform.position = origin;
        bullet.velocity = velocity;
        bullet.curveStrength = 0;
    }
}
