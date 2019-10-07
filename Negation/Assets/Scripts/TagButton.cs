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
    [SerializeField]
    private Image image;
    [SerializeField]
    Color activeColor;
    Color normalColor;
    private TextMeshProUGUI text;

    public float Height { get { return rectTransform.rect.height; } }

    private void OnEnable()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        normalColor = image.color;
    }

    public void SetTag(string tag)
    {
        conceptTag = tag;
        text.text = tag;
    }

    public void SetAsActive(bool b)
    {
        image.color = b ? activeColor : normalColor;
    }

    public void StartInvert()
    {
        InvertionManager.Instance.AddTagInvertor(conceptTag);
    }
}
