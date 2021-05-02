using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flower : MonoBehaviour
{
    public Text finalScoreText;
    public Score score;

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
        finalScoreText.gameObject.SetActive(false);
        life = numPetals;
        for (int i = 0; i < numPetals; i++)
        {
            petals.Add(GeneratePetal(i));
        }
    }

    private float angle;
    private float angleVelocity;
    public float angleSmoothTime = 0.5f;

    private void Update()
    {
        Vector2 sunDirection = sun.position - transform.position;
        float radians = Mathf.Atan2(sunDirection.y, sunDirection.x);

        float targetAngle = radians * Mathf.Rad2Deg - 90.0f;
        angle = Mathf.SmoothDamp(angle, targetAngle, ref angleVelocity, angleSmoothTime);

        transform.rotation = Quaternion.Euler(0, 0, angle);
        transform.position += transform.up * speed * Time.deltaTime;

        head.gameObject.SetActive(AboveGround);
        seed.gameObject.SetActive(!AboveGround);

        if (AboveGround)
        {
            if (life < 0)
            {
                Die();
                return;
            }
            Life -= losePetalRate * Time.deltaTime;
        }
    }

    private void Die()
    {
        finalScoreText.text = "Game Over!\nYour flower reached a height of:\n" + score.Altitude + "ft";
        finalScoreText.gameObject.SetActive(true);
        Time.timeScale = 0;
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
