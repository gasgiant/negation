using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField]
    private string deathSourse;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !GameManager.Instance.isPlayerDead)
        {
            GameManager.Instance.OnDeath(deathSourse);
            
        }
    }
}
