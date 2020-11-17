using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameObject player;
    public Transform destination;

    public PlayerMovement playerMovement;

    public bool isCarrying;
    public bool playPickUp;
    public bool playThrow;

    public Vector3 originalScale;

    void Start()
    {
        playPickUp = false;
        isCarrying = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isCarrying)
        {
            GetComponent<SphereCollider>().enabled = false;
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

            playPickUp = true;

            playerMovement.stopMovement();

            Debug.Log(isCarrying);
        }

        if (Input.GetMouseButtonDown(0) && isCarrying)
        {
            playThrow = true;
        }
    }

    public void PickingUp()
    {
        this.transform.position = destination.position;
        this.transform.parent = GameObject.Find("Destination").transform;
        isCarrying = true;
    }

    public void UnFreeze()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }    
}
