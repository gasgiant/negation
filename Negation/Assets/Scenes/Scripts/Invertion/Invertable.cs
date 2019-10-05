using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invertable : MonoBehaviour
{
    [SerializeField]
    protected TagGroup tagGroup;

    protected int invertorsCount;
    protected List<ConceptTag> tags;

    public void AddInvertor()
    {
        invertorsCount++;
    }

    public void RemoveInvertor()
    {
        invertorsCount--;
        if (invertorsCount < 0) Debug.LogError("Invertor count is less then zero!");
    }

    public bool ContainsTag(ConceptTag tag)
    {
        return tags.Contains(tag);
    }

    public virtual void Resolve()
    {
        gameObject.SetActive(invertorsCount % 2 == 0);   
    }

    public virtual void Initialize()
    {
        tags = tagGroup.GetTags();
    }

    public virtual void Reset()
    {
        invertorsCount = 0;
    }
}
