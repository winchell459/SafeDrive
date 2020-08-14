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

    //public string[] Indicators = { "left", "right" };
    public enum Indicator
    {
        left, //0
        right //1
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DisplaySpeed();
        HandleHorn();
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
}
