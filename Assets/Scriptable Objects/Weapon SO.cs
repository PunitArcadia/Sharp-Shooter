using UnityEngine;

public enum FireMode
{
    SemiAuto,
    FullAuto
}

[CreateAssetMenu(fileName = "WeaponSO", menuName = "Weapons/WeaponSO")]
public class WeaponSO : ScriptableObject
{
    public GameObject weaponPrefab;
    public int damage;
    public int magazineSize;
    public float fireRate;
    public FireMode fireMode;
    public bool canZoom;
    public float zoomFOV;
    public AudioClip shootSound;
}