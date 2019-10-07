using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookRaycaster : MonoBehaviour
{
    [SerializeField]
    private PlayerInput playerInput;
    [SerializeField]
    private Transform grabTransform;

    [SerializeField]
    private LayerMask layer;
    private GameObject invertSymbol;
    private GameObject grabTip;
    private GameObject releaseTip;


    private Grabable grabable;
    private Grabable currentGrabable;
    private bool justGrabbed;

    private Invertable invertable;

    private void Start()
    {
        invertSymbol = GameObject.Find("InvertSymbol");
        grabTip = GameObject.Find("GrabTip");
        releaseTip = GameObject.Find("ReleaseTip");
    }
    private void Update()
    {
        justGrabbed = false;
        invertSymbol.SetActive(false);
        grabTip.SetActive(false);
        releaseTip.SetActive(false);

        RaycastHit hitInfo;
        if (Physics.Raycast(new Ray(transform.position, transform.forward), out hitInfo, 10000, layer))
        {
            invertable = hitInfo.transform.gameObject.GetComponent<Invertable>();
            grabable = hitInfo.transform.gameObject.GetComponent<Grabable>();
            if (invertable != null)
            {
                if (!playerInput.CoursorFree)
                {
                    invertSymbol.SetActive(true);
                }
                if (Input.GetKeyDown(KeyCode.Mouse0) && !playerInput.CoursorFree)
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
                grabTip.SetActive(true);
                if (Input.GetKeyDown(KeyCode.F) && !playerInput.CoursorFree)
                {
                    grabable.Grab(grabTransform, 2);
                    currentGrabable = grabable;
                    justGrabbed = true;
                }    
            }

        }

        if (currentGrabable != null)
        {
            releaseTip.SetActive(true);
        }


        if (!justGrabbed && Input.GetKeyDown(KeyCode.F) && currentGrabable != null)
        {
            currentGrabable.Release();
            currentGrabable = null;
        }

        if (currentGrabable != null && currentGrabable.Target != grabTransform)
        {
            currentGrabable = null;
        }

    }
}
