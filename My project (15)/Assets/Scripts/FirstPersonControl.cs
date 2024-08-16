using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonControl : MonoBehaviour
{
    [ Header("MOVEMENT SETTINGS")]
    [Space(5)]
    public float moveSpeed;
    public float lookSpeed;
    public GameObject Player;
    public float gravity = -9.81f;
    public float jumpHeight = 1.0f;
    public Transform playerCamera;

    private Vector2 moveInput;
    private Vector2 lookInput;
    private float verticalLookRotation = 0f;
    private Vector3 velocity;

    private CharacterController characterController;

    [Header("SHOOTING SETTINGS")]
    [Space(5)]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 20f;
    public float pickUpRange = 3f;
    private bool holdingGun = false;


    [Header("PICKING UP SETTINGS")]
    [Space(5)]
    public Transform holdPosition;
    private GameObject heldObject;


    [Header("CROUCH SETTINGS")]
    [Space(5)]
    public float crouchHeight = 1.0f;
    public float standingHeight = 2.0f;
    public float crouchSpeed = 2.0f;


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

   private Controls playerInput;

private void OnEnable()
{
        var playerInput = new Controls();

        playerInput.Player.Enable();

        playerInput.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        playerInput.Player.Movement.canceled += ctx => moveInput = Vector2.zero;

        playerInput.Player.LookAround.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        playerInput.Player.LookAround.canceled += ctx => lookInput = Vector2.zero;

        playerInput.Player.Jump.performed += ctx => Jump();

        playerInput.Player.Shoot.performed += ctx => Shoot();
        playerInput.Player.PickUp.performed += ctx => PickUpObject();

        
        playerInput.Player.Crouch.performed += ctx => ToggleCrouch();

    }

    private bool isCrouching = false;


    public void ToggleCrouch()
    {
        isCrouching = !isCrouching;

        if (isCrouching)
        {
            characterController.height = crouchHeight;
            moveSpeed /= crouchSpeed; // Decrease move speed when crouching
        }
        else
        {
            characterController.height = standingHeight;
            moveSpeed *= crouchSpeed; // Reset move speed to normal when standing
        }

        // Adjust the camera position relative to the new height
        Vector3 cameraPosition = playerCamera.localPosition;
        cameraPosition.y = characterController.height / 2f;
        playerCamera.localPosition = cameraPosition;
       
}

    public void PickUpObject()
    {
        if (heldObject != null)
        {
            heldObject.GetComponent<Rigidbody>().isKinematic = false;
            heldObject.transform.parent = null;
            holdingGun = false;
        }

        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        Debug.DrawRay(playerCamera.position, playerCamera.forward * pickUpRange, Color.red, 2f);

        if (Physics.Raycast(ray, out hit, pickUpRange))
        {
            if (hit.collider.CompareTag("PickUp"))
            {
                heldObject = hit.collider.gameObject;
                heldObject.GetComponent<Rigidbody>().isKinematic = true;


                heldObject.transform.position = holdPosition.position;
                heldObject.transform.rotation = holdPosition.rotation;
                heldObject.transform.parent = holdPosition;

            }
            if (hit.collider.CompareTag("Gun"))
            {
                heldObject = hit.collider.gameObject;
                heldObject.GetComponent<Rigidbody>().isKinematic = true;


                heldObject.transform.position = holdPosition.position;
                heldObject.transform.rotation = holdPosition.rotation;
                heldObject.transform.parent = holdPosition;

                holdingGun = true;  


            }
        }
    }
    public void Shoot()
    {
        if (holdingGun == true)

        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.velocity = firePoint.forward * projectileSpeed;
            Destroy(projectile, 1f);
            Debug.Log("jjjjjjjjjjjjjj");
        }

    }

    public void Jump()
    {
        if (characterController.isGrounded)
        {
            velocity.y =Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
      
    }
     private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        Move();
        LookAround();
        ApplyGravity();

    }

    public void Move()
    {
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);

        move = transform.TransformDirection(move);

        characterController.Move(move * moveSpeed * Time.deltaTime);

    }

    public void LookAround()
    {
        float LookX = lookInput.x * lookSpeed;
        float LookY = lookInput.y * lookSpeed;

        transform.Rotate(0, LookX, 0);

        verticalLookRotation -= LookY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90, 90);

        playerCamera.localEulerAngles = new Vector3(verticalLookRotation, 0, 0);
    }
    public void ApplyGravity()
    {
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -0.5f;
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

    }
}
 
