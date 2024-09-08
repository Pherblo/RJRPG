using UnityEngine;
using UnityEngine.InputSystem;

public class BaseCharacter : MonoBehaviour
{
    public float WalkSpeed;
    public float RunSpeed;

    public Rigidbody2D rb;

    private Vector2 movementInput;
    private bool forceWalk;

    private void FixedUpdate()
    {
        Move(movementInput, forceWalk);
    }

    public void Move(Vector2 dir, bool walking)
    {
        Vector2 speed = dir.normalized * (walking ? WalkSpeed : RunSpeed);

        rb.MovePosition((Vector2)transform.position + speed * Time.fixedDeltaTime);
    }

    public void OnMovement(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }
    public void OnForceWalk(InputValue value)
    {
        forceWalk = value.Get<float>() == 0 ? false : true;
    }

}
