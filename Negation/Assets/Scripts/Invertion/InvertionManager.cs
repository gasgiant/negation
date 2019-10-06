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
    private Dictionary<string, TagButton> tagButtons;

    private RectTransform buttonsParent;
    private Vector3 tagButtonsTopPosition;
    private bool isTagButtonsTopPositionSet;

    private void Awake()
    {
        Instance = this;
        invertables = FindObjectsOfType<Invertable>();

        foreach (var invertable in invertables)
        {
            invertable.Initialize();
        }
        tagButtons = new Dictionary<string, TagButton>();
        buttonsParent = GameObject.Find("Content").GetComponent<RectTransform>();
        availableTags = new List<string>();

        tagButtons = new Dictionary<string, TagButton>();

        AddAvailableTag("CUBE");
        AddAvailableTag("SPHERE");
        AddAvailableTag("RED");
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
        if (!availableTags.Contains(tag))
        {
            availableTags.Add(tag);
            TagButton button = Instantiate(tagButtonPrefab);
            tagButtons.Add(tag, button);
            button.SetTag(tag);
            button.transform.SetParent(buttonsParent, false);
            if (!isTagButtonsTopPositionSet) tagButtonsTopPosition = button.GetComponent<RectTransform>().position;
            buttonsParent.sizeDelta = buttonsParent.sizeDelta + Vector2.up * button.Height;

            availableTags.Sort();
            for (int i = 0; i < availableTags.Count; i++)
            {
                tagButtons[availableTags[i]].GetComponent<RectTransform>().position = tagButtonsTopPosition + Vector3.down * button.Height * i;
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
