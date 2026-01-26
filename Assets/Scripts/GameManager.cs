using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game Over UI")]
    public Canvas gameOverCanvas;
    public TextMeshProUGUI resultText;

    bool gameEnded = false;

    void Awake()
    {
        Instance = this;
        gameOverCanvas.gameObject.SetActive(false);
    }

    public void PlayerLost()
    {
        if (gameEnded) return;
        gameEnded = true;
        Debug.Log("Canvas enabled: " + gameOverCanvas.enabled);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameOverCanvas.gameObject.SetActive(true);
        resultText.text = "YOU LOSE";
    }

    public void PlayerWon()
    {
        if (gameEnded) return;
        gameEnded = true;

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameOverCanvas.gameObject.SetActive(true);
        resultText.text = "YOU WIN";
    }

    public void PlayAgain()
    {
        Debug.Log("PLAY AGAIN CLICKED");
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
