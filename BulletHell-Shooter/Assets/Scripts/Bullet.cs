using UnityEngine;

/// <summary>
/// This class represents a single bullet in the game. 
/// Handles movement, optional curve behavior, and self-deactivation after a maximum lifetime.
/// </summary>
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

    /// <summary>
    /// Updates the bullet's position every frame, applies optional curve, and disables it if maxTime is exceeded.
    /// </summary>
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

    /// <summary>
    /// Disables the bullet, resetting its lifetime counter and making it inactive.
    /// </summary>
    void Disable()
    {
        actualTime = 0f;
        gameObject.SetActive(false);
    }
}
