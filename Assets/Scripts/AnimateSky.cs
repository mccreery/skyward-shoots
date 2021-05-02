using UnityEngine;

public class AnimateSky : MonoBehaviour
{
    public float cameraStartPoint;
    public float backgroundStartPoint;

    public float cameraEndPoint;
    public float backgroundEndPoint;

    private void Update()
    {
        float t = Mathf.InverseLerp(cameraStartPoint, cameraEndPoint, Camera.main.transform.position.y);

        Vector3 position = transform.localPosition;
        position.y = Mathf.SmoothStep(backgroundStartPoint, backgroundEndPoint, t);
        transform.localPosition = position;
    }
}
