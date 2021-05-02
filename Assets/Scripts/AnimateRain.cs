using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimateRain : MonoBehaviour
{
    public float angle;
    public float speed;
    public float fadeSpeed;

    public Color color;

    public bool showRain;

    private Vector2 OffsetStep => new Vector2(Mathf.Cos(angle) * speed, Mathf.Sin(angle) * speed);

    private Material material;

    private void Awake()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        material = new Material(spriteRenderer.material);
        spriteRenderer.material = material;

        Color fadedColor = color;
        fadedColor.a = 0;
        material.color = fadedColor;
    }

    private void Update()
    {
        material.mainTextureOffset += OffsetStep * Time.deltaTime;

        float targetOpacity = showRain ? color.a : 0.0f;

        Color fadedColor = material.color;
        fadedColor.a = Mathf.MoveTowards(fadedColor.a, targetOpacity, fadeSpeed * Time.deltaTime);
        material.color = fadedColor;
    }
}
