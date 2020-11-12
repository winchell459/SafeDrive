using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    public Light Red, Yellow, Green;
    public float GreenDuration = 5, RedDuration = 5, YellowDuration = 1;
    private float greenStart, redStart, yellowStart;
    public bool RedWaitUntilTriggered = false;
    // Start is called before the first frame update
    void Start()
    {
        ChangeLight("r");
    }

    // Update is called once per frame
    private void Update()
    {
        if(Green.gameObject.activeSelf)
        {
            if (greenStart + GreenDuration < Time.time) ChangeLight("y");

        }else if(Yellow.gameObject.activeSelf)
        {
            if (yellowStart + YellowDuration < Time.time) ChangeLight("r");

        }else if(!RedWaitUntilTriggered)
        {
            if (redStart + RedDuration < Time.time) ChangeLight("g");
        }else if(RedWaitUntilTriggered)
        {
            redStart = Time.time;
        }
    }

    public void ChangeLight(string color)
    {
        if (color == "green" || color == "Green" || color == "g" || color == "G" )
        {
            Green.gameObject.SetActive(true);
            Yellow.gameObject.SetActive(false);
            Red.gameObject.SetActive(false);
            greenStart = Time.time;
        }
        else if (color == "yellow" || color == "Yellow" || color == "y" || color == "Y")
        {
            Green.gameObject.SetActive(false);
            Yellow.gameObject.SetActive(true);
            Red.gameObject.SetActive(false);
            yellowStart = Time.time;
        }
        else if (color == "red" || color == "Red" || color == "r" || color == "R")
        {
            Green.gameObject.SetActive(false);
            Yellow.gameObject.SetActive(false);
            Red.gameObject.SetActive(true);
            redStart = Time.time;
        }
    }
}
