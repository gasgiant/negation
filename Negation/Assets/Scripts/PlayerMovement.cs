﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] 
    private float speed;
    [SerializeField]
    private float jumpMultiplier;
    [SerializeField]
    private AnimationCurve jumpFallOff;

    private Vector3 lastPositionOnGround;
    private bool isJumping;

    private Vector3 prevStepPosition;

    private CharacterController characterController;
    void Start()
    {
        prevStepPosition = transform.position;
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 input = transform.right * Input.GetAxisRaw("Horizontal") + transform.forward * Input.GetAxisRaw("Vertical");
        if (Physics.gravity.magnitude > 0.1f)
        {
            
            if (Input.GetKey(KeyCode.LeftShift))
                characterController.SimpleMove(input.normalized * speed * 1.8f);
            else
                characterController.SimpleMove(input.normalized * speed);
        }
        else
        {
            characterController.Move(input.normalized * speed * Time.deltaTime);
        }

        if (characterController.isGrounded && (prevStepPosition - transform.position).magnitude > 0.3)
        {
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlaySound("Steps");
            prevStepPosition = transform.position;
        }

        JumpInput();
        if (characterController.isGrounded)
        {
            lastPositionOnGround = transform.position;
        }
    }

    private void JumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping && Physics.gravity.magnitude > 0.1f)
        {
            isJumping = true;
            StartCoroutine(JumpEvent(0.3f));
        }
    }

    private IEnumerator JumpEvent(float duration)
    {
        characterController.slopeLimit = 90.0f;
        float timeInAir = 0.0f;

        do
        {
            float jumpForce = jumpFallOff.Evaluate(timeInAir);
            characterController.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime);
            timeInAir += Time.deltaTime / duration;
            yield return null;
        } while (!characterController.isGrounded && characterController.collisionFlags != CollisionFlags.Above);

        characterController.slopeLimit = 45.0f;
        isJumping = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FallDeath"))
        {
            characterController.enabled = false;
            transform.position = lastPositionOnGround;
            characterController.enabled = true;
        }

        if (other.CompareTag("Death") && !GameManager.Instance.isPlayerDead)
        {
            characterController.enabled = false;
            transform.position -= Vector3.up;
        }
    }
}
