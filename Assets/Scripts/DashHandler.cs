using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class DashHandler : MonoBehaviour
{
    public DriverHandler Driver;
    public Text SpeedometerText;
    public Text ClockText;
    public Animator LeftIndicator, RightIndicator;
    public AudioClip HornClip;
    public AudioSource PlayerCameraSource;
    public RawImage HandbrakeSymbol;
    public Transform SteeringWheelImage;
    public GameObject LowBeams, HighBeams;

    private TimeManager tm;
    public GameObject IgnitionButton;
    //public string[] Indicators = { "left", "right"};
    public enum Indicator
    {
        left,
        right
    }
    // Start is called before the first frame update
    void Start()
    {
        SetHandbrake(false);
        LowBeams.SetActive(false);
        HighBeams.SetActive(false);
        tm = FindObjectOfType<TimeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        DisplaySpeed();
        HandleHorn();
        handleLights();
        handleClock();

        handleEngineToggle();
    }

    private void FixedUpdate()
    {
        //Debug.Log(Driver.GetSteeringAngle());
        handleSteeringWheel();
    }

    private void handleEngineToggle()
    {
        if (Input.GetKeyDown(KeyCode.I)) ToggleEngine();
    }
    private void handleClock()
    {
        float time = tm.CurrentTime;
        //hh:mm:ss
        int hours = (int)(time / 3600);
        time -= hours * 3600;
        int minutes = (int)(time / 60);
        time -= minutes * 60;
        string clock = "";
        if (hours < 10) clock += "0";
        clock += hours;
        clock += ":";
        if (minutes < 10) clock += "0";
        clock += minutes;
        if (hours >= 24) hours = 0;
        ClockText.text = clock;

    }

    private void handleLights()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(LowBeams.activeSelf || HighBeams.activeSelf)
            {
                HighBeams.SetActive(false);
                LowBeams.SetActive(false);
            }
            else
            {
                LowBeams.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if(LowBeams.activeSelf)
            {
                HighBeams.SetActive(true);
                LowBeams.SetActive(false);
            }
            else if (HighBeams.activeSelf)
            {
                HighBeams.SetActive(false);
                LowBeams.SetActive(true);
            }
        }
    }
    
    public bool HighBeamsOn() { return HighBeams.activeSelf; }
    public bool LowBeamsOn() { return LowBeams.activeSelf; }
    public float rotationRate = 45;

    private void handleSteeringWheel()
    {
        if (!GetHandbrake() && Driver.GetVehicleSpeed() > 0)
        {
            float angle = Driver.GetSteeringAngle();
            Vector3 currentAngles = SteeringWheelImage.localEulerAngles;
            SteeringWheelImage.localEulerAngles = new Vector3(currentAngles.x, currentAngles.y, -angle);
        }
        else if(GetHandbrake())
        {
            float h = Input.GetAxis("Horizontal");
            Vector3 currentAngles = SteeringWheelImage.localEulerAngles;
            float angle = currentAngles.z -h * rotationRate * Time.deltaTime;
            //-25 <-> 25
            //-180 < angle < 180
            if (angle < -180) angle += 360;
            else if (angle > 180) angle -= 360;
            //Debug.Log(angle);
            angle = Mathf.Clamp(angle, -25, 25);
            SteeringWheelImage.localEulerAngles = new Vector3(currentAngles.x, currentAngles.y, angle);

        }
    }         

    bool honking = false;
    private void HandleHorn()
    {
        if (!honking && Input.GetAxis("Honk") > 0.1f)
        {
            honking = true;
            PlayerCameraSource.clip = HornClip;
            PlayerCameraSource.Play();
        }
        else if (Input.GetAxis("Honk") < 0.1f) honking = false;
    }

    private void DisplaySpeed()
    {
        //45.899289 = 45.8; 45.08428 = 45 or 45.0
        float speed = Driver.GetVehicleSpeed();
        string display = ((int)speed).ToString();
        speed -= (int)speed; //0.899289
        if ((int)(speed*10) > 0) display += "." + (int)(speed*10);

        SpeedometerText.text = display;
    }

    public void SetIndicator(Indicator indicator, bool active)
    {
        if(indicator == Indicator.left)
        {
            LeftIndicator.SetBool("Blinking", active);
            //DriverHandler dh = FindObjectOfType<DriverHandler>();
        }
        else if(indicator == Indicator.right)
        {
            RightIndicator.SetBool("Blinking", active);
        }
        
    }
    
    public bool GetIndicator(Indicator indicator)
    {
        if (indicator == Indicator.left) return LeftIndicator.GetBool("Blinking");
        else return RightIndicator.GetBool("Blinking");
    }

    public void ToggleIndicator(Indicator indicator)
    {
        if (GetIndicator(indicator))
        {
            //blinker stops
            SetIndicator(indicator, false);
        }
        else
        {
            SetIndicator(indicator, true);
        }
        if (indicator == Indicator.left)
        {
            SetIndicator(Indicator.right, false);
        }
        if (indicator == Indicator.right)
        {
            SetIndicator(Indicator.left, false);
        }
    }

    public bool GetHandbrake()
    {
        return HandbrakeSymbol.gameObject.activeSelf;
    }
    public void SetHandbrake(bool active)
    {
        HandbrakeSymbol.gameObject.SetActive(active);
    }

    public float GetSpeed()
    {
        return Driver.GetVehicleSpeed();
    }
    public float GetWheelAngle()
    {
        float angle = SteeringWheelImage.localEulerAngles.z;
        if (angle >= 180) angle -= 360;
        return angle;
    }

    public void ToggleEngine()
    {
        //if (Driver.GetVehicleSpeed() > 0.1f) return;

        if (Driver.GetVehicleSpeed() < 0.1f)
        {
            if (Driver.ToggleEngine()) //bool values when not defined explicitly are by default true
            {
                IgnitionButton.transform.GetChild(0).GetComponent<Text>().text = "Engine On";
            }
            else
            {
                IgnitionButton.transform.GetChild(0).GetComponent<Text>().text = "Engine Off";
            }
        }
    }
}
