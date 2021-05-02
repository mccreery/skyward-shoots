using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Bird : MonoBehaviour
{
    public Transform anchor;
    public float sideDistance;
    public float speed;

    private bool movingRight;

    private void Update()
    {
        transform.position += (movingRight ? Vector3.right : Vector3.left) * speed * Time.deltaTime;

        if (movingRight ? transform.position.x > anchor.position.x + sideDistance : transform.position.x < anchor.position.x - sideDistance)
        {
            movingRight = !movingRight;
        }

        GetComponent<SpriteRenderer>().flipX = !movingRight;
    }
}
