using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New tag group", menuName = "Tag group")]
public class TagGroup : ScriptableObject
{
    [SerializeField]
    private TagGroup parent;

    [SerializeField]
    private List<ConceptTag> myTags;

    public List<ConceptTag> GetTags(int nestingCount = 0)
    {
        List<ConceptTag> tags = new List<ConceptTag>();
        if (parent != null)
            if (nestingCount < 10)
                tags.AddRange(parent.GetTags(nestingCount + 1) as IEnumerable<ConceptTag>);
            else
                Debug.Log("TagGroups nesting is too deep!");
        tags.AddRange(myTags as IEnumerable<ConceptTag>);

        return tags;
    }
}
