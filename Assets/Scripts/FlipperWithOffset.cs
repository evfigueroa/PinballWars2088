using UnityEngine;
using UnityEngine.InputSystem; // NEW input system

public class FlipperWithOffset : MonoBehaviour
{
    [Header("Angles")]
    public float restAngle  = -30f;   // e.g., left flipper at rest
    public float activeAngle = 30f;   // left flipper pressed

    [Header("Y Positions (UI/World-Space compensation)")]
    public float restY   = -420f;
    public float activeY = -335f;

    [Header("Motion")]
    public float rotateSpeedDegPerSec = 600f; // rotation speed
    public float ySnapSpeed = 8f;            // how quickly we move to target Y

    [Header("Input (Keyboard)")]
    public bool useLeftArrow = true;   // left flipper → LeftArrow, right flipper → RightArrow

    // Optional: if you prefer an InputAction in your InputActions asset, assign it here:
    [Header("Optional: InputAction")]
    public InputActionReference pressAction; // expects a Button action, performed while pressed

    float _targetAngle;
    float _targetY;

    void OnEnable()
    {
        if (pressAction != null)
        {
            pressAction.action.Enable();
        }
    }

    void OnDisable()
    {
        if (pressAction != null)
        {
            pressAction.action.Disable();
        }
    }

    bool IsPressed()
    {
        // If an InputAction is provided, use it first
        if (pressAction != null)
            return pressAction.action.IsPressed();

        // Fallback to Keyboard (works in new Input System)
        var kb = Keyboard.current;
        if (kb == null) return false;

        return useLeftArrow ? kb.leftArrowKey.isPressed
                            : kb.rightArrowKey.isPressed;
    }

    void Update()
    {
        bool pressed = IsPressed();

        _targetAngle = pressed ? activeAngle : restAngle;
        _targetY     = pressed ? activeY     : restY;

        // Rotate smoothly toward target Z
        float currentZ = transform.localEulerAngles.z;
        float nextZ = Mathf.MoveTowardsAngle(currentZ, _targetAngle, rotateSpeedDegPerSec * Time.deltaTime);
        transform.localEulerAngles = new Vector3(0f, 0f, nextZ);

        // Move Y to compensate for visual pivot offset
        var lp = transform.localPosition;
        float yStep = Mathf.Abs(activeY - restY) * ySnapSpeed * Time.deltaTime;
        lp.y = Mathf.MoveTowards(lp.y, _targetY, yStep);
        transform.localPosition = lp;
    }
}
