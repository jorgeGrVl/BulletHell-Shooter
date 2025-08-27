using UnityEngine;

/// <summary>
/// StarShootController is responsible for enabling or disabling a StarShoot component at runtime.
/// This allows controlling when star shooting starts or stops without destroying the component.
/// </summary>
public class StarShootController : MonoBehaviour
{
    public StarShoot shoot;

    /// <summary>
    /// Enables the StarShoot component if it exists.
    /// </summary>
    public void EnableShooting()
    {
        if (shoot != null)
            shoot.enabled = true;
    }

    /// <summary>
    /// Disables the StarShoot component if it exists.
    /// </summary>
    public void DisableShooting()
    {
        if (shoot != null)
            shoot.enabled = false;
    }
}