using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriverHandler : MonoBehaviour
{
    public UnityStandardAssets.Vehicles.Car.CarController Vehicle;
    public UnityStandardAssets.Vehicles.Car.CarUserControl Controller;
    public DashHandler Dash;

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
}
