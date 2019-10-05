using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField]
    private Transform lookTransform;
    [SerializeField] 
    private float mouseSensitivity;

    private float x;
    private float y;
    Vector3 euler;

    bool active;

    private void Awake()
    {
        x = lookTransform.eulerAngles.x;
        y = transform.eulerAngles.y;
    }


    public void ToggleActive()
    {
        if (active)
        {
            Cursor.lockState = CursorLockMode.None;
            
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            GameObject myEventSystem = GameObject.Find("EventSystem");
            myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
        }
        active = !active;
    }

    void Update()
    {
        if (!active) return;
        x -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        y += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        x = ClampAngle(x, -90, 90);

        euler = lookTransform.eulerAngles;
        euler.x = x;
        lookTransform.eulerAngles = euler;

        euler = transform.eulerAngles;
        euler.y = y;
        transform.eulerAngles = euler;
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
