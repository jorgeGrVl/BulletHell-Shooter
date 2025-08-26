using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BulletPool : MonoBehaviour
{
    public Bullet bulletPrefab;
    public int poolSize = 10;
    public TextMeshProUGUI bulletCounterText;

    private List<Bullet> bulletPool = new List<Bullet>();

    void Start()
    {
        AddBulletsToPool(poolSize);
    }

    void Update()
    {
        if (bulletCounterText != null)
            bulletCounterText.text = "Bullets: " + GetActiveBulletCount();
    }

    void AddBulletsToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Bullet bullet = Instantiate(bulletPrefab);
            bullet.gameObject.SetActive(false);
            bulletPool.Add(bullet);
            bullet.transform.parent = transform;
        }
    }

    public Bullet RequestBullet()
    {
        for (int i = 0; i < bulletPool.Count; i++)
        {
            if (!bulletPool[i].gameObject.activeSelf)
            {
                bulletPool[i].gameObject.SetActive(true);
                return bulletPool[i];
            }
        }
        AddBulletsToPool(1);
        bulletPool[bulletPool.Count - 1].gameObject.SetActive(true);
        return bulletPool[bulletPool.Count - 1];
    }

    public int GetActiveBulletCount()
    {
        int count = 0;
        for (int i = 0; i < bulletPool.Count; i++)
        {
            if (bulletPool[i].gameObject.activeSelf)
                count++;
        }
        return count;
    }
}