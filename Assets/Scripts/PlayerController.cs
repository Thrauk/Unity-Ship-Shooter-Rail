using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("General Setup Settings")]
    [Tooltip("Input Actions keybinds")] [SerializeField] InputAction movement;
    [Tooltip("How fast the ship is going is going")] [SerializeField] float controlSpeed = 30f;
    [Tooltip("How fast player moves horizontally")] [SerializeField] float xRange = 10f;
    [Tooltip("How fast player moves vertically")] [SerializeField] float yRange = 7f;

    [Header("Laser gun array")]
    [Tooltip("Add all player lasers here")] [SerializeField] GameObject[] lasers;


    [Header("Screen position based tuning")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float positionYawFactor = 2f;

    [Header("Screen position based tuning")]
    [SerializeField] float controlPitchFactor = -2f;
    [SerializeField] float controlRotateFactor = -20f;

    float xThrow, yThrow;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        movement.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    void ProcessRotation()
    {
        // Pitch
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRotateFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        // xThrow = movement.ReadValue<Vector2>().x;
        // yThrow = movement.ReadValue<Vector2>().y;

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float yOffset = yThrow * Time.deltaTime * controlSpeed;

        float newPositionX = transform.localPosition.x + xOffset;
        float newPositionY = transform.localPosition.y + yOffset;

        float clampedXPos = Mathf.Clamp(newPositionX, -xRange, xRange);
        float clampedYPos = Mathf.Clamp(newPositionY, -yRange, yRange);


        transform.localPosition = new Vector3(
            clampedXPos,
            clampedYPos,
            transform.localPosition.z
        );

        // Debug.Log(xThrow);
        // Debug.Log(yThrow);

        // float horizontalThrow = Input.GetAxis("Horizontal");

        // float verticalThrow = Input.GetAxis("Vertical");
    }

    void ProcessFiring()
    {
        // if pushing fire button
        // then print shooting
        if (Input.GetButton("Fire1"))
        {
            SetLasersActive(true);
        }
        else
        {
            SetLasersActive(false);
        }
    }

    void SetLasersActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            // laser.SetActive(true);
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }

    // void DeactivateLasers()
    // {
    //     foreach (GameObject laser in lasers)
    //     {
    //         var emissionModule = laser.GetComponent<ParticleSystem>().emission;
    //         emissionModule.enabled = false;
    //         // laser.SetActive(false);
    //         // ParticleSystem particleSystem = laser.GetComponent<ParticleSystem>();
    //         // particleSystem.Stop();
    //     }
    // }


}
