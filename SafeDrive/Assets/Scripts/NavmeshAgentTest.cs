using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavmeshAgentTest : MonoBehaviour
{
    private NavMeshAgent agent;
    public Vector3 MoveTo;
    private float height;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Vector3.Distance(transform.position, MoveTo));
        if(Vector3.Distance(transform.position, MoveTo) < 1.0f)
        {
            MoveTo = new Vector3(Random.Range(-10, 10), 0.5f, Random.Range(-10, 10));
        }
        agent.SetDestination(MoveTo);

    }
}
