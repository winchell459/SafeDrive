using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriverHandler : MonoBehaviour
{
    public UnityStandardAssets.Vehicles.Car.CarController Vehicle;
    public UnityStandardAssets.Vehicles.Car.CarUserControl Controller;
    public DashHandler Dash;
    public MasterControl MC;

    public float GetVehicleSpeed()
    {
        float speed = Vehicle.CurrentSpeed;
        return speed;
    }

    public float GetSteeringAngle()
    {
        return Vehicle.CurrentSteerAngle;
    }

    

    private void Update()
    {
        Controller.ControlsPaused = MC.Paused;
        HandleDriverInput();
        
    }

    bool handbrakeDown = false;
    private void HandleDriverInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Dash.ToggleIndicator(DashHandler.Indicator.left);
        }

        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            Dash.ToggleIndicator(DashHandler.Indicator.right);
        }

        if(!handbrakeDown && Input.GetAxis("Jump") > 0.1f)
        {
            handbrakeDown = true;
            Dash.SetHandbrake(!Dash.GetHandbrake());
            Controller.HandbrakeSet = Dash.GetHandbrake();
        }else if(handbrakeDown && Input.GetAxis("Jump") < 0.1f) {
            
            handbrakeDown = false;
        }
    }

    public bool ToggleEngine()
    {
        if (Controller.enabled)
        {
            Debug.Log("Toggle off");
            Controller.enabled = false;
            foreach(AudioSource AS in Controller.GetComponents<AudioSource>())
            {
                AS.enabled = false;
            }
            return false;
        }
        else
        {
            Controller.enabled = true;
            foreach (AudioSource AS in Controller.GetComponents<AudioSource>())
            {
                AS.enabled = true;
            }
            return true;
        }
    }

    public bool EngineState()
    {
        return Controller.enabled;
    }
}
