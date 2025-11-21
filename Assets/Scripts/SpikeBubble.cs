using UnityEngine;

public class SpikeBubble : MonoBehaviour
{
    public float moveSpeed = 2f; // скорость движения вниз

    private void Update()
    {
        // Пузырь движется вниз
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Проверяем, что столкнулся игрок
        if (collision.collider.CompareTag("Player"))
        {
            // вызываем метод смерти
            BallController player = collision.collider.GetComponent<BallController>();

            if (player != null)
            {
                player.Die(); // функция смерти (её нужно иметь в BallController)
            }
        }
    }
}
