using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeathScreen : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private GameObject panel;

    public void SetText(string reason)
    {
        text.text = "KILLED BY: " + reason;
        panel.SetActive(true);
    }
}
