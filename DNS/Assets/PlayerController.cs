using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Input Field")] [SerializeField]
    private Inputs inputsAsset;
    [SerializeField] private InputAction move;

    [Header("RigidBody")] 
    [SerializeField] private Rigidbody rb;

    [Header("Forces & Speed")] [SerializeField]
    private float movementForce;
    [SerializeField] private float jumpForce;
    [SerializeField] private float maxSpeed;
    private Vector3 forceDirection = Vector3.zero;

    
    [Header("Camera")]
    [SerializeField]private Camera playerCam;


    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        inputsAsset = new Inputs();
    }

    private void OnEnable()
    {
        inputsAsset.Player.Jump.started += DoJump;
        move = inputsAsset.Player.Move;
        inputsAsset.Player.Enable();
    }
    private void OnDisable()
    {
        inputsAsset.Player.Jump.started -= DoJump;
        inputsAsset.Player.Disable();
    }

    private void FixedUpdate()
    {
        forceDirection += move.ReadValue<Vector2>().x * GetCameraRight(playerCam) *movementForce;
        forceDirection += move.ReadValue<Vector2>().y * GetCameraForward(playerCam)* movementForce;
        
        rb.AddForce(forceDirection,ForceMode.Impulse);
        forceDirection = Vector3.zero;

        //fall faster to reduce "floating" effect
        if (rb.velocity.y < 0)
        {
            rb.velocity-= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime;
        }

        
        //lmit horizontal velocity
        Vector3 horizontalVelocity = rb.velocity;
        horizontalVelocity.y = 0;
        if (horizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed)
        {
            rb.velocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * rb.velocity.y;
        }
    }

    private Vector3 GetCameraRight(Camera playerCamera)
    {
        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    private Vector3 GetCameraForward(Camera playerCamera)
    {
        Vector3 right = playerCamera.transform.right;
        right.y = 0;
        return right.normalized;
    }

    private void DoJump(InputAction.CallbackContext context)
    {
        if (IsGrounded())
        {
            forceDirection += Vector3.up * jumpForce;
        }
    }

    private bool IsGrounded()
    {
        Ray ray = new Ray(this.transform.position + Vector3.up *0.25f, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit, 0.3f))
        {
            return true;
        }
        else return false;
    }
}


