using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [Header("Panels")]
    public GameObject pauseMenu;
    public GameObject modeMenu;
    public GameObject gamePanel;

    [Header("Input")]
    public Key pauseKey = Key.Escape;

    [Header("Objects to Reset")]
    public Transform player;              // Jorge
    public Transform pinball;             // Pinball
    public PlayerController2D playerScript; // <-- add this reference
    public Vector3 playerStartPos;
    public Vector3 pinballStartPos;

    bool isPaused;

    void Start()
    {
        if (player) playerStartPos = player.position;
        if (pinball) pinballStartPos = pinball.position;
    }

    void Update()
    {
        var kb = Keyboard.current;
        if (kb == null) return;

        if (kb[pauseKey].wasPressedThisFrame)
        {
            if (isPaused) Resume();
            else Pause();
        }
    }

    public void Pause()
    {
        if (!pauseMenu) return;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        if (!pauseMenu) return;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void ChangeMode()
    {
        Time.timeScale = 1f;
        isPaused = false;

        // 🔄 Reset Jorge's health
        if (playerScript)
        {
            playerScript.ResetHealth();
        }

        // Reset player + pinball positions
        if (player)
        {
            player.position = playerStartPos;
            var rb = player.GetComponent<Rigidbody2D>();
            if (rb) rb.linearVelocity = Vector2.zero;
        }

        if (pinball)
        {
            pinball.position = pinballStartPos;
            var rb = pinball.GetComponent<Rigidbody2D>();
            if (rb)
            {
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }
        }

        // Switch panels
        if (gamePanel) gamePanel.SetActive(false);
        if (pauseMenu) pauseMenu.SetActive(false);
        if (modeMenu)  modeMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
        Debug.Log("Game quit.");
    }
}
