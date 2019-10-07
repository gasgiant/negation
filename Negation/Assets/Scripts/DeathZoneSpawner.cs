using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZoneSpawner : MonoBehaviour
{
    public GameObject deathZone;
    public string tagToCheck;

    private void OnTriggerEnter(Collider other)
    {
        Invertable invertable = other.gameObject.GetComponent<Invertable>();
        if (invertable != null && invertable.ContainsTag(tagToCheck))
        {
            deathZone.SetActive(true);
            Invoke("DisableZone", 0.5f);
        }
    }

    void DisableZone()
    {
        deathZone.SetActive(false);
    }
}
