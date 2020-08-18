using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    #region PrivateVariables
        bool isGrounded;
        bool facingRight = true;
        float fallMultiplier=2.5f;
        float lowJumpMultiplier=2f;
        float jumpPressedRemember=0;
        float groundRemember=0;
        PlayerInput input;
        Rigidbody2D rb;
    #endregion

    [SerializeField] float vel=5f;
    [SerializeField] float jumpVel = 5f;
    [SerializeField] Transform tagGround;
    [SerializeField] LayerMask playerMask;
    [Range(0.1f, 0.2f)]
	[SerializeField] private float jumpRememberTime;
    [Range(0f, 0.15f)]
    [SerializeField] private float groundedRememberTime;
    void Awake()
    {
        input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        jumpPressedRemember -= Time.deltaTime;
		groundRemember -= Time.deltaTime;

        if (rb.velocity.y < 0)
		{
			rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
		} else if (rb.velocity.y > 0 && !input.jumpTrig)
		{
			rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
		}

        if(input.jumpTrig)
        {
            jumpPressedRemember = jumpRememberTime;
			// Invoke("ResetJumpCom", 0.05f);
        }
        if ((jumpPressedRemember > 0))
		{
			Jump();
		}

        Move(input.dir);
    }

    void FixedUpdate()
    {
        if(Physics2D.Linecast(transform.position, tagGround.position, playerMask))
        {
            groundRemember = groundedRememberTime;
            isGrounded = true;
        } else isGrounded = false;
    }

    void Move(float direc)
    {
        Vector2 moveVel = rb.velocity;
        moveVel.x = direc * vel;
        rb.velocity = moveVel;

        if(direc<0 && facingRight) Flip();
        else if(direc>0 && !facingRight) Flip();
    }

    void Jump()
    {
        if ((groundRemember > 0))
		{
            groundRemember = 0;
			jumpPressedRemember = 0;
            Vector2 moveVel = rb.velocity;
            moveVel.y =  jumpVel;
            rb.velocity = moveVel;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
