using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    public int currentHealth;
    public int MaxHealth => maxHealth;

    public bool IsDead { get; private set; }

    void Start()
    {
        currentHealth = maxHealth;
        IsDead = false;
    }

    void Update()
    {
        if (!IsDead && currentHealth <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(int damage)
    {
        if (IsDead) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    void Die()
    {
        IsDead = true;

        Debug.Log("Die() called");

        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager.Instance IS NULL");
        }
        else
        {
            Debug.Log("Calling PlayerLost()");
            GameManager.Instance.PlayerLost();
        }
        gameObject.SetActive(false);
    }
}