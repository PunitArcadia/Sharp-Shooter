using Cinemachine;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] WeaponSO startingWeapon;
    [SerializeField] WeaponSO weaponSO;
    [SerializeField] GameObject zoomEffect;
    [SerializeField] float zoomSens;
    [SerializeField] TMP_Text currentAmmoUI;
    [SerializeField] TMP_Text totalAmmoUI;

    Weapon currentWeapon;
    CinemachineVirtualCamera virtualCamera;
    FirstPersonController fpc;

    float initTime;
    float defaultFOV;
    float defaultZoomSens;
    int currentAmmo;

    private void Start()
    {
        currentWeapon = FindFirstObjectByType<Weapon>();
        SwitchWeapon(startingWeapon);
        virtualCamera = FindFirstObjectByType<CinemachineVirtualCamera>();
        fpc = FindFirstObjectByType<FirstPersonController>();
        initTime = 10;
        defaultFOV = virtualCamera.m_Lens.FieldOfView;
        defaultZoomSens = 1;
    }
    private void Update()
    {
        initTime += Time.deltaTime;
        HandleShoot();
        HandleZoom();
    }

    private void HandleShoot()
    {
        if (weaponSO.fireMode == FireMode.SemiAuto)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && currentAmmo > 0)
            {
                if (initTime >= weaponSO.fireRate)
                {
                    currentWeapon.StartFiring(weaponSO);
                    currentWeapon.Shoot(weaponSO);
                    ChangeAmmo(-1);
                    initTime = 0;
                }
            }
        }
        else if (weaponSO.fireMode == FireMode.FullAuto)
        {
            if (Input.GetKey(KeyCode.Mouse0) && currentAmmo > 0)
            {
                if (initTime >= weaponSO.fireRate)
                {
                    currentWeapon.StartFiring(weaponSO);
                    currentWeapon.Shoot(weaponSO);
                    ChangeAmmo(-1);
                    initTime = 0;
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            currentWeapon.StopFiring();
        }
    }

    private void HandleZoom()
    {
        if (!weaponSO.canZoom) return;
        if (Input.GetKey(KeyCode.Mouse1)) 
        {
            virtualCamera.m_Lens.FieldOfView = weaponSO.zoomFOV;
            zoomEffect.SetActive(true);
            fpc.ChangeSens(zoomSens);
        }
        else
        {
            virtualCamera.m_Lens.FieldOfView = defaultFOV;
            zoomEffect.SetActive(false);
            fpc.ChangeSens(defaultZoomSens);
        }
    }

    public void SwitchWeapon(WeaponSO weaponSO)
    {
        Debug.Log("Weapon Changed! " +  weaponSO.name);

        if(currentWeapon)
        {
            Destroy(currentWeapon.gameObject);
        }
        totalAmmoUI.text = weaponSO.magazineSize.ToString("D2");
        currentAmmo = weaponSO.magazineSize;
        currentAmmoUI.text = weaponSO.magazineSize.ToString("D2");
        Weapon NewWeapon = Instantiate(weaponSO.weaponPrefab, transform).GetComponent<Weapon>();
        currentWeapon = NewWeapon;
        this.weaponSO = weaponSO;
    }

    public void ChangeAmmo(int amount)
    {
        currentAmmo += amount;
        currentAmmoUI.text = currentAmmo.ToString("D2");
    }
}
