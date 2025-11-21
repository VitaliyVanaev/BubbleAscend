using UnityEngine;

public class PushBubble : MonoBehaviour
{
    public float moveSpeed = 2f;      // скорость падения пузыря
    public float pushForce = 6f;      // сила отталкивания

    private void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                // Сбрасываем линейную скорость (новый API)
                rb.linearVelocity = Vector2.zero;

                // Добавляем импульс вниз
                rb.AddForce(Vector2.down * pushForce, ForceMode2D.Impulse);
            }
        }
    }
}
