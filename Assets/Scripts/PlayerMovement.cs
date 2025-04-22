using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 moveInput;
    private Vector2 lastMoveDir = Vector2.down;

    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
    
        if (moveInput != Vector2.zero)
        {
            lastMoveDir = moveInput;
        }
    
        // ✅ Always face the last horizontal direction (even when idle)
        if (lastMoveDir.x != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = lastMoveDir.x > 0 ? -1 : 1;
            transform.localScale = scale;
        }
    
        animator.SetFloat("MoveX", lastMoveDir.x);
        animator.SetFloat("MoveY", lastMoveDir.y);
        animator.SetBool("IsMoving", moveInput != Vector2.zero);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput.normalized * moveSpeed * Time.fixedDeltaTime);
    }
}
