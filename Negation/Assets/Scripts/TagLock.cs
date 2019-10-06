using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TagLock : MonoBehaviour
{
    [SerializeField]
    private List<string> requiredTags;
    [SerializeField]
    private Lock lockObject;
    [SerializeField]
    private Renderer lockGlow;
    [SerializeField]
    private LineRenderer lineRenderer;
    [SerializeField]
    private TextMeshPro text;

    [SerializeField]
    private Material redMat;
    [SerializeField]
    private Material greenMat;

    private string textRequirement;

    private void Start()
    {
        text.text = "Place here: \n";
        foreach (var item in requiredTags)
        {
            text.text += item + "\n";
        }
        textRequirement = text.text;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, transform.position + Vector3.down);
        lineRenderer.SetPosition(1, lockGlow.transform.position);
        lockGlow.material = redMat;
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
    }

    private void Update()
    {
        CheckColliders(Physics.OverlapSphere(transform.position, 1));
    }

    private void CheckColliders(Collider[] colliders)
    {
        Invertable invertable;
        bool check = false;
        foreach (var collider in colliders)
        {
            invertable = collider.gameObject.GetComponent<Invertable>();
            if (invertable != null && CheckTags(invertable.GetTags()))
            {
                check = true;
                break;
            }
        }

        if (check)
        {
            lockObject.gameObject.SetActive(false);
            lockGlow.material = greenMat;
            lineRenderer.startColor = Color.green;
            lineRenderer.endColor = Color.green;
            text.text = "";
        }
        else
        {
            lockObject.gameObject.SetActive(true);
            lockGlow.material = redMat;
            lineRenderer.startColor = Color.red;
            lineRenderer.endColor = Color.red;
            text.text = textRequirement;
        }
    }

    private bool CheckTags(List<string> tags)
    {
        foreach (var item in requiredTags)
        {
            if (!tags.Contains(item)) return false;
        }
        return true;
    }


}
