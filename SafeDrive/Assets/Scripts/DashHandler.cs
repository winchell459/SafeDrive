using System.Collections;
using System.Collections.Generic;
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
    public GameObject LowBeams, HighBeams; //press button off -> on -> high -> off  ||  off -> on -> high -> one -> off

    private TimeManager tm;

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
        tm = FindObjectOfType<TimeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        DisplaySpeed();
        HandleHorn();
        handleLights();
        handleClock();
    }

    private void FixedUpdate()
    {
        //Debug.Log(Driver.GetSteeringAngle());
        handleSteeringWheel();
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

    public float rotationRate = 5;
    private void handleSteeringWheel()
    {
        if(!GetHandbrake() && Driver.GetVehicleSpeed() > 0 )
        {
            float angle = Driver.GetSteeringAngle();
            Vector3 currentAngles = SteeringWheelImage.localEulerAngles;

            SteeringWheelImage.localEulerAngles = new Vector3(currentAngles.x, currentAngles.y, -angle);
        }
        else if(GetHandbrake())
        {
            //Debug.Log("Parking Brake On");
            float h = Input.GetAxis("Horizontal");
            
            //finish parked wheel angle
            Vector3 currentAngles = SteeringWheelImage.localEulerAngles;
            float angle = currentAngles.z - h * rotationRate * Time.deltaTime;
            //-25 > angle < 25
            //-180 > angle < 180
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
        float angle = SteeringWheelImage.localEulerAngles.z;
        if (angle > 180) angle -= 360;
        return angle;
    }
}
