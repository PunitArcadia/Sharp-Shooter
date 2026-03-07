using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject hitFx;
    [SerializeField] private ParticleSystem muzzleFx;
    [SerializeField] private LayerMask layerMask;

    private AudioSource audioSource;
    private Camera mainCamera;

    private WeaponSO currentWeaponData;

    private bool isFiring;
    private bool triggerReleasedSinceLastShot = true;

    private float nextFireTime;

    private int currentMagazineAmmo;
    private int reserveAmmo;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        mainCamera = Camera.main;
    }

    // Called when weapon is equipped
    public void Initialize(WeaponSO weaponData)
    {
        currentWeaponData = weaponData;
        currentMagazineAmmo = weaponData.magazineSize;
        reserveAmmo = weaponData.reserveAmmo;
        Debug.Log("Initialized weapon with mag: " + currentMagazineAmmo);
    }

    public void StartFiring()
    {
        if (currentWeaponData == null) return;
        if (currentMagazineAmmo <= 0) return;

        isFiring = true;

        if (audioSource != null && currentWeaponData.shootSound != null)
        {
            audioSource.clip = currentWeaponData.shootSound;
            audioSource.Play();
        }
    }

    public void StopFiring()
    {
        isFiring = false;
        triggerReleasedSinceLastShot = true;

        if (audioSource != null)
            audioSource.Stop();
    }

    public void Shoot()
    {
        if (!isFiring) return;
        if (currentWeaponData == null) return;
        if (currentMagazineAmmo <= 0) 
        {
            StopFiring();
            return; 
        }

        if (currentWeaponData.fireMode == FireMode.SemiAuto)
        {
            if (!triggerReleasedSinceLastShot) return;

            Fire();
            triggerReleasedSinceLastShot = false;
        }
        else // FullAuto
        {
            if (Time.time < nextFireTime) return;

            nextFireTime = Time.time + (1f / currentWeaponData.fireRate);
            Fire();
        }
    }

    private void Fire()
    {
        currentMagazineAmmo--;

        muzzleFx?.Play();

        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out RaycastHit hit, Mathf.Infinity, layerMask, QueryTriggerInteraction.Ignore))
        {
            Instantiate(hitFx, hit.point, Quaternion.identity);
            IDamageable damageable = hit.transform.GetComponent<IDamageable>();
            damageable?.TakeDamage(currentWeaponData.damage);
        }
    }

    public void Reload()
    {
        if (currentWeaponData == null) return;

        if (currentMagazineAmmo >= currentWeaponData.magazineSize) return;

        if (reserveAmmo <= 0) return;

        int neededAmmo = currentWeaponData.magazineSize - currentMagazineAmmo;

        int ammoToReload = Mathf.Min(neededAmmo, reserveAmmo);

        currentMagazineAmmo += ammoToReload;
        reserveAmmo -= ammoToReload;
    }

    public int GetCurrentAmmo()
    {
        return currentMagazineAmmo;
    }

    public int GetReserveAmmo()
    {
        return reserveAmmo;
    }

    public int GetMagazineSize()
    {
        return currentWeaponData != null ? currentWeaponData.magazineSize : 0;
    }

    public void AddAmmo(int amount)
    {
        if (currentWeaponData == null) return;

        reserveAmmo += amount;
    }
}