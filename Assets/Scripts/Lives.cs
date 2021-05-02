using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Lives : MonoBehaviour
{
    public Flower flower;

    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        text.text = "Life: " + Mathf.CeilToInt(flower.Life);
    }
}
