using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    private static int enemyCount = 0;

    void OnEnable()
    {
        enemyCount++;
    }

    void OnDestroy()
    {
        enemyCount--;

        if (enemyCount <= 0)
        {
            GameManager.Instance.PlayerWon();
        }
    }
}
