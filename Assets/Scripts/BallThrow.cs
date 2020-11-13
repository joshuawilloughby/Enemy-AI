using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallThrow : MonoBehaviour
{
    public GameObject parentBone;
    public GameObject playerCamera;

    public GameObject ball;

    public Rigidbody rig;

    public PickUp pickUp;

    public int throwForce = 5000;
   // public throwDirection 

    void Start()
    {
        GetComponent<Rigidbody>().useGravity = true;
    }

    public void ReleaseMe()
    {
        this.transform.parent = null;

        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<SphereCollider>().enabled = true;
    }

    public void Throw()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * throwForce);
        pickUp.isCarrying = false;

        Debug.Log("Add Force");
    } 
}
