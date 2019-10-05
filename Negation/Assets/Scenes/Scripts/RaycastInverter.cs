using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastInverter : MonoBehaviour
{
    Invertor invertor;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (invertor == null)
            {
                RaycastHit hitInfo;
                if (Physics.Raycast(new Ray(transform.position, transform.forward), out hitInfo))
                {
                    Invertable invertable = hitInfo.transform.gameObject.GetComponent<Invertable>();
                    if (invertable != null)
                    {
                        invertor = new Invertor();
                        invertor.Apply(invertable);
                    }

                }
            }
            else
            {
                invertor.Clear();
                invertor = null;
            }
        }
            
    }
}
