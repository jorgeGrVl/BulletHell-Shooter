using UnityEngine;

/// <summary>
/// ShootController is responsible for enabling or disabling a Shoot component at runtime.
/// This allows controlling when shooting starts or stops without destroying the component.
/// </summary>i
public class ShootController : MonoBehaviour
{
    public Shoot shoot;

    /// <summary>
    /// Enables the Shoot component if it exists.
    /// </summary>
    public void EnableShooting()
    {
        if (shoot != null)
            shoot.enabled = true;
    }

    /// <summary>
    /// Disables the Shoot component if it exists.
    /// </summary>
    public void DisableShooting()
    {
        if (shoot != null)
            shoot.enabled = false;
    }
}
