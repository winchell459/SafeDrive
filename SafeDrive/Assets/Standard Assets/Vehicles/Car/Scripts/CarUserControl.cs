using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
//using Control;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use
        public bool HandbrakeSet = false;
        //public Control.MasterControl MC;
        
        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
        }


        private void FixedUpdate()
        {
            // pass the input to the car!
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
            float b = Input.GetKey(KeyCode.B)? -1: 0;
#if !MOBILE_INPUT
            //float handbrake = CrossPlatformInputManager.GetAxis("Jump");
            float handbrake = 0;
            if (HandbrakeSet)
            {
                v = 0;
                handbrake = 1;
            }
            m_Car.Move(h, v, b, handbrake);
#else
            m_Car.Move(h, v, v, 0f);
#endif
        }
    }
}
