using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField]
    private Transform lookTransform;
    [SerializeField] 
    private float mouseSensitivity;

    private PlayerInput playerInput;

    private float x;
    private float y;
    Vector3 euler;

    private void Awake()
    {
        x = lookTransform.eulerAngles.x;
        y = transform.eulerAngles.y;
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        if (playerInput.Active) return;
        x -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        y += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        x = ClampAngle(x, -90, 90);

        euler = lookTransform.eulerAngles;
        euler.x = x;
        lookTransform.rotation = Quaternion.Lerp(lookTransform.rotation, Quaternion.Euler(euler), 40 * Time.deltaTime);

        euler = transform.eulerAngles;
        euler.y = y;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(euler), 40 * Time.deltaTime);
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
        {
            angle += 360;
        }
        if (angle > 360)
        {
            angle -= 360;
        }
        return Mathf.Clamp(angle, min, max);
    }
}
