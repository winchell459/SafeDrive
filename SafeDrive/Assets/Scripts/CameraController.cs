using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speedH = 2;
    private float yaw = 0;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        yaw += speedH * Input.GetAxis("Mouse X");
        //pitch += speedV * Input.GetAxis("Mouse Y");

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, yaw, transform.localEulerAngles.z);
    }
}
