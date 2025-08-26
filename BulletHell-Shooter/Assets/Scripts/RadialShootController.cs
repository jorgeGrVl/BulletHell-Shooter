using UnityEngine;

public class RadialShootController : MonoBehaviour
{
    public RadialShoot shoot;

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