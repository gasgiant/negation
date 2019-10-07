using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Transform target;
    [SerializeField]
    float smoothTime = 0.05f;
    [SerializeField]
    float maxDistance;
    Vector3 vel;
    void Start()
    {
        transform.position = target.position;
        transform.rotation = target.rotation;
    }

    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, target.position, ref vel, smoothTime);
        //if ((transform.position - target.position).magnitude > maxDistance)
        //    transform.position = (transform.position - target.position).normalized * maxDistance + target.position;
        transform.rotation = target.rotation;
    }
}
