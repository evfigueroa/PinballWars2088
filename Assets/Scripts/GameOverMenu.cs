using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    public GameObject modeMenu;  // assign your ModeMenu panel
    public GameObject gamePanel; // assign your GamePanel
    public GameObject gameOverPanel; // assign GameOverPanel

    public Transform player;  // Jorge
    public Transform pinball; // Pinball
    Vector3 playerStart, pinballStart;

    void Start()
    {
        if (player) playerStart = player.position;
        if (pinball) pinballStart = pinball.position;
    }

    public void PlayAgain()
    {
        // Unpause
        Time.timeScale = 1f;

        // Reset player & ball
        if (player)
        {
            player.position = playerStart;
            var rb = player.GetComponent<Rigidbody2D>();
            if (rb) rb.linearVelocity = Vector2.zero;
        }

        if (pinball)
        {
            pinball.position = pinballStart;
            var rb = pinball.GetComponent<Rigidbody2D>();
            if (rb)
            {
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }
        }

        // Hide Game Over panel, show Mode Menu
        if (gameOverPanel) gameOverPanel.SetActive(false);
        if (gamePanel) gamePanel.SetActive(false);
        if (modeMenu) modeMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
        Debug.Log("Quit Game.");
    }
}