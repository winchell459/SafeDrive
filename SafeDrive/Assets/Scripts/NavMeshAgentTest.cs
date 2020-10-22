using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentTest : MonoBehaviour
{
    private NavMeshAgent agent;
    public Vector3 MoveTo;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, MoveTo) < 0.1f)
        {
            MoveTo = new Vector3(Random.Range(-10, 10), 0.5f, Random.Range(-10, 10));
        }
        agent.SetDestination(MoveTo);
    }
}
