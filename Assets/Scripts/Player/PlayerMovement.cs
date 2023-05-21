using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    public bool isFacingRight = true;

    public Animator animator;

    [SerializeField] public Rigidbody2D rb;
    [SerializeField] public Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private AudioSource jumpSoundEffect;
    [SerializeField] private AudioSource walkSoundEffect;
    
    
    void Update()
    {
        if(!GlobalManager.OnPause)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
         
            animator.SetFloat("Running", Mathf.Abs(horizontal));
            if(Mathf.Abs(horizontal)>0.01f)
            {
                walkSoundEffect.Play();
            }

            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                animator.SetBool("isJumping", true);
                jumpSoundEffect.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            }
            else if (IsGrounded())
                animator.SetBool("isJumping", false);
            
            if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
            {
                
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
               
            }

            Flip();
        }
        
    }

    private void FixedUpdate()
    {
       
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    public void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
