using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Transform flower;
    public Text scoreText;
    public int Altitude => (int)Mathf.Round(flower.position.y);

    void Update()
    {
        scoreText.text = "Flower height: " + Altitude + "ft";
    }
}
