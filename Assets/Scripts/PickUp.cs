using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameObject player;
    public Transform destination;

    public bool isCarrying;
    public bool playThrow;

    public Vector3 originalScale;

    void Start()
    {
        isCarrying = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isCarrying)
        {
            GetComponent<SphereCollider>().enabled = false;
            GetComponent<Rigidbody>().useGravity = false;

            this.transform.position = destination.position;
            this.transform.parent = GameObject.Find("Destination").transform;

            isCarrying = true;
            Debug.Log(isCarrying);
        }

        if (Input.GetMouseButtonDown(0) && isCarrying)
        {
            playThrow = true;
        }
    }
}
