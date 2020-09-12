using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityStandardAssets.Vehicles.Car;

public class TurnWheelDetection : EventScript
{
    /*
     * - x (or > 180) angle on car vehicle is point upwards
     * + x angle on car vehicle is poing downwards
     *
     */
    public float WheelAngleThreshold = 30;
    private Transform car;

    public override bool Pass { get => CheckWheelAngle(); set => base.Completed = value; }

    private void Awake()
    {
        EventType = EventTypes.Wheel;
    }
    public override void Initialize()
    {
        car = FindObjectOfType<CarUserControl>().transform;
        //Completed = true;
    }

    private bool CheckWheelAngle()
    {
        bool pass = false;

        float angle = car.localEulerAngles.x;
        Debug.Log("Car Angle: " + angle);
        if (angle > 180) angle -= 360;
        if(angle < 0) //car is pointed uphill
        {
            Debug.Log(FindObjectOfType<DashHandler>().GetWheelAngle());
            if (FindObjectOfType<DashHandler>().GetWheelAngle() > WheelAngleThreshold)
            {
                pass = true;
                Completed = true;
            }
        }
        else //car is pointed downhill
        {
            Debug.Log(FindObjectOfType<DashHandler>().GetWheelAngle());
            if (FindObjectOfType<DashHandler>().GetWheelAngle() < -WheelAngleThreshold)
            {
                pass = true;
                Completed = true;
            }
        }
        Pass = pass;
        return pass;
    }

    
}
