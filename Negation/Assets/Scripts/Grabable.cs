using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabable : MonoBehaviour
{
    public Transform Target { get; set; }
    private Rigidbody rb;
    private new Collider collider;
    private bool isGrabbed;
    private int grabPriority;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    private void Update()
    {
        if (isGrabbed)
        {
            transform.position = Vector3.Lerp(transform.position, Target.position, 20 * Time.deltaTime);
        }
    }

    public void Grab(Transform newTarget, int priority)
    {
        if (isGrabbed)
        {
            if (priority > grabPriority)
                Release();
            else
                return;
        }
        grabPriority = priority;
        rb.isKinematic = true;
        collider.isTrigger = true;
        isGrabbed = true;
        Target = newTarget;
    }

    public void Release()
    {
        if (!isGrabbed) return;
        grabPriority = 0;
        rb.isKinematic = false;
        collider.isTrigger = false;
        isGrabbed = false;
    }
}
