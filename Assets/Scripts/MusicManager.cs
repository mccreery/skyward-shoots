using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource activeSource;
    private AudioSource inactiveSource;

    public GameObject sourcePrefab;
    public float crossFade = 1.0f;

    private float switchTime;

    [SerializeField]
    private AudioClip track;
    public AudioClip Track
    {
        get => track;
        set
        {
            if (track != value)
            {
                switchTime = Time.time;
                track = value;

                AudioSource temp = activeSource;
                activeSource = inactiveSource;
                inactiveSource = temp;

                activeSource.clip = value;
                activeSource.timeSamples = inactiveSource.timeSamples;
                activeSource.Play();
            }
        }
    }

    private void OnValidate()
    {
        if (activeSource != null && inactiveSource != null)
        {
            Track = track;
        }
    }

    private void Awake()
    {
        activeSource = CreateSource();
        inactiveSource = CreateSource();
    }

    private void Start()
    {
        Track = track;
    }

    private AudioSource CreateSource()
    {
        return Instantiate(sourcePrefab, transform).GetComponentInChildren<AudioSource>();
    }

    private void Update()
    {
        float volume = Mathf.Clamp01(Mathf.InverseLerp(switchTime, switchTime + crossFade, Time.time));

        activeSource.volume = volume;
        inactiveSource.volume = 1 - volume;
    }
}
