using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int health = 3;
    [SerializeField] GameObject explosionFX;
    [SerializeField] Vector3 explosionOffset;
    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

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
        AudioSource.PlayClipAtPoint(
            audioSource.clip,
            transform.position,
            audioSource.volume
        );
        Instantiate(explosionFX, transform.position + explosionOffset, Quaternion.identity);
        Destroy(gameObject);
    }
}
