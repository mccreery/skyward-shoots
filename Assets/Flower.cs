using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    public GameObject petalPrefab;
    public int numPetals = 8;

    public float maxRadius = 2;
    public float minRadius = 1;

    private List<GameObject> petals = new List<GameObject>();

    [Min(0)]
    [SerializeField]
    private int life;

    public int Life
    {
        get
        {
            return life;
        }
        set
        {
            life = value;
            for (int i = 0; i < petals.Count; i++)
            {
                petals[i].SetActive(life > i);
            }
        }
    }

    private void OnValidate()
    {
        Life = life;
    }

    private void Start()
    {
        life = numPetals;
        for (int i = 0; i < numPetals; i++)
        {
            petals.Add(GeneratePetal(i));
        }
    }

    private GameObject GeneratePetal(int i)
    {
        float degrees = i * 360.0f / numPetals;
        float radians = degrees * Mathf.Deg2Rad;

        Vector2 petalPosition = new Vector2(maxRadius * Mathf.Cos(radians), minRadius * Mathf.Sin(radians));
        GameObject petal = Instantiate(petalPrefab, petalPosition, Quaternion.Euler(0, 0, degrees), transform);

        return petal;
    }
}
