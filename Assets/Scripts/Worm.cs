using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Worm : MonoBehaviour
{
    public Transform anchor;
    public float radius;
    public float smoothTime;
    public float rotateRandomSpeed;
    public float speed;

    public float colliderDistance = 0.1f;
    public int colliderCount = 10;

    public GameObject colliderPrefab;

    private float targetRotation;

    private float rotation;
    private float rotationVelocity;

    private List<GameObject> colliders;

    private void Start()
    {
        targetRotation = Random.Range(-180, 180);

        colliders = new List<GameObject>();
        for (int i = 0; i < colliderCount; i++)
        {
            colliders.Add(Instantiate(colliderPrefab));
        }
    }

    private void Update()
    {
        rotation = Mathf.SmoothDampAngle(rotation, targetRotation, ref rotationVelocity, smoothTime);
        transform.rotation = Quaternion.Euler(0, 0, rotation);
        transform.position += transform.right * speed * Time.deltaTime;

        Vector2 anchorDirection = anchor.position - transform.position;

        if (anchorDirection.sqrMagnitude > radius * radius)
        {
            targetRotation = Mathf.Atan2(anchorDirection.y, anchorDirection.x) * Mathf.Rad2Deg;
        }
        else
        {
            targetRotation += Random.Range(-rotateRandomSpeed, rotateRandomSpeed) * Time.deltaTime;
        }

        if ((colliders[0].transform.position - transform.position).sqrMagnitude > colliderDistance * colliderDistance)
        {
            GameObject tailCollider = colliders[colliders.Count - 1];
            colliders.RemoveAt(colliders.Count - 1);

            tailCollider.transform.position = transform.position;
            tailCollider.transform.rotation = transform.rotation;
            colliders.Insert(0, tailCollider);
        }
    }

    private void OnDestroy()
    {
        foreach (GameObject collider in colliders)
        {
            Destroy(collider);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (anchor != null)
        {
            Handles.DrawWireDisc(anchor.position, Vector3.forward, radius);
        }
    }
}
