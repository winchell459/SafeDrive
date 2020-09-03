using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : EventScript
{
    public static bool CollisionCompleted;
    public static bool CollisionPass;
    public override bool Pass
    {
        get => CollisionPass;
        set { CollisionPass = value; _pass = value; }
    }
    public override bool Completed {
        get => CollisionCompleted;
        set { CollisionCompleted = value; _completed = value; }
    }

    public string[] AvoidTags = { "Building",  "Pedestrian", "Curb", "Obsticle", "Vehicle"};
    //public List<string> AvoidTagsList = new List<string>();

    public override void Initialize()
    {
        Pass = true;
        Completed = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (string tag in AvoidTags) {
            if (collision.gameObject.CompareTag(tag))
            {
                Pass = false;
                Completed = true;
            }
         }

        Debug.Log(collision.gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (string tag in AvoidTags)
        {
            if (other.gameObject.CompareTag(tag))
            {
                Pass = false;
                Completed = true;
            }
        }
    }
}
