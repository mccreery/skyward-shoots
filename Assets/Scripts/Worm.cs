using UnityEditor;
using UnityEngine;

public class Worm : MonoBehaviour
{
    public Transform anchor;
    public float radius;
    public float smoothTime;
    public float rotateRandomSpeed;
    public float speed;

    private float targetRotation;

    private float rotation;
    private float rotationVelocity;

    private void Start()
    {
        targetRotation = Random.Range(-180, 180);
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
    }

    private void OnDrawGizmosSelected()
    {
        Handles.DrawWireDisc(anchor.position, Vector3.forward, radius);
    }
}
