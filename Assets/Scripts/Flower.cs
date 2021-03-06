using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flower : MonoBehaviour
{
    public Text finalScoreText;
    public Text pausedText;
    public GameObject pausePanel;
    public GameObject gameOverPanel;
    public Score score;
    public Button playAgain;
    public Button mainMenu;
    public Button continueGame;
    public Button quitToMenu;

    public AudioClip wateringCan;
    public AudioSource audioSource;

    public AudioClip damage;

    public Transform head;
    public Transform seed;

    public GameObject petalPrefab;
    public int numPetals = 8;

    public float losePetalRate = 0.5f;
    public float pickupAmount = 2.0f;
    public float damageAmount = 1.0f;

    public float maxRadius = 2;
    public float minRadius = 1;

    private float speed;
    public float minSpeed = 1f;
    public float maxSpeed = 2f;

    public float percentageLife;

    public Transform sun;

    public float Altitude => transform.position.y;
    public bool AboveGround => Altitude >= 0;

    public float iframeTime;
    private float vulnerableTime;

    private List<GameObject> petals = new List<GameObject>();

    public List<Renderer> sprites;

    private List<Renderer> SpritesToFlash
    {
        get
        {
            List<Renderer> sprites = new List<Renderer>();
            sprites.AddRange(sprites);
            foreach (GameObject petal in petals)
            {
                sprites.Add(petal.GetComponentInChildren<Renderer>());
            }
            return sprites;
        }
    }

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
            value = Mathf.Clamp(value, -1, numPetals);
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
        Time.timeScale = 1;
        finalScoreText.gameObject.SetActive(false);
        playAgain.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(false);

        gameOverPanel.gameObject.SetActive(false);
        pausePanel.gameObject.SetActive(false);
        pausedText.gameObject.SetActive(false);
        continueGame.gameObject.SetActive(false);
        quitToMenu.gameObject.SetActive(false);

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
        percentageLife = life / numPetals;
        speed = Mathf.Lerp(minSpeed, maxSpeed, percentageLife);

        Vector2 sunDirection = sun.position - transform.position;
        float radians = Mathf.Atan2(sunDirection.y, sunDirection.x);

        float targetAngle = radians * Mathf.Rad2Deg - 90.0f;
        angle = Mathf.SmoothDamp(angle, targetAngle, ref angleVelocity, angleSmoothTime);

        transform.rotation = Quaternion.Euler(0, 0, angle);
        transform.position += transform.up * speed * Time.deltaTime;

        head.gameObject.SetActive(AboveGround);
        seed.gameObject.SetActive(!AboveGround);

        if (life < 0)
        {
            Die();
            return;
        }
        Life -= losePetalRate * Time.deltaTime;

        if (Input.GetKeyDown("escape"))
        {
            PauseGame();
        }

        if (Time.time > disableRainTime)
        {
            Raining = false;
        }

        UpdateFlash();
    }

    private void UpdateFlash()
    {
        bool flashOn;
        if (Time.time < vulnerableTime)
        {
            // Flash
            flashOn = Mathf.Repeat(Time.time, 0.1f) < 0.05f;
        }
        else
        {
            // Don't flash
            flashOn = true;
        }

        foreach (Renderer renderer in SpritesToFlash)
        {
            renderer.enabled = flashOn;
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.gameObject.SetActive(true);
        pausedText.gameObject.SetActive(true);
        continueGame.gameObject.SetActive(true);
        quitToMenu.gameObject.SetActive(true);
    }

    private void Die()
    {
        finalScoreText.text = "Game Over!\nYour flower reached a height of:\n" + score.Altitude + "ft";
        gameOverPanel.gameObject.SetActive(true);
        finalScoreText.gameObject.SetActive(true);
        playAgain.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(true);
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
            audioSource.PlayOneShot(wateringCan);
            Life += pickupAmount;
            Destroy(collision.gameObject);

            Raining = true;
            disableRainTime = Time.time + rainTime;
        }

        if (Time.time > vulnerableTime && (collision.CompareTag("Bee") || collision.CompareTag("Bird") || collision.CompareTag("Worm")))
        {
            audioSource.PlayOneShot(damage);
            Life -= damageAmount;
            vulnerableTime = Time.time + iframeTime;
        }
    }

    public AnimateRain[] rains;
    private float disableRainTime;
    public float rainTime = 2;

    private bool Raining
    {
        set
        {
            foreach (AnimateRain rain in rains)
            {
                rain.showRain = value;
            }
        }
    }
}
