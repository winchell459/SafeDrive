using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashHandler : MonoBehaviour
{
    public DriverHandler Driver;
    public Text SpeedometerText;
    public Animator LeftIndicator, RightIndicator;
    public AudioClip HornClip;
    public AudioSource PlayerCameraSource;
    public RawImage HandbrakeSymbol;
    public Transform SteeringWheelImage;
    public GameObject LowBeams, HighBeams; //press button off -> on -> high -> off  ||  off -> on -> high -> one -> off

    //public string[] Indicators = { "left", "right" };
    public enum Indicator
    {
        left, //0
        right //1
    }
    // Start is called before the first frame update
    void Start()
    {
        SetHandbrake(false);
        LowBeams.SetActive(false);
        HighBeams.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        DisplaySpeed();
        HandleHorn();
        handleLights();
    }

    private void FixedUpdate()
    {
        //Debug.Log(Driver.GetSteeringAngle());
        handleSteeringWheel();
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
                LowBeams.SetActive(false);
                HighBeams.SetActive(true);
            }else if (HighBeams.activeSelf)
            {
                LowBeams.SetActive(true);
                HighBeams.SetActive(false);
            }
        }
    }

    public bool HighBeamsOn() { return HighBeams.activeSelf; }
    public bool LowBeamsOn() { return LowBeams.activeSelf; }

    private void handleSteeringWheel()
    {
        if(Driver.GetVehicleSpeed() > 0 )
        {
            float angle = Driver.GetSteeringAngle();
            Vector3 currentAngles = SteeringWheelImage.localEulerAngles;

            SteeringWheelImage.localEulerAngles = new Vector3(currentAngles.x, currentAngles.y, -angle);
        }
        else if(GetHandbrake())
        {
            float h = Input.GetAxis("Horizontal");
            //finish parked wheel angle
            //float angle = Mathf.Clamp(angle, -25, 25);
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
        else if(Input.GetAxis("Honk") < 0.1f) honking = false;
    }

    public bool GetHandbrake()
    {
        return HandbrakeSymbol.gameObject.activeSelf;
    }
    public void SetHandbrake(bool active)
    {
        HandbrakeSymbol.gameObject.SetActive(active);
    }

    private void DisplaySpeed()
    {
        //45.8098543 = 45.8; 45.089809845 = 45 or 45.0
        float speed = Driver.GetVehicleSpeed();
        string display = ((int)speed).ToString();
        speed -= (int)speed; // 0.80985..
        if((int)(speed*10) > 0) display += "." + (int)(speed * 10);
        //display = display + "." //.....


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
        else if (indicator == Indicator.right)
        {
            
            SetIndicator(Indicator.left, false);
        }
    }

    public float GetSpeed()
    {
        return Driver.GetVehicleSpeed();
    }
    public float GetWheelAngle()
    {
        return SteeringWheelImage.localEulerAngles.z;
    }
}
