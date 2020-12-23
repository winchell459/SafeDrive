using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenuHandler : MonoBehaviour
{
    public GameObject OptionsPanel;
    public GameObject CheatSheet;
    public GameObject volumeSlider;

    public void Start()
    {

        SetDefaultVolume();
        //foreach (AudioSource source in GameObject.FindObjectsOfType<AudioSource>())
        //{
        //    Debug.Log(source.gameObject.name + ": " + source.volume);
        //}
    }
    public VolumeControl volControl;
    private bool optionsOpen;

    public void ToggleOptionPanel()
    {
        OptionsPanel.SetActive(!OptionsPanel.activeSelf);
        if (OptionsPanel.activeSelf) CheatSheet.SetActive(false);
        optionsOpen = OptionsPanel.activeSelf;
    }

    public void ToggleCheatSheet()
    {
        CheatSheet.SetActive(!CheatSheet.activeSelf);
        if (CheatSheet.activeSelf) OptionsPanel.SetActive(false);
        else if (optionsOpen) OptionsPanel.SetActive(true);
    }
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) ToggleCheatSheet();
        if (Input.GetKeyDown(KeyCode.O)) ToggleOptionPanel();
    }

    public void SetVolume()//UnityEngine.UI.Slider slider
    {
        float volume = volumeSlider.GetComponent<UnityEngine.UI.Slider>().value;
        
        volControl.SetVolume(volume);
        Debug.Log("SliderValue: " + volume);
    }
    private void SetDefaultVolume()
    {
        float volume = volumeSlider.GetComponent<UnityEngine.UI.Slider>().value;
        if (PlayerPrefs.HasKey("Volume"))
        {
            volume = PlayerPrefs.GetFloat("Volume");
        }
        volumeSlider.GetComponent<UnityEngine.UI.Slider>().value = volume;
        volControl.SetVolume(volume);
        Debug.Log("SliderValue: " + volume);
    }

    public void ResetStageButton()
    {
        FindObjectOfType<UnitHandler>().ResetCurrentStage();
        
    }

    public void ReturnToMainMenuButton()
    {
        FindObjectOfType<SystemHandler>().LoadMainMenu();
    }
}

[System.Serializable]
public class VolumeControl
{
    public DashHandler DashVolume;
    public UnityStandardAssets.Vehicles.Car.CarAudio CarVolume;
    public GameObject Wheels;
    public BackgroundAudioHandler BackgroundAudio;

    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat("Volume", volume);
        if(!DashVolume) DashVolume = GameObject.FindObjectOfType<DashHandler>();
        DashVolume.volumeControl = volume;
        if (!CarVolume) CarVolume = GameObject.FindObjectOfType<UnityStandardAssets.Vehicles.Car.CarAudio>();
        CarVolume.volumeControl = volume;
        if(!Wheels) Wheels = GameObject.FindObjectOfType<UnityStandardAssets.Vehicles.Car.CarController>().Wheels;
        foreach (AudioSource source in Wheels.transform.GetComponentsInChildren<AudioSource>())
        {
            source.volume = volume;
        }
        if (!BackgroundAudio) BackgroundAudio = GameObject.FindObjectOfType<BackgroundAudioHandler>();
        else
        {
            BackgroundAudio.ChangeVolume(volume);
        }
    }
}

