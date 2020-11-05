using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMarkers : MonoBehaviour
{
    public Transform[] Markers;
    public bool LoadMarkersOnStart;
    private void Start()
    {
        if (LoadMarkersOnStart)
        {
            int markerCount = transform.childCount;
            Transform[] markers = new Transform[markerCount];
            for (int i = 0; i < markerCount; i += 1)
            {
                markers[i] = transform.GetChild(i);
            }
            Markers = markers;
        }
    }
}
