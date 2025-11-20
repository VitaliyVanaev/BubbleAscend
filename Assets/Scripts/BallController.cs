using UnityEngine;
using UnityEngine.InputSystem;

public class BallController : MonoBehaviour, PlayerControls.ITouchActions
{
    [Header("Movement Settings")]
    public float moveSpeed = 20f; // чувствительность движения

    private PlayerControls controls;
    private Camera cam;

    private Vector2 lastTouchPos;
    private bool isDragging = false;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Touch.SetCallbacks(this);
        cam = Camera.main;
    }

    private void OnEnable()
    {
        controls.Touch.Enable();
    }

    private void OnDisable()
    {
        controls.Touch.Disable();
    }

    // Вызывается при нажатии или отпускании
    public void OnPress(InputAction.CallbackContext context)
    {
        if (context.performed) // палец/кнопка нажата
        {
            lastTouchPos = controls.Touch.Position.ReadValue<Vector2>();
            isDragging = true;
        }
        else if (context.canceled) // палец/кнопка отпущена
        {
            isDragging = false;
        }
    }

    // Вызывается при движении
    public void OnPosition(InputAction.CallbackContext context)
    {
        if (!isDragging) return; // движение только если удерживаем

        Vector2 currentTouchPos = context.ReadValue<Vector2>();
        Vector2 delta = currentTouchPos - lastTouchPos;
        lastTouchPos = currentTouchPos;

        Vector3 worldDelta = new Vector3(
            delta.x / Screen.width * moveSpeed,
            delta.y / Screen.height * moveSpeed,
            0f
        );

        transform.position += worldDelta;
    }

    public void OnHold(InputAction.CallbackContext context) { }
    public void OnDelta(InputAction.CallbackContext context) { }
}
