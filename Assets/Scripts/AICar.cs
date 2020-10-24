using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICar : MonoBehaviour
{
    public Transform[] Waypoints;
    private int waypointIndex = 0;
    private Vector3 direction;
    public GameObject FrontLeftWheel, FrontRightWheel;
    private GameObject player;
    float angle;
    public GameObject HighBeams; //lights with flare
    public GameObject LowBeams;
    public float DistanceToPlayer = 25;
    // Start is called before the first frame update
    void Start()
    {
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
        angle = Vector3.SignedAngle(transform.forward, Waypoints[waypointIndex].position - transform.position, transform.up);
        FrontLeftWheel.transform.localEulerAngles = new Vector3(0, angle, 0);
        FrontRightWheel.transform.localEulerAngles = new Vector3(0, angle, 0);
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
