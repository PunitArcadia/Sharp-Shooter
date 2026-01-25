using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int health = 10;

    private void Update()
    {
        if (health <= 0)
        {
            SelfDestroy();
        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Health" + damage);
        health -= damage;
    }

    public void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
