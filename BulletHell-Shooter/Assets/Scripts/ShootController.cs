using UnityEngine;

public class ShootController : MonoBehaviour
{
    public Shoot shoot;

    public void EnableShooting()
    {
        if (shoot != null)
            shoot.enabled = true;
    }

    public void DisableShooting()
    {
        if (shoot != null)
            shoot.enabled = false;
    }
}
