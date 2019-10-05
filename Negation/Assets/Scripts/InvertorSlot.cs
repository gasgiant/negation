using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InvertorSlot : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    public bool isFree;
    private Invertor invertor;

    private void OnEnable()
    {
        isFree = true;
        text.text = "";
    }

    public void TakeSlot(Invertor newInvertor, string name)
    {
        text.text = name;
        invertor = newInvertor;
        isFree = false;
    }

    public void FreeSlot()
    {
        if (!isFree)
        {
            invertor.Clear();
            text.text = "";
            isFree = true;
        }
    }
}
