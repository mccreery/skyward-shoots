using System;
using UnityEditor;
using UnityEngine;

using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public Transform spawnAnchor;
    public Rect queueArea;
    public float spawnLine;

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
            EnemySet enemySet = GetEnemySet(nextSpawnPosition.y);
            Instantiate(enemySet.RandomChoice(), nextSpawnPosition, Quaternion.identity);
            nextSpawnPosition = GenerateSpawnPosition();
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (spawnAnchor != null)
        {
            Rect worldQueueArea = WorldQueueArea;
            Handles.DrawWireCube(WorldQueueArea.center, WorldQueueArea.size);

            float worldSpawnLine = spawnAnchor.position.y + spawnLine;
            Handles.DrawLine(new Vector2(spawnAnchor.position.x - 20, worldSpawnLine), new Vector2(spawnAnchor.position.x + 20, worldSpawnLine));
        }
    }
#endif

    [Serializable]
    public struct Stop
    {
        public float height;
        public EnemySet enemySet;
    }

    public Stop[] stops;

    private EnemySet GetEnemySet(float height)
    {
        for (int i = stops.Length - 1; i >= 0; i--)
        {
            if (height > stops[i].height)
            {
                return stops[i].enemySet;
            }
        }
        return stops[0].enemySet;
    }
}
