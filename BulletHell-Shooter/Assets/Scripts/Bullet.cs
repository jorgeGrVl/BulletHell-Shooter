using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float maxTime = 3f;
    private float actualTime = 0f;

    public Vector3 velocity;
 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity * Time.deltaTime;
        actualTime += Time.deltaTime;

        if (actualTime > maxTime)
            Destroy(gameObject);
    }
}
