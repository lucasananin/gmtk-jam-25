using UnityEngine;
using System.Collections;

public class King_Midevil : MonoBehaviour
{
    [Header("Spawning Settings")]
    public GameObject spawnPrefab;
    public Vector2Int spawnCountRange = new Vector2Int(3, 6);
    public Vector2 spawnDelayRange = new Vector2(1f, 3f);
    public float spawnRadius = 5f;

    [Header("References")]
    public Transform player;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        if (player == null) yield break;

        int spawnCount = Random.Range(spawnCountRange.x, spawnCountRange.y + 1);

        for (int i = 0; i < spawnCount; i++)
        {
            Vector2 offset = Random.insideUnitCircle.normalized * Random.Range(1f, spawnRadius);
            Vector2 spawnPosition = (Vector2)player.position + offset;

            Instantiate(spawnPrefab, spawnPosition, Quaternion.identity);

            float delay = Random.Range(spawnDelayRange.x, spawnDelayRange.y);
            yield return new WaitForSeconds(delay);
        }
    }
}
