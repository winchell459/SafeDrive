using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICar : MonoBehaviour
{
    public Transform[] Waypoints;
    private int waypointIndex = 0;
    private Vector3 direction;
    private UnityStandardAssets.Vehicles.Car.CarController car;
    public GameObject FrontLeftWheel, FrontRightWheel;
    float angle;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<NavMeshAgent>().SetDestination(Waypoints[waypointIndex].position);
        direction = transform.forward;
        car = GetComponent<UnityStandardAssets.Vehicles.Car.CarController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(Waypoints[waypointIndex].position, transform.position) < 1)
        {
            GetComponent<NavMeshAgent>().speed = Waypoints[waypointIndex].GetComponent<AIMarker>().Speed;

            waypointIndex += 1;
            waypointIndex %= Waypoints.Length;
            GetComponent<NavMeshAgent>().SetDestination(Waypoints[waypointIndex].position);

            angle = Vector3.SignedAngle(transform.forward, Waypoints[waypointIndex].position - transform.position, transform.up);
        }

        //angle = Vector3.SignedAngle(transform.forward, direction, transform.up);
        angle = Vector3.SignedAngle(transform.forward, Waypoints[waypointIndex].position - transform.position, transform.up);
        FrontLeftWheel.transform.localEulerAngles = new Vector3(0, angle, 0);
        FrontRightWheel.transform.localEulerAngles = new Vector3(0, angle, 0);

        direction = transform.forward;
    }
}
