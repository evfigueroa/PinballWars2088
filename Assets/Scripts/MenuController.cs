using UnityEngine;

public class MenuController : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject startMenu; // your "Start Menu" panel
    [SerializeField] private GameObject modeMenu;  // your "ModeMenu" panel
    [SerializeField] private GameObject gamePanel;  // your "ModeMenu" panel
    [SerializeField] private TimerScript timer;

    void Awake()
    {
        ShowStartMenu();
    }

    public void ShowStartMenu()
    {
        if (startMenu) startMenu.SetActive(true);
        if (modeMenu)  modeMenu.SetActive(false);
        if (gamePanel) gamePanel.SetActive(false);
    }

    public void ShowModeMenu()
    {
        if (startMenu) startMenu.SetActive(false);
        if (gamePanel) gamePanel.SetActive(false);
        if (modeMenu)  modeMenu.SetActive(true);
    }

    public void ShowGamePanel()
    {
        if (modeMenu) modeMenu.SetActive(false);
        if (startMenu) startMenu.SetActive(false);
        if (gamePanel) gamePanel.SetActive(true);
        
        timer.ResetTimer();
        timer.StartTimer();
    }
}