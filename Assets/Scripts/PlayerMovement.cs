using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private enum MovementState {idle, running, jumping, falling}
    private MovementState state = MovementState.idle;

    private BoxCollider2D coll;
    private Rigidbody2D rb;
    private Animator anime;
    private SpriteRenderer sr;
    private float dirX = 0;

    [SerializeField] private LayerMask jumpableGround;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Player Movement Start.");
        rb = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(7f * dirX, rb.velocity.y);

        changeAnima();
    }

    private void changeAnima()
    {
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.velocity = new Vector3(rb.velocity.x, 7);
        }

        if (dirX > 0)
        {
            state = MovementState.running;
            sr.flipX = false;
        }
        else if (dirX < 0)
        {
            state = MovementState.running;
            sr.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anime.SetInteger("state", (int)state);
    }

    private bool isGrounded()
    {
        // create box, move down 1px (to overlap with ground), 
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
