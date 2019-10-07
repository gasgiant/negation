using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityInvertable : Invertable
{
    Vector3 baseGravity;
    public override void Initialize()
    {
        base.Initialize();
        baseGravity = Physics.gravity;
    }
    public override void Resolve()
    {
        if (invertorsCount % 2 == 0)
        {
            Physics.gravity = baseGravity;
        }
        else
        {
            Physics.gravity = Vector3.zero;
        }
    }
}
