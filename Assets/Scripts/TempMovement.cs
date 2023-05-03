using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// a temporary class to allow you to move something around with WASD. Intended for use to test other mechanics while the actual player is being worked on.
public class TempMovement : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    private Rigidbody2D rb;
    
    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.velocity = movement.normalized * speed;
    }
}
