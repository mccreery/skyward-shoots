using UnityEngine;

public class Sun : MonoBehaviour
{
    private float angle;
    private float angleVelocity;

    public float angleSmoothTime = 0.1f;

    public float angleLimit = 40.0f;

    private void Update()
    {
        Vector2 mouseOffset = Camera.main.ScreenPointToRay(Input.mousePosition).origin - transform.position;

        float targetAngle = Mathf.Atan2(mouseOffset.y, mouseOffset.x) * Mathf.Rad2Deg;
        targetAngle = Mathf.Clamp(targetAngle, 90 - angleLimit, 90 + angleLimit);

        angle = Mathf.SmoothDamp(angle, targetAngle, ref angleVelocity, angleSmoothTime);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
