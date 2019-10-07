using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public bool CoursorFree { get; private set; }
    private bool justChanged;
    private GameObject crosshair;


    private void Start()
    {
        crosshair = GameObject.Find("Crosshair");
        SetCoursorFree(false);
    }

    void Update()
    {
        //if (CoursorFree && (Input.GetKeyDown(KeyCode.Mouse1))) //|| Input.GetKeyDown(KeyCode.Mouse0)))
        //{
        //    SetCoursorFree(false);
        //    justChanged = true;
        //}

        //if (!justChanged && Input.GetKeyDown(KeyCode.Mouse1))
        //{
        //    SetCoursorFree(true);
        //}
        justChanged = false;



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

    public void SetCoursorFree(bool b)
    {
        CoursorFree = b;
        if (b)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            crosshair.SetActive(false);
        }
        else
        {
            
            crosshair.SetActive(true);
            LockCoursor();
            //Invoke("LockCoursor", 0.25f);
        }
    }

    public void LockCoursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameObject myEventSystem = GameObject.Find("EventSystem");
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }
}
