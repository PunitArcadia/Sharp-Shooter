using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int health = 3;
    [SerializeField] GameObject explosionFX;
    [SerializeField] Vector3 explosionOffset;

    private void Update()
    {
        if (health <= 0)
        {
            SelfDestroy();
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    public void SelfDestroy()
    {
        //Destroying self Stuff
        Instantiate(explosionFX, transform.position + explosionOffset, Quaternion.identity);
        Destroy(gameObject);
    }
}
