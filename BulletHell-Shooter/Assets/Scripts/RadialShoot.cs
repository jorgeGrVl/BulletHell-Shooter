using UnityEngine;

public class RadialShoot : MonoBehaviour
{
    public int bullets;
    public float cooldown;
    public float speed;
    public BulletPool bulletPool;

    public float curveStrength = 1f;
    private float cooldownTime = 0f;

    void Update()
    {
        cooldownTime -= Time.deltaTime;

        if (cooldownTime <= 0f)
        {
            RadialShot(transform.position, transform.up, bullets, cooldown, speed, curveStrength);
            cooldownTime += cooldown;
        }
    }

    void Shot(Vector3 origin, Vector3 velocity, float curveStrength)
    {
        Bullet bullet = bulletPool.RequestBullet();
        bullet.transform.position = origin;
        bullet.velocity = velocity;
        bullet.curveStrength = curveStrength;
    }

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

    Vector2 Rotate(Vector2 originalVector, float rotateAngle)
    {
        Quaternion rotation = Quaternion.AngleAxis(rotateAngle, Vector3.forward);
        return rotation * originalVector;
    }
}
