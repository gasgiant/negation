using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotTeapotInvertable : Invertable
{
    Camera cam;
    LayerMask normalMask;
    public LayerMask onlyTeapotMask;

    public override void Initialize()
    {
        base.Initialize();
        cam = FindObjectOfType<Camera>();
        normalMask = cam.cullingMask;
    }
    public override void Resolve()
    {
        if (invertorsCount % 2 == 0)
        {
            cam.cullingMask = normalMask;
        }
        else
        {
            cam.cullingMask = onlyTeapotMask;
        }
    }
}
