using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private PlayerLook playerLook;

    private void Start()
    {
        playerLook.ToggleActive();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            playerLook.ToggleActive();
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
}
