using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float maxTime = 3.5f;
    private float actualTime = 0f;

    public Vector3 velocity;

    private void Update()
    {
        transform.position += velocity * Time.deltaTime;
        actualTime += Time.deltaTime;

        if (actualTime > maxTime)
            Disable();
    }

    void Disable()
    {
        actualTime = 0;
        gameObject.SetActive(false);
    }
}
