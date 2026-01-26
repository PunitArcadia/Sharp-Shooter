using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Slider healthBar;
    public TextMeshProUGUI healthText;
    public Image glowImage;

    [Header("Animation")]
    public float smoothSpeed = 6f;
    public float pulseSpeed = 3f;

    void Start()
    {
        healthBar.maxValue = playerHealth.MaxHealth;
        healthBar.value = playerHealth.currentHealth;
    }

    void Update()
    {
        if (playerHealth == null) return;

        // Smooth bar animation
        healthBar.value = Mathf.Lerp(
            healthBar.value,
            playerHealth.currentHealth,
            Time.deltaTime * smoothSpeed
        );

        // Update text
        healthText.text =
            playerHealth.currentHealth + " / " + playerHealth.MaxHealth;

        float healthPercent =
            (float)playerHealth.currentHealth / playerHealth.MaxHealth;

        // LOW HEALTH EFFECTS
        if (healthPercent <= 0.25f)
        {
            float pulse = Mathf.Abs(Mathf.Sin(Time.time * pulseSpeed));

            glowImage.color =
                new Color(1f, 0f, 0f, pulse * 0.6f);

            healthBar.transform.localScale =
                Vector3.one * (1f + pulse * 0.05f);
        }
        else
        {
            glowImage.color = new Color(1f, 0f, 0f, 0f);
            healthBar.transform.localScale = Vector3.one;
        }
    }
}
