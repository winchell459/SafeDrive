using System.Collections;
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

    public Transform RearCheck, FrontCheck;
    public float RotateRate = 1;
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

    void FixedUpdate()
    {
       // print("FixedUpdate");
        
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

    

    private void CheckForGround()
    {
        float rearHeight = 1000000;
        RaycastHit hit1;
        //Physics.Raycast()
        if(Physics.Raycast(RearCheck.position, -transform.up, out hit1, 10000, RoadMask ))
        {
            rearHeight = hit1.distance;
        }

        float frontHeight = 1000000;
        RaycastHit hit2;
        if (Physics.Raycast(FrontCheck.position, -transform.up, out hit2, 10000, RoadMask))
        {
            frontHeight = hit2.distance;
        }

        float z = Vector3.Distance(RearCheck.position, FrontCheck.position);

        float theta = Mathf.Atan(Mathf.Abs(rearHeight - frontHeight) / z) * 180f / 3.14f;
        if (hit1.point.y > hit2.point.y)
        {
            //transform.localEulerAngles = transform.localEulerAngles + new Vector3(-RotateRate, 0, 0);
            Debug.Log("down hill");
        }
        else if (hit1.point.y < hit2.point.y)
        {
            //transform.localEulerAngles = transform.localEulerAngles + new Vector3(RotateRate, 0, 0);
            theta *= -1;
            Debug.Log("up hill");
        }

        
        print(frontHeight + " " + rearHeight + " -> " + theta + " " + transform.localEulerAngles.x + " " + z);
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, transform.localEulerAngles.z) + new Vector3(theta, 0, 0);

        
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
