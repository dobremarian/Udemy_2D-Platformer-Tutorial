using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed;
    public Rigidbody2D playerRB;
    public float jumpForce;

    private bool isGrounded;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;

    private bool canDoubleJump;

    private Animator playerAnimator;
    private SpriteRenderer playerSR;

    public float knockBackLength, knockBackForce;
    private float knockBackCounter;

    public float bounceForce;

    public bool stopInput;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerSR = GetComponent<SpriteRenderer>();

    }


    void Update()
    {
        if (!PauseMenu.instance.isPaused && !stopInput)
        {
            if (knockBackCounter <= 0)
            {

                playerRB.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), playerRB.velocity.y);

                isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, 0.2f, whatIsGround);

                if (isGrounded)
                {
                    canDoubleJump = true;
                }

                if (Input.GetButtonDown("Jump"))
                {
                    if (isGrounded)
                    {
                        playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
                        canDoubleJump = true;

                        AudioManager.instance.PlaySFX(10);
                    }
                    else
                    {
                        if (canDoubleJump)
                        {
                            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
                            canDoubleJump = false;

                            AudioManager.instance.PlaySFX(10);
                        }
                    }

                }

                if (playerRB.velocity.x < 0)
                {
                    playerSR.flipX = true;
                }
                else if (playerRB.velocity.x > 0)
                {
                    playerSR.flipX = false;
                }
            }
            else
            {
                knockBackCounter -= Time.deltaTime;
                if (!playerSR.flipX)
                {
                    playerRB.velocity = new Vector2(-knockBackForce, playerRB.velocity.y);
                }
                else
                {
                    playerRB.velocity = new Vector2(knockBackForce, playerRB.velocity.y);
                }

            }
        }

        playerAnimator.SetFloat("moveSpeed", Mathf.Abs(playerRB.velocity.x));
        playerAnimator.SetBool("isGrounded", isGrounded);
    }

    public void KnockBack()
    {
        knockBackCounter = knockBackLength;
        playerRB.velocity = new Vector2(0f, knockBackForce);
        playerAnimator.SetTrigger("isHurt");
    }

    public void Bounce()
    {
        playerRB.velocity = new Vector2(playerRB.velocity.x, bounceForce);
        AudioManager.instance.PlaySFX(10);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Platform")
        {
            transform.parent = other.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.tag == "Platform")
        {
            transform.parent = null;
        }
    }
}
