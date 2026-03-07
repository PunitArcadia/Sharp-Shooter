using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    private int totalEnemies;
    private int killedEnemies;

    private void Start()
    {
        totalEnemies = FindObjectsByType<EnemyHealth>(FindObjectsSortMode.None).Length;
    }

    private void OnEnable()
    {
        EnemyEvents.OnEnemyKilled += HandleEnemyKilled;
    }

    private void OnDisable()
    {
        EnemyEvents.OnEnemyKilled -= HandleEnemyKilled;
    }

    private void HandleEnemyKilled()
    {
        killedEnemies++;

        if (killedEnemies >= totalEnemies)
        {
            GameManager.Instance.PlayerWon();
        }
    }
}
