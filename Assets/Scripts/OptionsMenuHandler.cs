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
        SetVolume();
        foreach (AudioSource source in GameObject.FindObjectsOfType<AudioSource>())
        {
            Debug.Log(source.gameObject.name + ": " + source.volume);
        }
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
        volControl.SetVolume(volumeSlider.GetComponent<UnityEngine.UI.Slider>().value);
        Debug.Log("SliderValue: " + volumeSlider.GetComponent<UnityEngine.UI.Slider>().value);
    }
}

[System.Serializable]
public class VolumeControl
{
    public DashHandler DashVolume;
    public UnityStandardAssets.Vehicles.Car.CarAudio CarVolume;
    public GameObject Wheels;

    public void SetVolume(float volume)
    {
        DashVolume.volumeControl = volume;
        CarVolume.volumeControl = volume;
        foreach (AudioSource source in Wheels.transform.GetComponentsInChildren<AudioSource>())
        {
            source.volume = volume;
        }
    }
}

