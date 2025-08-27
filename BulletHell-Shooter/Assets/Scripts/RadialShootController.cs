using UnityEngine;

/// <summary>
/// RadialShootController is responsible for enabling or disabling a RadialShoot component at runtime.
/// This allows controlling when radial shooting starts or stops without destroying the component.
/// </summary>
public class RadialShootController : MonoBehaviour
{
    public RadialShoot shoot;

    /// <summary>
    /// Enables the RadialShoot component if it exists.
    /// </summary>
    public void EnableShooting()
    {
        if (shoot != null)
            shoot.enabled = true;
    }

    /// <summary>
    /// Disables the RadialShoot component if it exists.
    /// </summary>
    public void DisableShooting()
    {
        if (shoot != null)
            shoot.enabled = false;
    }
}