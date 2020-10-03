using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float MaxTurn = 45;
    public float YawSpeed = 2;
    public float yaw = 0;
    public float Yaw { get { return yaw; } private set {; } }
    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            yaw += YawSpeed * Input.GetAxis("Mouse X");
            yaw = Mathf.Clamp(yaw, -MaxTurn, MaxTurn);
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, yaw, transform.localEulerAngles.z);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
