using UnityEngine;

public class SoapBubble : MonoBehaviour
{
    public float moveSpeed = 2f; // скорость падения пузыря

    private void Update()
    {
        // Пузырь опускается вниз
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Если коснулся игрок
        if (collision.CompareTag("Player"))
        {
            // Лопаем пузырь — просто удаляем объект
            Destroy(gameObject);
        }
    }
}
