using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundAudioHandler : MonoBehaviour
{
    AudioSource audioSource;
    float defaultVolume;
    public static BackgroundAudioHandler BAH;
    
    // Start is called before the first frame update
    void Awake()
    {
        if(BAH && BAH != this)
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
            if (gameObject) Destroy(gameObject);
        }
            
    }

    public void ChangeVolume(float volume)
    {
        if (!audioSource) audioSource = GetComponent<AudioSource>();
        audioSource.volume = volume * defaultVolume;
    }
}
