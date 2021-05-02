using UnityEditor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform spawnAnchor;
    public Rect queueArea;
    public float spawnLine;

    public EnemySet enemySet;
    private Vector2 nextSpawnPosition;

    private Rect WorldQueueArea
    {
        get
        {
            Rect worldQueueArea = queueArea;
            worldQueueArea.position += (Vector2)spawnAnchor.position;
            return worldQueueArea;
        }
    }

    private void Start()
    {
        nextSpawnPosition = GenerateSpawnPosition();
    }

    private Vector2 GenerateSpawnPosition()
    {
        Rect worldQueueArea = WorldQueueArea;

        return new Vector2(
            Random.Range(worldQueueArea.xMin, worldQueueArea.xMax),
            Random.Range(worldQueueArea.yMin, worldQueueArea.yMax));
    }

    private void Update()
    {
        float worldSpawnLine = spawnAnchor.position.y + spawnLine;

        if (nextSpawnPosition.y < worldSpawnLine)
        {
            Instantiate(enemySet.prefabs[Random.Range(0, enemySet.prefabs.Length)], nextSpawnPosition, Quaternion.identity);
            nextSpawnPosition = GenerateSpawnPosition();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Rect worldQueueArea = WorldQueueArea;
        Handles.DrawWireCube(WorldQueueArea.center, WorldQueueArea.size);

        float worldSpawnLine = spawnAnchor.position.y + spawnLine;
        Handles.DrawLine(new Vector2(spawnAnchor.position.x - 20, worldSpawnLine), new Vector2(spawnAnchor.position.x + 20, worldSpawnLine));
    }
}
