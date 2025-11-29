using UnityEngine;
using UnityEngine.InputSystem;

public class FlipperWithOffset : MonoBehaviour
{
    [Header("Angles")]
    public float restAngle = -30f;
    public float activeAngle = 30f;

    [Header("Y Positions")]
    public float restY = -420f;
    public float activeY = -335f;

    [Header("Motion")]
    public float rotateSpeedDegPerSec = 600f;
    public float ySnapSpeed = 8f;

    [Header("Input (Keyboard for Multiplayer)")]
    public bool useLeftArrow = true;

    [Header("Optional New Input System Action")]
    public InputActionReference pressAction;

    float _targetAngle;
    float _targetY;
    bool aiActive = false;

    void OnEnable()
    {
        if (pressAction != null)
            pressAction.action.Enable();
    }

    void OnDisable()
    {
        if (pressAction != null)
            pressAction.action.Disable();
    }

    bool IsPressed()
    {
        if (pressAction != null)
            return pressAction.action.IsPressed();

        var kb = Keyboard.current;
        if (kb == null) return false;

        return useLeftArrow ? kb.leftArrowKey.isPressed
                            : kb.rightArrowKey.isPressed;
    }

    void Update()
    {
        bool pressed = false;

        var mode = GameManager.Instance.currentMode;

        if (mode == GameMode.Multiplayer)
        {
            pressed = IsPressed();
        }
        else if (mode == GameMode.SinglePlayer)
        {
            pressed = aiActive;   // AI replaces pressed input
        }

        _targetAngle = pressed ? activeAngle : restAngle;
        _targetY = pressed ? activeY : restY;

        // apply rotation
        float currentZ = transform.localEulerAngles.z;
        float nextZ = Mathf.MoveTowardsAngle(
            currentZ,
            _targetAngle,
            rotateSpeedDegPerSec * Time.deltaTime
        );
        transform.localEulerAngles = new Vector3(0f, 0f, nextZ);

        // apply Y offset
        var lp = transform.localPosition;
        float yStep = Mathf.Abs(activeY - restY) * ySnapSpeed * Time.deltaTime;
        lp.y = Mathf.MoveTowards(lp.y, _targetY, yStep);
        transform.localPosition = lp;
    }

    // -------- AI CONTROL METHODS --------

    public void ForceFlip()
    {
        aiActive = true;
    }

    public void ReleaseFlipper()
    {
        aiActive = false;
    }

}
