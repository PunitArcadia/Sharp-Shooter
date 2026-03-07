using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 3;

    [Header("Effects")]
    [SerializeField] private GameObject explosionFX;
    [SerializeField] private Vector3 explosionOffset;
    [SerializeField] private AudioClip deathSound;

    private int currentHealth;
    private bool isDead;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        if (deathSound != null)
        {
            AudioSource.PlayClipAtPoint(
                deathSound,
                transform.position
            );
        }
        if (explosionFX != null)
        {
            Instantiate(
                explosionFX,
                transform.position + explosionOffset,
                Quaternion.identity
            );
        }
        EnemyEvents.OnEnemyKilled?.Invoke();
        Destroy(gameObject);
    }
}
