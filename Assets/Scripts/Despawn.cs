using UnityEditor;
using UnityEngine;

public class Despawn : MonoBehaviour
{
    public float despawnLine;

    private float WorldDespawnLine => Camera.main.transform.position.y + despawnLine;

    private void Update()
    {
        if (transform.position.y < WorldDespawnLine)
        {
            Destroy(gameObject.transform.root.gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Handles.DrawLine(new Vector2(transform.position.x - 20, WorldDespawnLine), new Vector2(transform.position.x + 20, WorldDespawnLine));
    }
}
