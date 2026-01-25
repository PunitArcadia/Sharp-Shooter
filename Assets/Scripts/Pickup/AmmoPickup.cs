using UnityEngine;

public class AmmoPickup : Pickup
{
    [SerializeField] int ammo = 10;

    protected override void OnPickup(ActiveWeapon activeWeapon)
    {
        activeWeapon.ChangeAmmo(ammo);
    }
}
