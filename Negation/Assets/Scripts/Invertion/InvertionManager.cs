﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertionManager : MonoBehaviour
{
    public static InvertionManager Instance;

    private Invertable[] invertables;

    [SerializeField]
    private List<string> initialTagsAvailable;

    [SerializeField]
    private TagButton tagButtonPrefab;

    [SerializeField]
    private List<InvertorSlot> invertorSlots;
    private List<string> availableTags;
    private List<string> activeTags;
    private List<string> activeTagsAddedPyPostProcess;
    private List<Invertor> activeTagInvertors;
    private Dictionary<string, TagButton> tagButtons;

    private RectTransform buttonsParent;
    private Vector3 tagButtonsTopPosition;
    private bool isTagButtonsTopPositionSet;
    private int postProcessRecursCount;

    private int activeTagButtonIndex = -1;


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
        activeTagInvertors = new List<Invertor>();
        activeTags = new List<string>();

        tagButtons = new Dictionary<string, TagButton>();

        foreach (var item in initialTagsAvailable)
        {
            AddAvailableTag(item);
        }
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

    public void RegisterInvertor(string tag, Invertor invertor)
    {
        activeTagInvertors.Add(invertor);
        activeTags.Add(tag);

        if (activeTags.Contains("NOT TEAPOT")  && activeTags.Contains("TEAPOT"))
        {
            GameManager.Instance.LoadLevel("Nothingness");
        }

        if (postProcessRecursCount < 3)
            PostProcessInvertors();
        else
           Debug.Log("Recursion too deep in tag post process!");
    }

    public void UnregisterInvertor(string tag, Invertor invertor)
    {
        if (activeTagInvertors.Contains(invertor))
        {
            if (activeTags.Contains(tag))
            {
                activeTags.Remove(tag);
            }
            activeTagInvertors.Remove(invertor);
            if (postProcessRecursCount < 3)
                PostProcessInvertors();
            else
                Debug.Log("Recursion too deep in tag post process!");
        }
        
    }

    private void PostProcessInvertors()
    {
        List<string> conditions = new List<string>()
        {
            "RED",
            "BLUE"
        };

        if (CheckHaveEveryTag(conditions) && !activeTags.Contains("MAGENTA"))
        {
            Invertor invertor = new Invertor();
            invertor.Apply("MAGENTA", invertables, true, conditions);
            postProcessRecursCount += 1;
        }

        postProcessRecursCount -= 1;
        
        List<Invertor> invertorsToClear = new List<Invertor>();

        foreach (var invertor in activeTagInvertors)
        {
            if (invertor.AddedByPostprocessor)
            {
                if (!CheckHaveEveryTag(invertor.conditionsToStay))
                {
                    invertorsToClear.Add(invertor);
                }
            }
        }

        foreach (var invertor in invertorsToClear)
        {
            invertor.Clear();
        }
    }

    private bool CheckHaveEveryTag(List<string> conditions)
    {
        foreach (var tag in conditions)
        {
            if (!activeTags.Contains(tag))
            {
                return false;
            }
        }
        return true;
    }

    public void AddTagInvertor(string tag)
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
            if (activeTagButtonIndex < 0)
            {
                activeTagButtonIndex = 0;
                tagButtons[availableTags[activeTagButtonIndex]].SetAsActive(true);
            }
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

    private void Update()
    {
        if (availableTags.Count > 0)
        {
            if (Mathf.Abs(Input.mouseScrollDelta.y) > 0.1f || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Q))
            {

                if (Input.mouseScrollDelta.y > 0 || Input.GetKeyDown(KeyCode.Q))
                {
                    activeTagButtonIndex -= 1;
                }
                else
                {
                    if (Input.mouseScrollDelta.y < 0 || Input.GetKeyDown(KeyCode.E))
                    {
                        activeTagButtonIndex += 1;
                    }
                }

                if (activeTagButtonIndex > availableTags.Count - 1)
                    activeTagButtonIndex = availableTags.Count - 1;
                if (activeTagButtonIndex < 0)
                    activeTagButtonIndex = 0;


                for (int i = 0; i < availableTags.Count; i++)
                {
                    tagButtons[availableTags[i]].SetAsActive(i == activeTagButtonIndex);
                }


                
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                tagButtons[availableTags[activeTagButtonIndex]].StartInvert();
            }

        }  
    }

    public void ResolveAllInvertables()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySound("Invert");
        foreach (var invertable in invertables)
        {
            invertable.Resolve();
        }
    }

}
