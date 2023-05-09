using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody2D rb;
    private float horizontal;
    private float gravityScale = 1f;
    private float vertical;
    private bool ladderTriggered = false;
    private bool isFacingRight = true;
    private bool isClimbing = false;
    void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {

        if(ladderTriggered && Mathf.Abs(vertical) > 0) {
            isClimbing = true;
        }
    }
    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if(!isFacingRight && horizontal > 0f) {
            FlipCharacter();
        }
        else if (isFacingRight && horizontal < 0f) {
            FlipCharacter();
        }

        if(isClimbing) {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, vertical * speed);
        }
        else {
            rb.gravityScale = gravityScale;
        }
    }

    public void PlayerMove(InputAction.CallbackContext context) {
        horizontal = context.ReadValue<Vector2>().x;
        vertical = context.ReadValue<Vector2>().y;
    }

    public void PlayerJump (InputAction.CallbackContext context) {
        Debug.Log(isGrounded());
        if(context.performed && isGrounded()) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private bool isGrounded() {
        return Physics2D.OverlapCircle(groundCheck.position,0.2f,groundLayer);
    }

    private void FlipCharacter() {
        isFacingRight = !isFacingRight;
        Vector3 localscale = transform.localScale;
        localscale.x = localscale.x * -1f;
        transform.localScale = localscale;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Ladder")) {
            ladderTriggered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.CompareTag("Ladder")) {
            ladderTriggered = false;
            isClimbing = false;
        }
    }
}
