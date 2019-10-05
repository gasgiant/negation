using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invertor
{
    private List<Invertable> invertables;

    public Invertor()
    {
        invertables = new List<Invertable>();
    }

    public string Apply(Invertable invertable)
    {
        invertables.Add(invertable);
        invertable.AddInvertor();
        InvertionManager.Instance.ResolveAllInvertables();
        return invertable.gameObject.name;
    }

    public string Apply(string tag, Invertable[] allInvertables)
    {
        foreach (var invertable in allInvertables)
        {
            if (invertable.ContainsTag(tag))
            {
                invertables.Add(invertable);
                invertable.AddInvertor();
            }
        }
        InvertionManager.Instance.ResolveAllInvertables();
        return tag;
    }

    public void Clear()
    {
        foreach (var invertable in invertables)
        {
            invertable.RemoveInvertor();
        }
        invertables.Clear();
        InvertionManager.Instance.ResolveAllInvertables();
    }
}
