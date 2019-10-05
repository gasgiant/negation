using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    CharacterController characterController;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 input = transform.right * Input.GetAxisRaw("Horizontal") + transform.forward * Input.GetAxisRaw("Vertical");
        characterController.SimpleMove(input.normalized * speed);
    }
}
