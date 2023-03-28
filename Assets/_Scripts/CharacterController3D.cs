using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController3D : MonoBehaviour
{
    public float moveSpeed = 5.0f; 
    public float mouseSensitivity = 100.0f; 

    private CharacterController controller; 
    private Transform cameraTransform; 
    private float verticalRotation = 0.0f;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {

        // handle movement input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 moveDirection = transform.right * x + transform.forward * z;
        moveDirection.Normalize();
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);

        // handle camera rotation input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            velocity.y = Mathf.Sqrt(4 * -2f);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            velocity.y = Mathf.Sqrt(4 * 2f);
        }


        controller.Move(velocity * Time.deltaTime);


        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
