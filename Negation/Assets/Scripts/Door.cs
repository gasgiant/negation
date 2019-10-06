using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private List<Lock> locks;
    private new Collider collider;
    private Animator animator;

    private void Start()
    {
        collider = GetComponent<Collider>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        collider.enabled = IsClosed();
        animator.SetBool("Opened", !collider.enabled);
    }

    public bool IsClosed()
    {
        foreach (var item in locks)
        {
            if (!item.gameObject.activeInHierarchy) return false;
        }
        return true;
    }
}
