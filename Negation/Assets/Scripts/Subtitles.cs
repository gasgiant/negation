using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Subtitles : MonoBehaviour
{
    [SerializeField]
    private GameObject titlesObject;

    [SerializeField]
    private TextMeshProUGUI text;

    public void SetSubtitlesText(string line)
    {
        text.text = line;
    }

    public void SetActive(bool b)
    {
        titlesObject.SetActive(b);
    }
}
