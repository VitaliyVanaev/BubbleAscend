using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    [Header("Bubble Prefabs")]
    public GameObject pushBubblePrefab;
    public GameObject soapBubblePrefab;
    public GameObject spikeBubblePrefab;

    [Header("Spawn Settings")]
    public float spawnInterval = 1.5f;
    public float spawnXMargin = 0.5f;
    public float spawnY = 6f;

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
        InvokeRepeating(nameof(SpawnBubble), 0f, spawnInterval);
    }

    private void SpawnBubble()
    {
        GameObject[] bubbles = { pushBubblePrefab, soapBubblePrefab, spikeBubblePrefab };
        GameObject bubbleToSpawn = bubbles[Random.Range(0, bubbles.Length)];

        Vector3 left = cam.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 right = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
        float randomX = Random.Range(left.x + spawnXMargin, right.x - spawnXMargin);

        Instantiate(bubbleToSpawn, new Vector3(randomX, spawnY, 0f), Quaternion.identity);
    }
}
