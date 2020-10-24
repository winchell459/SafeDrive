using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour
{
    public bool MarkerActive
    {
        get { return markerActive; }
        set { SetMarkerActive(value); }
    }
    private bool markerActive = false;
    public Marker NextMarker;
    public Marker Previous;
    public GameObject MarkerMesh;

    private void Start()
    {
        if(NextMarker) NextMarker.Previous = this;
        MarkerMesh.SetActive(false);
    }

    public void SetMarkerActive(bool value)
    {
        markerActive = value;
        if (markerActive)
        {
            Marker next = this;
            while (next)
            {
                next.MarkerMesh.SetActive(true);
                next = next.NextMarker;
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Marker previous = this;
            while (previous)
            {
                previous.MarkerMesh.SetActive(false);
                previous = previous.Previous;
            }

        }
    }
}
