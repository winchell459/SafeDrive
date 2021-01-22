using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TouchControlsKit;
using UnityStandardAssets.Vehicles.Car;

public class OptionsMenuHandler : MonoBehaviour
{
    public GameObject OptionsPanel;
    public GameObject OptionsPanelTouch;
    public GameObject OptionsPanelNonTouch;
    public GameObject CheatSheet;
    public GameObject MobileControlsPage;
    public GameObject volumeSlider;
    public Slider VolumeConsoleSlider;
    public Slider VolumeTouchSlider;
    public GameObject OptionsButton;

    public Slider MobileSensitivitySlider;
    public Slider ConsoleSensitivitySlider;
    [Range(1, 5)]
    public float MobileSensitivityDefault = 1;
    [Range(10, 100)]
    public float ConsoleSensitivityDefault = 10;
    public void Start()
    {
        SetupTouchControls();
        SetDefaultVolume();
        //foreach (AudioSource source in GameObject.FindObjectsOfType<AudioSource>())
        //{
        //    //Debug.Log(source.gameObject.name + ": " + source.volume);
        //}
    }
    public VolumeControl volControl;
    private bool optionsOpen;

    public void SetupTouchControls()
    {
        //OptionsPanel.GetComponent<RectTransform>().localPosition = new Vector3(400, 235, 0);
        if (MasterControl.MC.TouchControls)
        {
            OptionsPanel = OptionsPanelTouch;
            volumeSlider = VolumeTouchSlider.gameObject;
            float value = PlayerPrefs.GetFloat("Turn Sensitivity", MobileSensitivityDefault);
            SetMobileSensitivity(value);
            MobileSensitivitySlider.value = value;
        }
        else
        {
            OptionsPanel = OptionsPanelNonTouch;
            volumeSlider = VolumeConsoleSlider.gameObject;
            float value = PlayerPrefs.GetFloat("Turn Sensitivity", ConsoleSensitivityDefault);
            SetConsoleSensitivity(value);
            ConsoleSensitivitySlider.value = value;
        }
    }

    public void ToggleOptionPanel()
    {
        OptionsButton.SetActive(OptionsPanel.activeSelf);
        OptionsPanel.SetActive(!OptionsPanel.activeSelf);
        if (OptionsPanel.activeSelf) CheatSheet.SetActive(false);
        //{
        //    CheatSheet.SetActive(false);
        //    OptionsButton.SetActive(false);
        //}
        //else if (!OptionsPanel.activeSelf)
        //{
        //    OptionsButton.SetActive(true);
        //}
        optionsOpen = OptionsPanel.activeSelf;
    }
    
    public void ToggleMobileControlsPage()
    {
        MobileControlsPage.SetActive(!MobileControlsPage.activeSelf);

        if (MobileControlsPage.activeSelf)
        {
            OptionsPanel.SetActive(false);
            OptionsButton.SetActive(false);
        }
        else if (optionsOpen)
        {
            OptionsPanel.SetActive(true);
            OptionsButton.SetActive(false);
        }
        else OptionsButton.SetActive(true);
    }

    public void ToggleCheatSheet()
    {
        CheatSheet.SetActive(!CheatSheet.activeSelf);
        if (CheatSheet.activeSelf)
        {
            OptionsPanel.SetActive(false);
            OptionsButton.SetActive(false);
        }
        else if (optionsOpen)
        {
            OptionsPanel.SetActive(true);
            OptionsButton.SetActive(false);
        }
        else OptionsButton.SetActive(true);
    }

    public void ReturnToMainMenu()
    {
        FindObjectOfType<SystemHandler>().LoadMainMenu(); 
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
        //Debug.Log("SliderValue: " + volume);
    }
    
    public void SetMobileSensitivity(float value)
    {
        PlayerPrefs.SetFloat("Turn Sensitivity", value);
        FindObjectOfType<TCKJoystick>().sensitivity = value;
    }

    public void SetConsoleSensitivity(float value)
    {
        PlayerPrefs.SetFloat("Turn Sensitivity", value);
        GameObject.FindWithTag("Player").GetComponent<CarController>().TurnFuncBase = value;
    }

    public void SetDefaultVolume()
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
        if (!DashVolume) DashVolume = GameObject.FindObjectOfType<DashHandler>();
        DashVolume.volumeControl = volume;
        if (!CarVolume) CarVolume = GameObject.FindObjectOfType<UnityStandardAssets.Vehicles.Car.CarAudio>();
        CarVolume.volumeControl = volume;
        if (!Wheels) Wheels = GameObject.FindObjectOfType<UnityStandardAssets.Vehicles.Car.CarController>().Wheels;
        foreach (AudioSource source in Wheels.transform.GetComponentsInChildren<AudioSource>())
        {
            source.volume = volume;
        }
        if (!BackgroundAudio) BackgroundAudio = GameObject.FindObjectOfType<BackgroundAudioHandler>();
        BackgroundAudio.ChangeBackgroundVolume(volume);
    }
}

