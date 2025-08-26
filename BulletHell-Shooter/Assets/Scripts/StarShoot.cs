using UnityEngine;

public class StarShoot : MonoBehaviour
{
    public int bullets;
    public float cooldown;
    public float speed;
    public BulletPool bulletPool;

    private float cooldownTime = 0f;

    void Update()
    {
        cooldownTime -= Time.deltaTime;

        if (cooldownTime <= 0f)
        {
            StarShot(transform.position, transform.up, bullets, cooldown, speed);
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

    Vector2 Rotate(Vector2 originalVector, float rotateAngle)
    {
        Quaternion rotation = Quaternion.AngleAxis(rotateAngle, Vector3.forward);
        return rotation * originalVector;
    }
}
