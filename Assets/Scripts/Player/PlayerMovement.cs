using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Components")]
    public Rigidbody2D rb;

    [Header("Movement Variables")]
    public float movementSpeed = 5f;

    private Vector2 movementInput;

    //Called on player input
    public void OnMovement(InputAction.CallbackContext value)
    {
        movementInput = value.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        //Move character here
        rb.velocity = new Vector2(movementInput.x*movementSpeed, rb.velocity.y);
    }
}
