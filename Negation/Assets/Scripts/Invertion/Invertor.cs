using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invertor
{
    public string ConceptTag { private set; get; }
    public bool AddedByPostprocessor { private set; get; }

    public List<string> conditionsToStay { private set; get; }

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

    public string Apply(string tag, Invertable[] allInvertables, bool isPostProcess = false, List<string> conditions = null)
    {
        foreach (var invertable in allInvertables)
        {
            if (invertable.ContainsTag(tag))
            {
                invertables.Add(invertable);
                invertable.AddInvertor();
            }
        }
        AddedByPostprocessor = isPostProcess;
        conditionsToStay = conditions;
        ConceptTag = tag;
        InvertionManager.Instance.RegisterInvertor(tag, this);
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
        if (ConceptTag != null)
            InvertionManager.Instance.UnregisterInvertor(ConceptTag, this);

        ConceptTag = null;
        InvertionManager.Instance.ResolveAllInvertables();
    }
}
