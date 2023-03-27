using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magneticprop : MonoBehaviour
{
    public float forcefactor = 200f;
    List<Rigidbody> rgballs = new List<Rigidbody>();
    Transform magnetpoint;
    // Start is called before the first frame update
    void Start()
    {
        magnetpoint = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        foreach(Rigidbody rgbballs in rgballs)
        {
            rgbballs.AddForce((magnetpoint.position-rgbballs.position)*forcefactor*Time.deltaTime);
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("point"))
            rgballs.Add(other.GetComponent<Rigidbody>());

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("point"))
            rgballs.Remove(other.GetComponent<Rigidbody>());
    }
}
