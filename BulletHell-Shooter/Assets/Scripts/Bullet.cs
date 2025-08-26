using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float maxTime = 3.5f;
    private float actualTime = 0f;

    public Vector3 velocity;
    public float curveStrength = 0f;

    private void OnEnable()
    {
        actualTime = 0f;
    }

    private void Update()
    {
        float dt = Time.deltaTime;

        if (Mathf.Abs(curveStrength) > 0.0001f && velocity.sqrMagnitude > 0.000001f)
        {
            Vector3 perp = new Vector3(-velocity.y, velocity.x, 0f).normalized;
            velocity -= perp * (curveStrength * dt);
        }

        transform.position += velocity * dt;
        actualTime += dt;

        if (actualTime > maxTime)
            Disable();
    }

    void Disable()
    {
        actualTime = 0f;
        gameObject.SetActive(false);
    }
}
