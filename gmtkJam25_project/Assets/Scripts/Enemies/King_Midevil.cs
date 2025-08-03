using UnityEngine;
using System.Collections;

public class King_Midevil : MonoBehaviour
{
    [Header("Spawning Settings")]
    public GameObject spawnPrefab;
    public Vector2Int spawnCountRange = new Vector2Int(5, 10);    
    public Vector2 spawnDelayRange = new Vector2(0.5f, 1.5f);   
    public float spawnRadius = 6f;                             
    public Vector2 waveDelayRange = new Vector2(3f, 5f);            

    [Header("References")]
    public Transform player;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
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

            float waveDelay = Random.Range(waveDelayRange.x, waveDelayRange.y);
            yield return new WaitForSeconds(waveDelay);
        }
    }
}
