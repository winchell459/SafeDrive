using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchControlsKit;

public class CameraController : MonoBehaviour
{
    public float MaxTurn = 45;
    public float YawSpeed = 2;
    public float yaw = 0;
    public float Yaw { get { return yaw; } private set {; } }

    public bool isTouchControlled;
    // Start is called before the first frame update
    void Start()
    {
        isTouchControlled = MasterControl.MC.TouchControls;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTouchControlled && Input.GetMouseButton(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            yaw += YawSpeed * Input.GetAxis("Mouse X");
            yaw = Mathf.Clamp(yaw, -MaxTurn, MaxTurn);
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, yaw, transform.localEulerAngles.z);
        }
        else if (isTouchControlled)
        {
            Vector2 look = TCKInput.GetAxis("Touchpad");
            Debug.Log(look.x);
            yaw += 10 * YawSpeed * TCKInput.GetAxis("Touchpad").x;
            yaw = Mathf.Clamp(yaw, -MaxTurn, MaxTurn);
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, yaw, transform.localEulerAngles.z);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
