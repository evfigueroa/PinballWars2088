using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    public GameObject modeMenu;  // assign your ModeMenu panel
    public GameObject gamePanel; // assign your GamePanel
    public GameObject gameOverPanel; // assign GameOverPanel
    public MultiplayerManager multiplayerManager;

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
        Time.timeScale = 1f;

        // Reset positions
        if (player)
        {
            player.position = playerStart;
            var rb = player.GetComponent<Rigidbody2D>();
            if (rb) rb.linearVelocity = Vector2.zero;
        }

        // Reset health
        var pc = player.GetComponent<PlayerController2D>();
        if (pc != null)
        {
            pc.ResetHealth();
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

        // NEW — clean multiplayer leftovers
        if (multiplayerManager != null && multiplayerManager.isMultiplayer)
        {
            // Reset multiplayer internal state
            multiplayerManager.ReturnToModeMenu();
        }

        // UI switching
        // If coming from single player, ensure the result text resets
        var resultText = gameOverPanel.GetComponentInChildren<UnityEngine.UI.Text>();
        if (resultText != null)
        {
            resultText.text = "Game Over";
        }
        if (gameOverPanel) gameOverPanel.SetActive(false);
        if (gamePanel) gamePanel.SetActive(false);
        if (modeMenu) modeMenu.SetActive(true);
    }
    public void ResetResultText()
    {
        var resultText = gameOverPanel.GetComponentInChildren<UnityEngine.UI.Text>();
        if (resultText != null)
            resultText.text = "Game Over";
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
        Debug.Log("Quit Game.");
    }
}