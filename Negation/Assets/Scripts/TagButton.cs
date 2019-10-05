using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TagButton : MonoBehaviour
{
    public string conceptTag;
    [SerializeField]
    private RectTransform rectTransform;
    private TextMeshProUGUI text;

    public float Height { get { return rectTransform.rect.height; } }

    private void OnEnable()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetTag(string tag)
    {
        conceptTag = tag;
        text.text = tag;
    }

    public void StartInvert()
    {
        InvertionManager.Instance.AddTagInverter(conceptTag);
    }
}
