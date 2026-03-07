using Cinemachine;
using StarterAssets;
using TMPro;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    [Header("Starting Weapon")]
    [SerializeField] private WeaponSO startingWeapon;

    [Header("Zoom")]
    [SerializeField] private GameObject zoomEffect;
    [SerializeField] private float zoomSensitivity = 0.5f;

    [Header("UI")]
    [SerializeField] private TMP_Text currentAmmoUI;
    [SerializeField] private TMP_Text totalAmmoUI;
    [SerializeField] private TMP_Text weaponNameUI;

    private Weapon currentWeapon;
    private WeaponSO currentWeaponData;

    private CinemachineVirtualCamera virtualCamera;
    private FirstPersonController firstPersonController;

    private float defaultFOV;
    private float defaultSensitivity = 1f;

    private void Start()
    {
        virtualCamera = FindFirstObjectByType<CinemachineVirtualCamera>();
        firstPersonController = FindFirstObjectByType<FirstPersonController>();

        defaultFOV = virtualCamera.m_Lens.FieldOfView;

        SwitchWeapon(startingWeapon);
    }

    private void Update()
    {
        HandleInput();
        HandleZoom();
        UpdateAmmoUI();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            currentWeapon.StartFiring();
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            currentWeapon.Shoot();
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            currentWeapon.StopFiring();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            currentWeapon.Reload();
        }
    }

    private void HandleZoom()
    {
        if (currentWeaponData == null) return;
        if (!currentWeaponData.canZoom) return;

        if (Input.GetKey(KeyCode.Mouse1))
        {
            virtualCamera.m_Lens.FieldOfView = currentWeaponData.zoomFOV;
            zoomEffect.SetActive(true);
            firstPersonController.ChangeSens(zoomSensitivity);
        }
        else
        {
            virtualCamera.m_Lens.FieldOfView = defaultFOV;
            zoomEffect.SetActive(false);
            firstPersonController.ChangeSens(defaultSensitivity);
        }
    }

    public void SwitchWeapon(WeaponSO weaponData)
    {
        Debug.Log("Weapon switched to: " + weaponData.name);
        if (currentWeapon)
        {
            Debug.Log("Destroyed weapon:" +  currentWeapon.name);
            Destroy(currentWeapon.gameObject);
        }

        currentWeaponData = weaponData;

        Debug.Log("Current weapon data:" + currentWeaponData.name);

        GameObject newWeaponObject =
            Instantiate(weaponData.weaponPrefab, transform);

        Debug.Log("new weapon object:" + newWeaponObject);

        currentWeapon = newWeaponObject.GetComponent<Weapon>();
        Debug.Log("currentWeapon:" + currentWeapon);

        currentWeapon.Initialize(weaponData);
        Debug.Log("currentWeapon after initialization:" + currentWeapon);
    }

    private void UpdateAmmoUI()
    {
        if (currentWeapon == null) return;

        currentAmmoUI.text =
            currentWeapon.GetCurrentAmmo().ToString("D2");

        totalAmmoUI.text =
            currentWeapon.GetReserveAmmo().ToString("D2");
    }
    public Weapon GetCurrentWeapon()
    {
        return currentWeapon;
    }
}