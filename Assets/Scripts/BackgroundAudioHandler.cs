using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundAudioHandler : MonoBehaviour
{
    AudioSource audioSource;
    float defaultVolume;
    public static BackgroundAudioHandler BAH; //objects referencing this class will have to use the value assigned to this variable

    // Start is called before the first frame update
    void Awake()
    {
        if (BAH && BAH != this)
        {
            Destroy(gameObject);
        }
        else
        {
            BAH = this;
            audioSource = GetComponent<AudioSource>();
            defaultVolume = audioSource.volume;
            SceneManager.sceneLoaded += OnSceneLoad;
            DontDestroyOnLoad(gameObject);
        }
    }

    void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0)
        {
            BAH = null;
            SceneManager.sceneLoaded -= OnSceneLoad;
            Destroy(gameObject);
        }
    }

    public void ChangeBackgroundVolume(float volume)
    {
        if (!audioSource) audioSource = GetComponent<AudioSource>();
        audioSource.volume = volume * defaultVolume;
    }
}
