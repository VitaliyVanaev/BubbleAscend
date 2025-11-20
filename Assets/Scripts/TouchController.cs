using UnityEngine;
using UnityEngine.InputSystem;

public class TouchController : MonoBehaviour, PlayerControls.ITouchActions
{
    private PlayerControls controls;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Touch.SetCallbacks(this);
    }

    private void OnEnable()
    {
        controls.Touch.Enable();
    }

    private void OnDisable()
    {
        controls.Touch.Disable();
    }

    public void OnPosition(InputAction.CallbackContext context)
    {
        Vector2 pos = context.ReadValue<Vector2>();
        Debug.Log("Position: " + pos);
    }

    public void OnPress(InputAction.CallbackContext context)
    {
        if (context.performed)
            Debug.Log("Press!");
    }

    public void OnHold(InputAction.CallbackContext context)
    {
        if (context.performed)
            Debug.Log("Hold!");
    }

    public void OnDelta(InputAction.CallbackContext context)
    {
        Vector2 delta = context.ReadValue<Vector2>();
        Debug.Log("Delta: " + delta);
    }
}
