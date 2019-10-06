using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabable : MonoBehaviour
{
    private Transform target;
    private Rigidbody rb;
    private bool isGrabbed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isGrabbed)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, 20 * Time.deltaTime);
        }
    }

    public void Grab(Transform newTarget)
    {
        if (isGrabbed) return;
        rb.isKinematic = true;
        isGrabbed = true;
        target = newTarget;
    }

    public void Release()
    {
        if (!isGrabbed) return;
        rb.isKinematic = false;
        isGrabbed = false;
    }
}
