using UnityEngine;
using UnityEngine.InputSystem;

public class BallController : MonoBehaviour, PlayerControls.ITouchActions
{
    [Header("Movement Settings")]
    public float moveSpeed = 20f; // чувствительность движени€
    public float sideMargin = 0.3f; // сколько шарик может уходить за экран

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

    // ---------------------------
    // INPUT SYSTEM
    // ---------------------------

    // Ќажатие пальца
    public void OnPress(InputAction.CallbackContext context)
    {
        if (context.performed) // палец прижат
        {
            lastTouchPos = controls.Touch.Position.ReadValue<Vector2>();
            isDragging = true;
        }
        else if (context.canceled) // палец отпущен
        {
            isDragging = false;
        }
    }

    // ƒвижение пальца
    public void OnPosition(InputAction.CallbackContext context)
    {
        if (!isDragging) return;

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

    // ---------------------------
    // GAME LOGIC
    // ---------------------------

    private void Update()
    {
        CheckBottomDeath();
    }

    private void LateUpdate()
    {
        ClampToSideBorders();
    }

    // ---------------------------------------
    // 1. —мерть, если шарик ушЄл ниже 19%
    // ---------------------------------------
    private void CheckBottomDeath()
    {
        // нижн€€ граница камеры в мировых координатах
        float bottomY = cam.ViewportToWorldPoint(Vector3.zero).y;

        // умираем, если шарик ушЄл ниже нижней границы
        if (transform.position.y < bottomY - 0.5f) // смещение 0.5f, чтобы полностью исчезнуть
        {
            Die();
        }
    }

    // ---------------------------------------
    // 2. ќграничение границ экрана
    //    Х можно немного заходить за левый/правый/верхний край
    //    Х смерть Ч только снизу
    // ---------------------------------------
    private void ClampToSideBorders()
    {
        Vector3 pos = transform.position;

        Vector3 left = cam.ScreenToWorldPoint(new Vector3(0, 0));
        Vector3 right = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0));
        Vector3 top = cam.ScreenToWorldPoint(new Vector3(0, Screen.height));

        // ограничение слева и справа (частично можно выходить)
        pos.x = Mathf.Clamp(pos.x, left.x - sideMargin, right.x + sideMargin);

        // сверху можно выходить немного
        pos.y = Mathf.Clamp(pos.y, left.y - 5f, top.y + sideMargin);

        transform.position = pos;
    }

    // ---------------------------------------
    // 3. —мерть (вызываетс€ пузыр€ми)
    // ---------------------------------------
    public void Die()
    {
        Debug.Log("PLAYER DIED");
        // «десь можно добавить:
        // - анимацию лопани€
        // - рестарт сцены
        // - GameOver экран

        // временно Ч просто выключаем объект
        gameObject.SetActive(false);
    }
}
