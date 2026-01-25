using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 100f;
    const string PLAYER_TAG = "Player";

    private void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(PLAYER_TAG))
        {
            //activeWeapon.SwitchWeapon(weaponSO);
            ActiveWeapon activeWeapon = other.GetComponentInChildren<ActiveWeapon>();
            OnPickup(activeWeapon);
            Destroy(gameObject);
        }
    }

    protected abstract void OnPickup(ActiveWeapon activeWeapon);
}
