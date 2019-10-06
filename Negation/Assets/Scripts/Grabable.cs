using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabable : MonoBehaviour
{
    public Transform Target { get; set; }
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
            transform.position = Vector3.Lerp(transform.position, Target.position, 20 * Time.deltaTime);
        }
    }

    public void Grab(Transform newTarget)
    {
        if (isGrabbed) Release();
        rb.isKinematic = true;
        isGrabbed = true;
        Target = newTarget;
    }

    public void Release()
    {
        if (!isGrabbed) return;
        rb.isKinematic = false;
        isGrabbed = false;
    }
}
