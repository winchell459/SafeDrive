using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchControlsKit;

public class DriverHandler : MonoBehaviour
{
    public UnityStandardAssets.Vehicles.Car.CarController Vehicle;
    public UnityStandardAssets.Vehicles.Car.CarUserControl Controller;
    public DashHandler Dash;
    public MasterControl MC;
    private bool touchControls;
    public GameObject TCKCanvas;

    public OptionsMenuHandler OMH;

    public bool isJoystickVelocity;
    private void Awake()
    {
        if (MC.TouchControls == true)
        {
            touchControls = true;
            Controller.TouchControls = true;
            Dash.SetupTouchControl();
            OMH.SetupTouchControls();
        }
        else
        {
            TCKCanvas.SetActive(false);
            Controller.TouchControls = false;
        }
    }

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
        if (touchControls)
        {
            if (isJoystickVelocity)
            {
                Controller.joystick = TCKInput.GetAxis("Joystick");
            }
            else
            {
                float steering = TCKInput.GetAxis("Joystick").x;
                float accel = TCKInput.GetAction("AccelBtn", EActionEvent.Press) ? 1 : 0;
                float reverse = TCKInput.GetAction("ReverseBtn", EActionEvent.Press) ? -1 : 0;

                Controller.joystick = new Vector2(steering, accel + reverse);
            }
            Controller.brake = TCKInput.GetAction("BrakeBtn", EActionEvent.Press) ? -1 : 0;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) || (touchControls && TCKInput.GetAction("LBlinkerBtn", EActionEvent.Down)))
        {
            Dash.ToggleIndicator(DashHandler.Indicator.left);
        }

        if (Input.GetKeyDown(KeyCode.RightShift) || (touchControls && TCKInput.GetAction("RBlinkerBtn", EActionEvent.Down)))
        {
            Dash.ToggleIndicator(DashHandler.Indicator.right);   
        }

        if(!handbrakeDown && Input.GetAxis("Jump") > 0.1f || (touchControls && TCKInput.GetAction("HBrakeBtn", EActionEvent.Down)))
        {
            handbrakeDown = true;
            Dash.SetHandbrake(!Dash.GetHandbrake());
            Controller.HandbrakeSet = Dash.GetHandbrake();
        }else if(handbrakeDown && Input.GetAxis("Jump") < 0.1f || (touchControls && TCKInput.GetAction("HBrakeBtn", EActionEvent.Down)))
        {
            handbrakeDown = false;
        }
    }

    public bool ToggleEngine()                                  
    {
        if (Controller.enabled)
        {
            Controller.enabled = false;
            foreach (AudioSource AS in Controller.GetComponents<AudioSource>())
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
