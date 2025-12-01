using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    public Vector2 Move;
    public Vector2 Look;
    public bool Sprint;
    public bool Jump;

    Controls controls;

    void OnEnable()
    {
        controls = new Controls();
        controls.Player.SetCallbacks(this);
        controls.Enable();
    }

    void OnDestroy()
    {
        controls.Disable();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        Move = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Look = context.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        Sprint = context.performed;
    }
    
    public void OnJump(InputAction.CallbackContext context)
    {
        Jump = context.performed;
    }
}
