using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Transform theDest;

    public bool isCarrying;
    public bool playThrow;

    void Start()
    {
        isCarrying = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isCarrying)
        {
            playThrow = true;
            OnPickup();
            Debug.Log(isCarrying);
        }

        
    }
    void OnPickup()
    {
        GetComponent<SphereCollider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
        this.transform.position = theDest.position;
        this.transform.parent = GameObject.Find("Destination").transform;

        isCarrying = true;
    }

    /*void OnMouseUp()
    {
        this.transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<SphereCollider>().enabled = true;
    }
    */
}
