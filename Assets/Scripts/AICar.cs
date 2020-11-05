using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICar : MonoBehaviour
{
    private Transform[] Waypoints { get { return Markers.Markers; } }
    public AIMarkers Markers;
    private int waypointIndex = 0;
    private Vector3 direction;
    public GameObject FrontLeftWheel, FrontRightWheel, RearLeftWheel, RearRightWheel;
    public float WheelRadius = 0.0125f;
    private GameObject player;
    float angle;
    public GameObject HighBeams; //lights with flare
    public GameObject LowBeams;
    public float DistanceToPlayer = 25;
    
    private Vector3 velocityPos;
    private float wheelLeftAngle, wheelRightAngle;


    // Start is called before the first frame update
    void Start()
    {
        
        velocityPos = transform.position;
        wheelLeftAngle = FrontLeftWheel.transform.localEulerAngles.x;
        wheelRightAngle = FrontRightWheel.transform.localEulerAngles.x;
        
        direction = transform.forward;
        player = FindObjectOfType<UnityStandardAssets.Vehicles.Car.CarController>().gameObject;
    }
    private void FixedUpdate()
    {
        
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
        float angularDisplacement = (180/3.14f) *velocity / WheelRadius;
        wheelLeftAngle += angularDisplacement;
        wheelRightAngle += angularDisplacement;

        //save position for next frame
        velocityPos = transform.position;

        angle = Vector3.SignedAngle(transform.forward, Waypoints[waypointIndex].position - transform.position, transform.up);

        FrontLeftWheel.transform.localEulerAngles = new Vector3(wheelLeftAngle, angle, 0);
        FrontRightWheel.transform.localEulerAngles = new Vector3(wheelRightAngle, angle, 0);
        RearLeftWheel.transform.localEulerAngles = new Vector3(wheelLeftAngle, 0, 0);
        RearRightWheel.transform.localEulerAngles = new Vector3(wheelRightAngle, 0, 0);
        //car.Move(90 / angle, 0, 0, 0, 0);
        //Debug.Log(waypointIndex); still a problem (runtime error)
        direction = transform.forward;
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
