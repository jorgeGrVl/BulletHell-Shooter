using UnityEngine;

public class StarShootController : MonoBehaviour
{
    public StarShoot shoot;

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