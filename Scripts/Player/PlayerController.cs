using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    [Header("References")]
    [SerializeField] private InputReader inputReader;
    //[SerializeField] private Transform bodyTransform;
    [SerializeField] private Rigidbody2D myRigidbody;
    [SerializeField] private Animator myAnimator;

    [Header("Settings")]
    [SerializeField] private float movementSpeed = 3f;

    public Vector2 LastDirection;

    private Vector2 previousMovementInput;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) {  return; }
        inputReader.MoveEvent += HandleMove;

        LastDirection = -1 * transform.up;
    }
    public override void OnNetworkDespawn()
    {
        if(!IsOwner) { return; }
        inputReader.MoveEvent -= HandleMove;
    }

    private void FixedUpdate()
    {
        if(!IsOwner) { return; }

        if(!myAnimator.GetBool("isLaunching"))
        {
            myRigidbody.velocity = previousMovementInput * movementSpeed;
        } else
        {
            myRigidbody.velocity = Vector3.zero;
        }

        if (myRigidbody.velocity.magnitude > 0.0001)
        {
            myAnimator.SetBool("isMoving", true);
            Vector2 moveInput = myRigidbody.velocity.normalized;
            myAnimator.SetFloat("moveX", moveInput.x);
            myAnimator.SetFloat("moveY", moveInput.y);
        }else
        {
            if(!myAnimator.GetBool("isLaunching"))
            {
                myAnimator.SetBool("isMoving", false);
            }
        }
        //myAnimator.SetFloat("Horizontal", myRigidbody.velocity.x);
        //myAnimator.SetFloat("Vertical", myRigidbody.velocity.y);
        //myAnimator.SetFloat("Speed", moveInput.sqrMagnitude);
    }       

    private void HandleMove(Vector2 movementInput)
    {
        previousMovementInput = movementInput;
        if(movementInput.magnitude > 0.1)
        {
            LastDirection = movementInput;
            Vector2 moveInput = movementInput.normalized;
            myAnimator.SetFloat("aimX", moveInput.x);
            myAnimator.SetFloat("aimY", moveInput.y);
        }
    }

}
