using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] InputAction movement;
    [SerializeField] float controlSpeed = 30f;
    [SerializeField] float xRange = 10f;
    [SerializeField] float yRange = 7f;

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
    }

    void ProcessRotation() {
        transform.localRotation = Quaternion.Euler(-30f, 30f, 0f);
    }

    private void ProcessTranslation()
    {
        float xThrow = movement.ReadValue<Vector2>().x;
        float yThrow = movement.ReadValue<Vector2>().y;

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
}
