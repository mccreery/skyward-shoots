using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Preload : MonoBehaviour
{
    public AudioMixer audioMixer;
    private void Start()
    {
        float value = PlayerPrefs.GetFloat("volume", -10);
        if (value == -20)
        {
            value = -80;
        }
        audioMixer.SetFloat("volume", value);
        DontDestroyOnLoad(transform.gameObject);
        SceneManager.LoadScene(1);
    }
}
