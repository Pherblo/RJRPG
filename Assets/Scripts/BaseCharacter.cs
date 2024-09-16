using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class BaseCharacter : MonoBehaviour
{
    public float WalkSpeed;
    public float RunSpeed;
    public float runThreshold = 0.5f;

    public Rigidbody2D rb;
    public Animator animator;

    private Vector2 movementInput;
    private bool forceWalk;
    private bool facingRight;

    private void FixedUpdate()
    {
        Move(movementInput, forceWalk);
    }

    public void Move(Vector2 dir, bool walking = false)
    {
        if (walking)
            dir *= runThreshold;

        Vector2 speed = dir.normalized * (dir.magnitude > runThreshold ? RunSpeed : WalkSpeed);
        
        rb.MovePosition((Vector2)transform.position + speed * Time.fixedDeltaTime);

    }

    public void OnMovement(InputValue value)
    {
        movementInput = value.Get<Vector2>();

        AnimateMove(movementInput);
    }
    public void OnForceWalk(InputValue value)
    {
        forceWalk = value.Get<float>() == 0 ? false : true; 
    }

    private void AnimateMove(Vector2 input)
    {
        input.x = Mathf.Abs(input.x);

        float angle = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;
        int facingDir = Mathf.RoundToInt(angle / 45);

        animator.SetFloat("speed", input.magnitude == 0 ? 0 : (input.magnitude < runThreshold || forceWalk == true ? 1 : 2));
        animator.SetFloat("facingDir", facingDir);

        // flip sprite if nessessary
        if(movementInput.x > 0 != facingRight)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;

            facingRight = !facingRight;
        }
    } 

}
