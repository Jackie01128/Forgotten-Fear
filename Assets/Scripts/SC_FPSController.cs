using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]

public class SC_FPSController : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float crouchSpeed = 4.0f;
    public float crouchHeight = 1.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    public float maxStamina = 10.0f;
    public float staminaRegenerationRate = 1.0f;
    public Slider staminaSlider;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;
    float standingHeight;
    bool isCrouching = false;
    float cameraHeightOffset = 0.0f;
    float currentStamina;
    private bool canSprint = true;
    private bool isSprinting = false;
    private float targetSpeed;
    private float minSpeed = 4.0f; // Set a minimum speed (e.g., walking speed)

    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        standingHeight = characterController.height;
        cameraHeightOffset = playerCamera.transform.localPosition.y;
        currentStamina = maxStamina;
        targetSpeed = walkingSpeed;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Detect crouch input
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = !isCrouching;

            // Update camera height and character controller height simultaneously
            float targetHeight = isCrouching ? crouchHeight : standingHeight;
            float targetCameraHeight = isCrouching ? cameraHeightOffset - 0.5f : cameraHeightOffset;

            characterController.height = targetHeight;
            Vector3 cameraPosition = playerCamera.transform.localPosition;
            cameraPosition.y = targetCameraHeight;
            playerCamera.transform.localPosition = cameraPosition;
        }

        // Calculate the player's speed based on current stamina
        float speedMultiplier = currentStamina / maxStamina;

        // Update the target speed based on sprinting and crouching status
        if (isSprinting)
        {
            targetSpeed = runningSpeed;
        }
        else if (isCrouching)
        {
            targetSpeed = crouchSpeed;
        }
        else
        {
            targetSpeed = walkingSpeed;
        }

        // Ensure that the speed never drops below the minimum speed
        if (speedMultiplier * targetSpeed < minSpeed)
        {
            targetSpeed = minSpeed / speedMultiplier;
        }

        float curSpeedX = canMove ? (isCrouching ? crouchSpeed : targetSpeed) * speedMultiplier * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isCrouching ? crouchSpeed : targetSpeed) * speedMultiplier * Input.GetAxis("Horizontal") : 0;

        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Detect running input
        isSprinting = Input.GetKey(KeyCode.LeftShift) && !isCrouching && currentStamina > 0 && canSprint;

        // Enable or disable the Stamina Slider based on sprinting status
        if (staminaSlider != null)
        {
            if (isSprinting)
            {
                // Deduct stamina when running
                currentStamina -= 2.0f * Time.deltaTime;

                // Ensure currentStamina does not go below 0
                currentStamina = Mathf.Max(0.0f, currentStamina);

                // Update the Stamina Slider value
                staminaSlider.value = currentStamina / maxStamina;
                staminaSlider.gameObject.SetActive(true);

                // Transition from sprint to walking when stamina is low
                if (currentStamina < 0.1f)
                {
                    isSprinting = false;
                }
            }
            else
            {
                // Regenerate stamina when not running
                currentStamina += staminaRegenerationRate * Time.deltaTime;

                // Ensure currentStamina does not exceed maxStamina
                currentStamina = Mathf.Clamp(currentStamina, 0.0f, maxStamina);

                // Hide the Stamina Slider when not running
                staminaSlider.gameObject.SetActive(false);
            }
        }

        
        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        // Debug logs
        //Debug.Log("Current Speed: " + curSpeedX);

        // Apply character movement
        characterController.Move(moveDirection * Time.deltaTime);
    }
}
