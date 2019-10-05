using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastInvertStarter : MonoBehaviour
{
    [SerializeField]
    private GameObject invertSymbol;

    [SerializeField]
    private LayerMask layer;
    private void Update()
    {
        invertSymbol.SetActive(false);

        RaycastHit hitInfo;
        if (Physics.Raycast(new Ray(transform.position, transform.forward), out hitInfo, 10000, layer))
        {
            Invertable invertable = hitInfo.transform.gameObject.GetComponent<Invertable>();
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

        }
            
    }
}
