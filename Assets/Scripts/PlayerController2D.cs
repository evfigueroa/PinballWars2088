using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerController2D : MonoBehaviour
{
    public float moveSpeed = 6f;
    public int maxHealth = 3;
    public float iFrames = 0.4f;
    public string pinballTag = "Pinball";
    public Text healthText;
    public GameObject gameOverPanel; // NEW

    Rigidbody2D rb;
    int health;
    float iTimer;
    Vector2 input;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.freezeRotation = true;
        health = maxHealth;
        UpdateHealthUI();
    }

    void Update()
    {
        var kb = Keyboard.current;
        if (kb != null)
        {
            float x = (kb.aKey.isPressed ? -1 : 0) + (kb.dKey.isPressed ? 1 : 0);
            float y = (kb.sKey.isPressed ? -1 : 0) + (kb.wKey.isPressed ? 1 : 0);
            input = new Vector2(x, y).normalized;
        }

        if (iTimer > 0) iTimer -= Time.deltaTime;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = input * moveSpeed;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag(pinballTag)) TakeHit();
    }

    void TakeHit()
    {
        if (iTimer > 0) return;

        health = Mathf.Max(0, health - 1);
        iTimer = iFrames;
        UpdateHealthUI();

        if (health <= 0)
        {
            GameOver();
        }
    }

    void UpdateHealthUI()
    {
        if (healthText) healthText.text = $"Health: {health}";
    }

    void GameOver()
    {
        if (gameOverPanel && !gameOverPanel.activeSelf)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;
        }

        // prevent repeated triggers
        health = 3; // reset so it doesn’t fire again
        UpdateHealthUI();
    }
    public void ResetHealth()
    {
        health = maxHealth;
        UpdateHealthUI();
    }
}

