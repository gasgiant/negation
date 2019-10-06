using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConceptHolder : MonoBehaviour
{
    [SerializeField]
    string conceptTag;
    [SerializeField]
    TextMeshPro text;

    private void OnEnable()
    {
        text.text = conceptTag;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InvertionManager.Instance.AddAvailableTag(conceptTag);
            gameObject.SetActive(false);
        }
    }
}
