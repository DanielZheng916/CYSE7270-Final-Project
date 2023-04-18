using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerController2D : MonoBehaviour
{

	[SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_WallCheck;                             //Posicion que controla si el personaje toca una pared

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 velocity = Vector3.zero;
	private float limitFallSpeed = 25f; // Limit fall speed

	public bool canDoubleJump = true; //If player can double jump
	[SerializeField] private float m_DashForce = 25f;
	private bool canDash = true;
	private bool isDashing = false; //If player is dashing
	private bool m_IsWall = false; //If there is a wall in front of the player
	private bool isWallSliding = false; //If player is sliding in a wall
	private bool oldWallSlidding = false; //If player is sliding in a wall in the previous frame
	private float prevVelocityX = 0f;
	private bool canCheck = false; //For check if player is wallsliding

	public float life = 10f; //Life of the player
	public bool invincible = false; //If player can die
	private bool canMove = true; //If player can move

	private Animator animator;
	public ParticleSystem particleJumpUp; //Trail particles
	public ParticleSystem particleJumpDown; //Explosion particles

	private float jumpWallStartX = 0;
	private float jumpWallDistX = 0; //Distance between player and wall
	private bool limitVelOnWallJump = false; //For limit wall jump distance with low fps

	private CapsuleCollider2D coll;

	[SerializeField] private LayerMask jumpableGround;

	public SpriteRenderer trailRender;

	public TrailRenderer trail;

	// Start is called before the first frame update
	void Start()
	{
		Debug.Log("Player Movement Start.");
		coll = GetComponent<CapsuleCollider2D>();
	}

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public void Move(float move, bool jump, bool dash)
    {
		if (dash && canDash)
		{
			//m_Rigidbody2D.AddForce(new Vector2(transform.localScale.x * m_DashForce, 0f));
			StartCoroutine(DashCooldown());
		}
		if (isDashing)
		{
			m_Rigidbody2D.velocity = new Vector2(transform.localScale.x * m_DashForce, 0);
			Debug.Log("Dash!");
		}
		//only control the player if grounded or airControl is turned on
		else 
		{
			if (m_Rigidbody2D.velocity.y < -limitFallSpeed)
				m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, -limitFallSpeed);
			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref velocity, m_MovementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}

		// If the player should jump...
		if (isGrounded() && jump)
		{
			// Add a vertical force to the player.
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
		}
	}

	IEnumerator DashCooldown()
	{
		animator.SetBool("IsDashing", true);
		isDashing = true;
		canDash = false;
		trailRender.enabled = true;
		trail.enabled = true;
		yield return new WaitForSeconds(0.2f);
		isDashing = false;
		trailRender.enabled = false;
		trail.enabled = false;
		yield return new WaitForSeconds(0.5f);
		canDash = true;
	}

	private bool isGrounded()
	{
		// create box, move down 1px (to overlap with ground), 
		return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}


}
