﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICar : MonoBehaviour
{
    private Transform[] Waypoints { get { return Markers.Markers; } }
    public AIMarkers Markers;
    public int waypointIndex = 0;
    private Vector3 direction;
    public GameObject FrontLeftWheel, FrontRightWheel, RearLeftWheel, RearRightWheel;
    public float WheelRadius = 0.0125f;
    private GameObject player;
    float angle;
    public GameObject HighBeams; //lights with flare
    public GameObject LowBeams;
    public float DistanceToPlayer = 25;

    private Vector3 velocityPos;
    private float wheelAngle;

    public Transform RearChecker, FrontChecker;
    public LayerMask RoadMask;

    // Start is called before the first frame update
    void Start()
    {
        velocityPos = transform.position;
        wheelAngle = FrontLeftWheel.transform.localEulerAngles.x;
        //GetComponent<NavMeshAgent>().SetDestination(Waypoints[waypointIndex].position);
        direction = transform.forward;
        player = FindObjectOfType<UnityStandardAssets.Vehicles.Car.CarController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(LowBeams.activeSelf && Vector3.Distance(player.transform.position, transform.position) < DistanceToPlayer)
        {
            HighBeams.SetActive(false);
        }
        else if (LowBeams.activeSelf)
        {
            HighBeams.SetActive(true);
        }
        if(Vector3.Distance(Waypoints[waypointIndex].position, transform.position) < 1)
        {
            GetComponent<NavMeshAgent>().speed = Waypoints[waypointIndex].GetComponent<AIMarker>().Speed;

            waypointIndex += 1;
            waypointIndex %= Waypoints.Length;
            GetComponent<NavMeshAgent>().SetDestination(Waypoints[waypointIndex].position);

            angle = Vector3.SignedAngle(transform.forward, Waypoints[waypointIndex].position - transform.position, transform.up);
            //car.Move(90 / angle, 0, 0, 0, 0);
        }

        //calc the wheel angular velocity
        Vector3 displacement = transform.position - velocityPos;
        //dot product gets the velocity in the forward direction of vehicle
        float velocity = Vector3.Dot(displacement, transform.forward);
        float angularDisplacement = (180 / 3.14f) * velocity / WheelRadius;
        wheelAngle += angularDisplacement;

        //set position for next frame
        velocityPos = transform.position;

        angle = Vector3.SignedAngle(transform.forward, Waypoints[waypointIndex].position - transform.position, transform.up);
        FrontLeftWheel.transform.localEulerAngles = new Vector3(wheelAngle, angle, 0);
        FrontRightWheel.transform.localEulerAngles = new Vector3(wheelAngle, angle, 0);
        RearLeftWheel.transform.localEulerAngles = new Vector3(wheelAngle, angle, 0);
        RearRightWheel.transform.localEulerAngles = new Vector3(wheelAngle, angle, 0);
        //car.Move(90 / angle, 0, 0, 0, 0);
        //Debug.Log(waypointIndex); still a problem (runtime error)
        direction = transform.forward;

        CheckForGround();
    }
    //sets angle of AICars relative to ground
    void CheckForGround()
    {
        float rearHeight = float.PositiveInfinity;
        float frontHeight = float.PositiveInfinity;

        RaycastHit rearHit;
        RaycastHit frontHit;

        if (Physics.Raycast(RearChecker.position, -transform.up, out rearHit, float.PositiveInfinity, RoadMask)) rearHeight = rearHit.distance;
        if (Physics.Raycast(FrontChecker.position, -transform.up, out frontHit, float.PositiveInfinity, RoadMask)) frontHeight = frontHit.distance;
        float z = Vector3.Distance(RearChecker.position, FrontChecker.position);
        float theta = Mathf.Atan(Mathf.Abs(rearHeight - frontHeight) / z) * 180 / 3.14f;
        if (rearHit.point.y < frontHit.point.y) theta *= -1;
        transform.localEulerAngles = new Vector3(theta, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MovementPause(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MovementPause(false);
        }
    }

    public void MovementPause(bool pause)
    {
        if (pause)
        {
            GetComponent<NavMeshAgent>().SetDestination(transform.position);
        }
        else
        {
            GetComponent<NavMeshAgent>().SetDestination(Waypoints[waypointIndex].position);
        }
    }
}
