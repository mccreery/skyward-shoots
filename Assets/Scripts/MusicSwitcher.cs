using System;
using UnityEngine;

public class MusicSwitcher : MonoBehaviour
{
    public Stop[] stops;

    [Serializable]
    public struct Stop
    {
        public float height;
        public AudioClip music;
    }

    private MusicManager musicManager;

    private void Awake()
    {
        musicManager = FindObjectOfType<MusicManager>();
    }

    private void Update()
    {
        for (int i = stops.Length - 1; i >= 0; i--)
        {
            Stop stop = stops[i];
            if (transform.position.y > stop.height)
            {
                musicManager.Track = stop.music;
                break;
            }
        }
    }
}
