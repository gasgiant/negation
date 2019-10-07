using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TagLock : MonoBehaviour
{
    [SerializeField]
    bool reversMode;
    [SerializeField]
    bool openWhenHaveObject;
    [SerializeField]
    private List<string> requiredTags;
    [SerializeField]
    private GameObject lockObject;
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
        if (!reversMode)
            CheckColliders(Physics.OverlapSphere(transform.position, 1));
        else
        {
            if (openWhenHaveObject)
            {
                SetOpen(lockObject.activeInHierarchy);
            }
            else
            {
                SetOpen(!lockObject.activeInHierarchy);
            }
        }
            
    }

    private void CheckColliders(Collider[] colliders)
    {
        Invertable invertable;
        Grabable grabable = null;
        bool check = false;
        foreach (var collider in colliders)
        {
            invertable = collider.gameObject.GetComponent<Invertable>();
            grabable = collider.gameObject.GetComponent<Grabable>();
            if (invertable != null && CheckTags(invertable.GetTags()))
            {
                check = true;
                break;
            }
        }

        SetOpen(check, grabable);
    }

    private void SetOpen(bool check, Grabable grabable = null)
    {
        if (!reversMode)
        {
            if (check)
            {
                lockObject.SetActive(false);
                lockGlow.material = greenMat;
                lineRenderer.startColor = Color.green;
                lineRenderer.endColor = Color.green;
                text.text = "";
                if (grabable != null) grabable.Grab(transform, 0);
            }
            else
            {
                lockObject.SetActive(true);
                lockGlow.material = redMat;
                lineRenderer.startColor = Color.red;
                lineRenderer.endColor = Color.red;
                text.text = textRequirement;
            }
        }
        else
        {
            if (check)
            {
                lockGlow.material = greenMat;
                lineRenderer.startColor = Color.green;
                lineRenderer.endColor = Color.green;
                text.text = "";
            }
            else
            {
                lockGlow.material = redMat;
                lineRenderer.startColor = Color.red;
                lineRenderer.endColor = Color.red;
                if (openWhenHaveObject)
                    text.text = "OBJECT\nMISSING";
                else
                    text.text = "REMOVE\nOBJECT";
            }
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
