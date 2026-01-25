using UnityEngine;

public class Weapon : MonoBehaviour
{
    RaycastHit hit;
    [SerializeField] GameObject hitFx;
    [SerializeField] ParticleSystem muzzleFx;
    [SerializeField] LayerMask layerMask;

    public void Shoot(WeaponSO weaponSO)
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, layerMask, QueryTriggerInteraction.Ignore))
        {
            muzzleFx.Play();
            Instantiate(hitFx, hit.point, Random.rotation);
            //Debug.Log(hit.transform.gameObject.name);
            EnemyHealth eh = hit.transform.gameObject.GetComponent<EnemyHealth>();
            eh?.TakeDamage(weaponSO.damage);
        }
    }
}
