using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New speech", menuName = "Speech")]
public class Speech : ScriptableObject
{
    public AudioClip clip;
    public List<Line> lines;
}

[System.Serializable]
public class Line
{
    public float timeCode;
    [TextArea]
    public string text;
}
