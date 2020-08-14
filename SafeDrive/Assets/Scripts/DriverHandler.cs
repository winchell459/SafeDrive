using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriverHandler : MonoBehaviour
{
    public UnityStandardAssets.Vehicles.Car.CarController Vehicle;
    public DashHandler Dash;

    public float GetVehicleSpeed()
    {
        float speed = Vehicle.CurrentSpeed;
        return speed;
    }

    private void Update()
    {
        HandleDriverInput();
    }

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
    }
}
