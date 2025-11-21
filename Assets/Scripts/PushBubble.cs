using UnityEngine;
using System.Collections;

public class PushBubble : MonoBehaviour
{
    public float moveSpeed = 2f;      // скорость падения пузыря
    public float pushDistance = 2f; // как сильно толкает
    public float pushTime = 0.4f;     // за сколько времени толкает

    private void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(PushDown(collision.transform));
        }
    }

    private IEnumerator PushDown(Transform player)
    {
        Vector3 startPos = player.position;
        Vector3 endPos = startPos + Vector3.down * pushDistance;
        float elapsed = 0f;

        while (elapsed < pushTime)
        {
            player.position = Vector3.Lerp(startPos, endPos, elapsed / pushTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        player.position = endPos;
    }
}
