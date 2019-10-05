using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invertable : MonoBehaviour
{
    [SerializeField]
    private Invertable parent;

    [SerializeField]
    private List<string> myTags;

    public List<string> GetTags(int nestingCount = 0)
    {
        List<string> tags = new List<string>();
        if (parent != null)
            if (nestingCount < 10)
                tags.AddRange(parent.GetTags(nestingCount + 1) as IEnumerable<string>);
            else
                Debug.Log("TagGroups nesting is too deep!");
        tags.AddRange(myTags as IEnumerable<string>);

        return tags;
    }

    protected int invertorsCount;
    protected List<string> tags;

    public void AddInvertor()
    {
        invertorsCount++;
    }

    public void RemoveInvertor()
    {
        invertorsCount--;
        if (invertorsCount < 0) Debug.LogError("Invertor count is less then zero!");
    }

    public bool ContainsTag(string tag)
    {
        return tags.Contains(tag);
    }

    public virtual void Resolve()
    {
        gameObject.SetActive(invertorsCount % 2 == 0);   
    }

    public virtual void Initialize()
    {
        tags = GetTags();
    }

    public virtual void Reset()
    {
        invertorsCount = 0;
    }
}
