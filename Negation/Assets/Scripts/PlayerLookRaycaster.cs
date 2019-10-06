using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookRaycaster : MonoBehaviour
{
    [SerializeField]
    private GameObject invertSymbol;
    [SerializeField]
    private Transform grabTransform;

    [SerializeField]
    private LayerMask layer;
    private Grabable grabable;
    private Grabable currentGrabable;
    private bool justGrabbed;

    private Invertable invertable;
    private void Update()
    {
        justGrabbed = false;
        invertSymbol.SetActive(false);

        RaycastHit hitInfo;
        if (Physics.Raycast(new Ray(transform.position, transform.forward), out hitInfo, 10000, layer))
        {
            invertable = hitInfo.transform.gameObject.GetComponent<Invertable>();
            grabable = hitInfo.transform.gameObject.GetComponent<Grabable>();
            if (invertable != null)
            {
                invertSymbol.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    InvertorSlot slot = InvertionManager.Instance.GetFreeInvertorSlot();
                    if (slot != null)
                    {
                        Invertor invertor = new Invertor();
                        slot.TakeSlot(invertor, invertor.Apply(invertable));
                    }
                }
            }

            if (grabable != null && currentGrabable == null)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    grabable.Grab(grabTransform);
                    currentGrabable = grabable;
                    justGrabbed = true;
                }    
            }

        }

        if (!justGrabbed && Input.GetKeyDown(KeyCode.F) && currentGrabable != null)
        {
            currentGrabable.Release();
            currentGrabable = null;
        }



    }
}
