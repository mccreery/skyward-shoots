using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;

    private void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("volume", -10);
    }

    public void OnSliderChange(Single value)
    {
        if (value == -20)
        {
            value = -80;
        }
        PlayerPrefs.SetFloat("volume", value);
        audioMixer.SetFloat("volume", value);
    }
}
