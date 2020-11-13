using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallThrow : MonoBehaviour
{
    public GameObject parentBone;
    public Rigidbody rig;
    public Vector3 lastPos;
    public Vector3 curVel;

    void Start()
    {
        transform.parent = parentBone.transform;
        rig.useGravity = false;
    }

    public void ReleaseMe()
    {
        transform.parent = null;

        rig.useGravity = true;
        transform.rotation = parentBone.transform.rotation;
        rig.AddForce(transform.forward * 20000);
    }
}
