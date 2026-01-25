using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "Weapons/WeaponSO")]
public class WeaponSO : ScriptableObject
{
    public GameObject weaponPrefab;
    public int damage;
    public int magazineSize;
    public float fireRate;
    public bool canZoom;
    public float zoomFOV;
}