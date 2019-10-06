using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public bool Active { get; private set; }

    private GameObject crosshair;


    private void Start()
    {
        crosshair = GameObject.Find("Crosshair");
        SetCoursorActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            SetCoursorActive(true);
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            SetCoursorActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            InvertionManager.Instance.FreeSlot(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            InvertionManager.Instance.FreeSlot(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            InvertionManager.Instance.FreeSlot(2);
        }

    }

    public void SetCoursorActive(bool b)
    {
        Active = b;
        if (b)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            crosshair.SetActive(false);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            crosshair.SetActive(true);
            GameObject myEventSystem = GameObject.Find("EventSystem");
            myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
        }
    }
}
