using UnityEngine;

public class Spaceship : MonoBehaviour
{
    public float acceleration;
    public float maxSpeed;
    public float rotationScale;

    private Transform flower;
    private Vector2 velocity;

    private void Awake()
    {
        flower = FindObjectOfType<Flower>().transform;
    }

    private void Update()
    {
        Vector2 distanceToFlower = flower.position - transform.position;
        velocity = Vector2.MoveTowards(velocity, distanceToFlower.normalized * maxSpeed, acceleration * Time.deltaTime);
        velocity.y *= 0.3f;
        transform.position += (Vector3)velocity * Time.deltaTime;

        transform.rotation = Quaternion.Euler(0, 0, -velocity.x * rotationScale);
    }
}
