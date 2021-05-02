using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneButton : MonoBehaviour
{
    public AudioClip select;
    public int sceneNumber;
    public void LoadScene()
    {
        GameObject.Find("Menu Audio Source").GetComponent<AudioSource>().PlayOneShot(select);
        SceneManager.LoadScene(sceneNumber);
    }
}
