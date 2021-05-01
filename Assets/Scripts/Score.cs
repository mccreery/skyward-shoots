using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Transform flower;
    public Text scoreText;

    void Update()
    {
        scoreText.text = "Flower height: " + Mathf.Round(flower.position.y) + "ft";
    }
}
