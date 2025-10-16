using UnityEngine;
using UnityEngine.UI; // for Image

[RequireComponent(typeof(Collider2D))]
public class BumperSpriteSwitch : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite idleSprite;   // normal
    public Sprite hitSprite;    // shown briefly on hit

    [Header("Timing")]
    public float revertDelay = 0.15f;
    public string pinballTag = "Pinball";

    Image uiImage;              // UI path
    SpriteRenderer sr;          // SpriteRenderer path (optional)
    bool hasUI, hasSR;

    void Awake()
    {
        // Grab whichever renderer this bumper has
        uiImage = GetComponent<Image>();
        sr      = GetComponent<SpriteRenderer>();
        hasUI   = uiImage != null;
        hasSR   = sr != null;

        if (!hasUI && !hasSR)
            Debug.LogWarning($"[{name}] No Image or SpriteRenderer found—assign one if you want visual swapping.");

        // Default idle sprite = current visual if not set
        if (!idleSprite)
        {
            if (hasUI) idleSprite = uiImage.sprite;
            else if (hasSR) idleSprite = sr.sprite;
        }
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.collider.CompareTag(pinballTag)) Flash();
    }

    void OnTriggerEnter2D(Collider2D o)
    {
        if (o.CompareTag(pinballTag)) Flash();
    }

    void Flash()
    {
        SetSprite(hitSprite);
        if (revertDelay > 0f) Invoke(nameof(Revert), revertDelay);
    }

    void Revert() => SetSprite(idleSprite);

    void SetSprite(Sprite s)
    {
        if (!s) return;
        if (hasUI) uiImage.sprite = s;
        if (hasSR) sr.sprite = s;
    }
}