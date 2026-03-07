using UnityEngine;

public class AmmoPickup : Pickup
{
    [SerializeField] int ammo = 10;

    protected override void OnPickup(ActiveWeapon activeWeapon)
    {
        Weapon weapon = activeWeapon.GetCurrentWeapon();
        if (weapon != null)
        {
            weapon.AddAmmo(ammo);
        }
    }
}
