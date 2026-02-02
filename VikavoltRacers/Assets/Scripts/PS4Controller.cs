using UnityEngine;
using UnityEngine.InputSystem;

public class PS4Controller: MonoBehaviour
{
    private Gamepad gamepad;
    private Vector2 moveInput; // Stores the current movement input

    public bool active { get; private set; }
    
    public Vector2 leftStick { get; private set; }
    public Vector2 rightStick { get; private set; }
    public Vector3 move { get; private set; }

    void Update()
    {
        gamepad = Gamepad.current; // get the currently connected gamepad

        if (gamepad == null)
        {
            active = false;
            //Debug.Log("No gamepad connected");
               // Convert input to 3D movement
            move = new Vector3(moveInput.x, 0f, moveInput.y);
            return;
        }

        // Left stick
        leftStick = gamepad.leftStick.ReadValue();

        // Right stick
        rightStick = gamepad.rightStick.ReadValue();

        //Debug.Log($"Left Stick: ({leftStick.x:F2}, {leftStick.y:F2}) | Right Stick: ({rightStick.x:F2}, {rightStick.y:F2})");
    }
    
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
}

