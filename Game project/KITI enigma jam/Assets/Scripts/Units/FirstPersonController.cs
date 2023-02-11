using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public bool canMove { get; private set; } = true;
    private bool isRunning => canRun && Input.GetKey(RunKey);
    private bool shouldJump => Input.GetKeyDown(JumpKey) && characterController.isGrounded;
    private bool shouldCrouch => Input.GetKeyDown(CrouchKey) && !duringCrouchAnimation && characterController.isGrounded;

    [Header("Functional Bools")]
    [SerializeField] private bool canRun = true;
    [SerializeField] private bool canJump = true;
    [SerializeField] private bool canCrouch = true;
    [SerializeField] private bool hasHeadBob = true;
    [SerializeField] private bool willSlideOnSlopes = true;
    [SerializeField] private bool canZoom = true;

    [Header("Controls")]
    [SerializeField] private KeyCode RunKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode JumpKey = KeyCode.Space;
    [SerializeField] private KeyCode CrouchKey = KeyCode.LeftControl;
    [SerializeField] private KeyCode ZoomKey = KeyCode.Mouse1;

    [Header("Movement Parameter")]
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float runSpeed = 6.0f;
    [SerializeField] private float crouchSpeed = 3.0f;
    [SerializeField] private float slopeSpeed = 8f;

    [Header("Look Parameter")]
    [SerializeField, Range(0.01f, 10)] private float lookSpeedX = 2.0f;
    [SerializeField, Range(0.01f, 10)] private float lookSpeedY = 2.0f;
    [SerializeField, Range(0.01f, 2)] private float upLookLimit = 2.0f;
    [SerializeField, Range(0.01f, 2)] private float downLookLimit = 2.0f;

    [Header("Jump Parameter")]
    [SerializeField] private float jumpForce = 8.0f;
    [SerializeField] private float gravity = 30.0f;

    [Header("Crouch Parameter")]
    [SerializeField] private float crouchHeight = 0.9f;
    [SerializeField] private float standingHeight = 1.8f;
    [SerializeField] private float timeToCrouch = 0.25f;
    [SerializeField] private Vector3 crouchingCentre = new Vector3(0, 0.3f, 0);
    [SerializeField] private Vector3 standingCentre = new Vector3(0, 0, 0);
    private bool isCrouching;
    private bool duringCrouchAnimation;

    [Header("headBob Parameter")]
    [SerializeField] private float walkBobspeed = 14f;
    [SerializeField] private float walkBobAmount = 0.05f;
    [SerializeField] private float RunBobspeed = 18f;
    [SerializeField] private float RunBobAmount = 0.11f;
    [SerializeField] private float crouchBobspeed = 8f;
    [SerializeField] private float crouchBobAmount = 0.025f;
    private float defualtYPos = 0;
    private float timer;

    [Header("Zoom Parameters")]
    [SerializeField] private float timeToZoom = 0.3f;
    [SerializeField] private float zoomFOV = 30f;
    private float defualtFOV;
    private Coroutine zoomRoutine;

    //sliding params
    private Vector3 hitPointNormal;

    private bool isSliding
    {
        get
        {
            if(characterController.isGrounded && Physics.Raycast(transform.position, Vector3.down, out RaycastHit slopeHit, 2f))
            {
                hitPointNormal = slopeHit.normal;
                return Vector3.Angle(hitPointNormal, Vector3.up) > characterController.slopeLimit;
            }
            else
            {
                return false;
            }
        }
    }


    private Camera playerCamera;
    private CharacterController characterController;

    private Vector3 moveDirection;
    private Vector3 currentInput;

    private float rotationX = 0;

    // Start is called before the first frame update
    void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();
        defualtYPos = playerCamera.transform.localPosition.y;
        defualtFOV = playerCamera.fieldOfView;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            HandleMovementInput();
            HandleMouseLook();

            if(canJump)
            {
                HandleJump();
            }
            if(canCrouch)
            {
                HandleCrouch();
            }
            if(hasHeadBob)
            {
                HandleHeadBob();
            }
            if(canZoom)
            {
                HandleZoom();
            }


            ApplyFinalMovements();
        }
    }

    private void HandleMovementInput()
    {
        //Gets the WASD input from the user
        currentInput = new Vector2((isCrouching ? crouchSpeed : isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical"), (isCrouching ? crouchSpeed : isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal"));

        //Makes the player move on the x and y
        float moveDirectionY = moveDirection.y;
        moveDirection = (transform.TransformDirection(Vector3.forward) * currentInput.x) + (transform.TransformDirection(Vector3.right) * currentInput.y);
        moveDirection.y = moveDirectionY;
    }

    private void HandleMouseLook()
    {
        rotationX -= Input.GetAxis("Mouse Y") * lookSpeedY;
        rotationX = Mathf.Clamp(rotationX, -upLookLimit, downLookLimit);
        playerCamera.transform.localRotation = quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedX, 0);
    }

    private void HandleJump()
    {
        if(shouldJump)
        {
            moveDirection.y = jumpForce;
        }
    }

    private void HandleCrouch()
    {
        if(shouldCrouch)
        {
            StartCoroutine(CrouchStand());
        }
    }

    private void HandleHeadBob()
    {
        if(!characterController.isGrounded)
        {
            return;
        }
        if(Mathf.Abs(moveDirection.x) > 0.1f || Mathf.Abs(moveDirection.z) > 0.1f)
        {
            timer += Time.deltaTime * (isCrouching ? crouchBobspeed : isRunning ? RunBobspeed : walkBobspeed);
            playerCamera.transform.localPosition = new Vector3(
                playerCamera.transform.localPosition.x,
                defualtYPos + Mathf.Sin(timer) * (isCrouching ? crouchBobAmount : isRunning ? RunBobAmount : walkBobAmount),
                playerCamera.transform.localPosition.z);
        }
    }

    private void HandleZoom()
    {
        if (Input.GetKeyDown(ZoomKey))
        {
            if(zoomRoutine != null)
            {
                StopCoroutine(zoomRoutine);
                zoomRoutine = null;
            }

            zoomRoutine = StartCoroutine(ToggleZoom(true));
        }
        if (Input.GetKeyUp(ZoomKey))
        {
            if (zoomRoutine != null)
            {
                StopCoroutine(zoomRoutine);
                zoomRoutine = null;
            }

            zoomRoutine = StartCoroutine(ToggleZoom(false));
        }
    }

    private IEnumerator CrouchStand()
    {
        if (isCrouching && Physics.Raycast(playerCamera.transform.position, Vector3.up, 1f))
        {
            yield break;
        }

        duringCrouchAnimation = true;

        float timeElapsed = 0;
        float targetHeight = isCrouching ? standingHeight : crouchHeight;
        float currentheight = characterController.height;
        Vector3 targetCentre = isCrouching ? standingCentre : crouchingCentre;
        Vector3 currentCentre = characterController.center;

        while(timeElapsed < timeToCrouch)
        {
            characterController.height = Mathf.Lerp(currentheight, targetHeight, timeElapsed / timeToCrouch);
            characterController.center = Vector3.Lerp(currentCentre, targetCentre, timeElapsed / timeToCrouch);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        characterController.height = targetHeight;
        characterController.center = targetCentre;

        isCrouching = !isCrouching;

        duringCrouchAnimation = false;
    }

    private IEnumerator ToggleZoom(bool isEnter)
    {
        float targetFOV = isEnter ? zoomFOV : defualtFOV;
        float startingFOV = playerCamera.fieldOfView;
        float timeElapsed = 0;

        while(timeElapsed < timeToZoom)
        {
            playerCamera.fieldOfView = Mathf.Lerp(startingFOV, targetFOV, timeElapsed / timeToZoom);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        playerCamera.fieldOfView = targetFOV;
        zoomRoutine = null;
    }

    private void ApplyFinalMovements()
    {
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        if(willSlideOnSlopes && isSliding)
        {
            moveDirection += new Vector3(hitPointNormal.x, -hitPointNormal.y, hitPointNormal.z) * slopeSpeed;
        }

        characterController.Move(moveDirection * Time.deltaTime);
    }
}