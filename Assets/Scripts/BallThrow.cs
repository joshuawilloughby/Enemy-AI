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

    public AudioClip bounce;

    public int throwForce;
    public int throwHeight;

    void Start()
    {
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<AudioSource>().playOnAwake = false;
        GetComponent<AudioSource>().clip = bounce;
    }

    void OnCollisionEnter (Collision col)
    {
        Debug.Log("colide");
        if (col.gameObject.tag == "Floor")
        {
            GetComponent<AudioSource>().PlayOneShot(bounce);
        }
    }

    public void ReleaseMe()
    {
        this.transform.parent = null;

        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<SphereCollider>().enabled = true;
    }

    public void Throw()
    {
        rig.AddForce(parentBone.transform.forward + Camera.main.transform.forward * throwForce, ForceMode.Impulse);
        rig.AddForce(parentBone.transform.up + Camera.main.transform.up * throwHeight, ForceMode.Impulse);

        pickUp.isCarrying = false;

        Debug.Log("Add Force");
    } 
}
