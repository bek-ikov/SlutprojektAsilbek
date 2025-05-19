using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        waveText.text = $"Reached Wave: {GameData.finalWave}";
        scoreText.text = $"Final Score: {GameData.finalScore}";
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }
}
