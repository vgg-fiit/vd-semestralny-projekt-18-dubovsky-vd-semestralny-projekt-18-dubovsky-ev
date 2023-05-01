using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    public float acceleration = 50; // how fast you accelerate
    public float accSprintMultiplier = 4; // how much faster you go when "sprinting"
    public float lookSensitivity = 1; // mouse look sensitivity
    public float dampingCoefficient = 5; // how quickly you break to a halt after you stop your input
    public bool focusOnEnable = true; // whether or not to focus and lock cursor immediately on enable

    Vector3 velocity; // current velocity

    static bool Focused
    {
        get => Cursor.lockState == CursorLockMode.Locked;
        set
        {
            Cursor.lockState = value ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = value == false;
        }
    }

    void OnEnable()
    {
        if (StaticFiltrationController.cameraStatic == false)
        {
            if (focusOnEnable) Focused = true;

        }
    }

    void OnDisable(){

        if (StaticFiltrationController.cameraStatic == false)
        {
            Focused = false;
        }
    }

    void Update()
    {
        if (StaticFiltrationController.cameraStatic == false)
        {
            // Input
            //if (Focused)
            UpdateInput();

            // Physics
            velocity = Vector3.Lerp(velocity, Vector3.zero, dampingCoefficient * Time.deltaTime);
            transform.position += velocity * Time.deltaTime;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            if (StaticFiltrationController.cameraStaticSpatialCube == false)
            {
                Camera.main.transform.localPosition = new Vector3(-37.43f, 5.9f, -41.96f);
                Camera.main.transform.rotation = Quaternion.Euler(13.787f, -3.243f, -0.774f);
            }
            else
            {
                Camera.main.transform.localPosition = new Vector3(StaticFiltrationController.cameraStaticSpatialCubeLastCube.transform.GetChild(0).transform.GetChild(0).transform.position.x + 19, 6.55f, StaticFiltrationController.cameraStaticSpatialCubeLastCube.transform.GetChild(0).transform.GetChild(0).transform.position.z - 4);
                Camera.main.transform.rotation = Quaternion.Euler(13.787f, 223.554f, -0.774f);
            }
            
        }
        
    }

    void UpdateInput()
    {
        // Position
        velocity += GetAccelerationVector() * Time.deltaTime;

        // Rotation
        Vector2 mouseDelta = lookSensitivity * new Vector2(Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"));
        Quaternion rotation = transform.rotation;
        Quaternion horiz = Quaternion.AngleAxis(mouseDelta.x, Vector3.up);
        Quaternion vert = Quaternion.AngleAxis(mouseDelta.y, Vector3.right);
        transform.rotation = horiz * rotation * vert;

        // Leave cursor lock
        if (Input.GetKeyDown(KeyCode.Escape))
            Focused = !Focused;
    }

    Vector3 GetAccelerationVector()
    {
        Vector3 moveInput = default;

        void AddMovement(KeyCode key, Vector3 dir)
        {
            if (Input.GetKey(key))
                moveInput += dir;
        }

        AddMovement(KeyCode.W, Vector3.forward);
        AddMovement(KeyCode.S, Vector3.back);
        AddMovement(KeyCode.D, Vector3.right);
        AddMovement(KeyCode.A, Vector3.left);
        AddMovement(KeyCode.Space, Vector3.up);
        AddMovement(KeyCode.LeftControl, Vector3.down);
        Vector3 direction = transform.TransformVector(moveInput.normalized);

        if (Input.GetKey(KeyCode.LeftShift))
            return direction * (acceleration * accSprintMultiplier); // "sprinting"
        return direction * acceleration; // "walking"
    }
}