using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Components")]
    public Rigidbody2D rb;

    [Header("Movement Variables")]
    public float movementSpeed = 5f;

    private float movementInput;

    //Called on player input
    public void OnMovement(InputAction.CallbackContext value)
    {
        movementInput = value.ReadValue<float>();
    }

    private void FixedUpdate()
    {
        //Move character here
        rb.velocity = new Vector2(movementInput*movementSpeed, rb.velocity.y);
    }
}
