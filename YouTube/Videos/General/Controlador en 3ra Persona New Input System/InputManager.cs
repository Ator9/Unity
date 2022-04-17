using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    private Rigidbody playerRb;
    private PlayerInputActions playerInputActions;
    private bool isGrounded;
    private float xDeg;

    private void Awake()
    {
        playerRb = this.GetComponent<Rigidbody>();
        playerInputActions = new PlayerInputActions();
    }

    // FixedUpdate executes 50 times per second
    void FixedUpdate()
    {
        if (isGrounded) Move(); // WASD
    }

    // LateUpdate is called after all Update functions have been called.
    void LateUpdate()
    {
        if (Mouse.current.rightButton.isPressed)
        {
            xDeg += Mouse.current.delta.ReadValue().x * 0.2f;
            transform.rotation = Quaternion.Euler(0, xDeg, 0);
        }
    }

    // This function is called when the object becomes enabled and active.
    private void OnEnable()
    {
        playerInputActions.Enable(); // Enable all actions maps or enable specific action map
        playerInputActions.Player.Jump.started += Jump;
    }

    public void Jump(InputAction.CallbackContext obj)
    {
        if (isGrounded)
        {
            playerRb.AddForce(Vector3.up * CharacterStats.JUMP_FORCE, ForceMode.Impulse);
            isGrounded = false; // fix in place jump (velocity zero)
        }
    }

    public void Move()
    {
        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        if (inputVector != Vector2.zero)
        {
            playerRb.AddRelativeForce(new Vector3(inputVector.x, 0, inputVector.y) * 200);
            playerRb.velocity = Vector3.ClampMagnitude(playerRb.velocity, CharacterStats.SPEED);
        }
        else if (playerRb.velocity != Vector3.zero)
        {
            playerRb.velocity = Vector3.zero;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Terrain") isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Terrain") isGrounded = false;
    }
}
