using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomFX : MonoBehaviour
{
    public float intervalMean;
    public float intervalVariance;
    public AudioClip audioClip;

    private AudioSource audioSource;
    private float nextTime;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        nextTime = GenerateNextTime();
    }

    private float GenerateNextTime()
    {
        return Time.time + intervalMean + Random.Range(-intervalVariance, intervalVariance);
    }

    private void Update()
    {
        if (Time.time > nextTime)
        {
            audioSource.PlayOneShot(audioClip);
            nextTime = GenerateNextTime();
        }
    }
}
