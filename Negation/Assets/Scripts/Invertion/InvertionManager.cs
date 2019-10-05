using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertionManager : MonoBehaviour
{
    public static InvertionManager Instance;

    private Invertable[] invertables;

    [SerializeField]
    private TagButton tagButtonPrefab;

    [SerializeField]
    private List<InvertorSlot> invertorSlots;
    private List<string> availableTags;

    private RectTransform buttonsParent;

    private void Awake()
    {
        Instance = this;
        invertables = FindObjectsOfType<Invertable>();

        foreach (var invertable in invertables)
        {
            invertable.Initialize();
        }

        buttonsParent = GameObject.Find("Content").GetComponent<RectTransform>();
        availableTags = new List<string>();

        AddAvailableTag("Cube");
        AddAvailableTag("Sphere");
        AddAvailableTag("Red");
    }

    public InvertorSlot GetFreeInvertorSlot()
    {
        foreach (var item in invertorSlots)
        {
            if (item.isFree)
                return item;
        }
        return null;
    }

    public void FreeSlot(int index)
    {
        invertorSlots[index].FreeSlot();
    }

    public void AddTagInverter(string tag)
    {
        InvertorSlot slot = GetFreeInvertorSlot();
        if (slot != null)
        {
            Invertor invertor = new Invertor();
            slot.TakeSlot(invertor, invertor.Apply(tag, invertables));
        }
    }

    public void AddAvailableTag(string tag)
    {
        availableTags.Add(tag);
        TagButton button = Instantiate<TagButton>(tagButtonPrefab);
        button.SetTag(tag);
        button.transform.SetParent(buttonsParent, false);
        button.GetComponent<RectTransform>().position += Vector3.down * button.Height * (availableTags.Count - 1);
        buttonsParent.sizeDelta = buttonsParent.sizeDelta + Vector2.up * button.Height;
    }


    public void ResolveAllInvertables()
    {
        foreach (var invertable in invertables)
        {
            invertable.Resolve();
        }
    }

}
