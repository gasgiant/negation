using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertionManager : MonoBehaviour
{
    public static InvertionManager Instance;

    private Invertable[] invertables;

    Invertor invertor;

    private void Awake()
    {
        Instance = this;
        invertables = FindObjectsOfType<Invertable>();

        foreach (var invertable in invertables)
        {
            invertable.Initialize();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (invertor == null)
            {
                invertor = new Invertor();
                invertor.Apply(ConceptTag.Cube, invertables);
            }
            else
            {
                invertor.Clear();
                invertor = null;
            }
        }
    }

    public void ResolveAllInvertables()
    {
        foreach (var invertable in invertables)
        {
            invertable.Resolve();
        }
    }
}
