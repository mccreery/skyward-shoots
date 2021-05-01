using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    public Transform head;
    public Transform seed;

    public GameObject petalPrefab;
    public int numPetals = 8;

    public float losePetalRate = 0.5f;
    public float pickupAmount = 2.0f;

    public float maxRadius = 2;
    public float minRadius = 1;

    public float speed = 1.0f;
    public Transform sun;

    public float Altitude => transform.position.y;
    public bool AboveGround => Altitude >= 0;

    private List<GameObject> petals = new List<GameObject>();

    [Min(0)]
    [SerializeField]
    private float life;

    public float Life
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
                petals[i].SetActive(life >= i);
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

    private void Update()
    {
        Quaternion rotation = Quaternion.Euler(0, 0, sun.rotation.eulerAngles.z - 90);
        head.rotation = rotation;
        seed.rotation = rotation;

        transform.position += head.up * speed * Time.deltaTime;
        head.gameObject.SetActive(AboveGround);
        seed.gameObject.SetActive(!AboveGround);

        if (AboveGround)
        {
            Life -= losePetalRate * Time.deltaTime;
        }
    }

    private GameObject GeneratePetal(int i)
    {
        float degrees = i * 360.0f / numPetals;
        float radians = degrees * Mathf.Deg2Rad;

        Vector2 petalPosition = new Vector2(maxRadius * Mathf.Cos(radians), minRadius * Mathf.Sin(radians));
        GameObject petal = Instantiate(petalPrefab, head);
        petal.transform.localPosition = petalPosition;
        petal.transform.localRotation = Quaternion.Euler(0, 0, degrees);

        return petal;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("WateringCan"))
        {
            Life += pickupAmount;
            Destroy(collision.gameObject);
        }
    }
}
