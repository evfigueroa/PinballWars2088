using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    [Header("UI")]
    public Text timeLabel;                 // assign your "Time - 0:00" Text
    public string prefix = "Time - ";

    [Header("Behavior")]
    public bool runOnStart = true;         // start automatically?
    public bool useUnscaledTime = false;   // ignore Time.timeScale (e.g., during pause)?
    public bool showHundredths = false;    // mm:ss or mm:ss.hh

    float elapsed;
    bool running;

    void Start()
    {
        running = runOnStart;
        UpdateLabel();
    }

    void Update()
    {
        if (!running) return;
        elapsed += (useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime);
        UpdateLabel();
    }

    void UpdateLabel()
    {
        int minutes = (int)(elapsed / 60f);
        int seconds = (int)(elapsed % 60f);

        if (showHundredths)
        {
            int hundredths = (int)((elapsed - Mathf.Floor(elapsed)) * 100f);
            timeLabel.text = $"{prefix}{minutes}:{seconds:00}.{hundredths:00}";
        }
        else
        {
            timeLabel.text = $"{prefix}{minutes}:{seconds:00}";
        }
    }

    // Public controls you can call from buttons/other scripts:
    public void StartTimer()  { running = true; }
    public void StopTimer()   { running = false; }
    public void ResetTimer()  { elapsed = 0f; UpdateLabel(); }
    public void SetTime(float seconds) { elapsed = Mathf.Max(0f, seconds); UpdateLabel(); }
    public float GetTime() => elapsed;
}