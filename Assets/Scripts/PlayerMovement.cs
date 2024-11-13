using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public Transform ceilingCheck;
    public LayerMask groundObjects;
    public float checkRadius;
    public int maxJumpCount;
    public Vector3 originalScale;

    private bool isGrounded;
    private Rigidbody2D rb;
    private bool facingRight = true;
    private float moveDirection;
    private bool isJumping = false;
    private int jumpCount;

    // Yeni dash değişkenleri
    private bool canDash = true;
    private bool isDashing;
    public float dashPower = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        jumpCount = maxJumpCount;
        originalScale = transform.localScale;
    }

    void Update()
    {
        ProcessInputs();
        Animate();
        
        // Dash'i tetikle
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && !isDashing)
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundObjects);
        if (isGrounded)
        {
            jumpCount = maxJumpCount;
        }

        if (isDashing) return; // Dash sırasında hareket işlemi durduruluyor

        Move();
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
        if (isJumping && jumpCount > 0)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            jumpCount--;
        }

        isJumping = false;
    }

    private void Animate()
    {
        if (moveDirection > 0 && !facingRight)
        {
            FlipCharacter();
        }
        else if (moveDirection < 0 && facingRight)
        {
            FlipCharacter();
        }
    }

    private void ProcessInputs()
    {
        moveDirection = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            isJumping = true;
        }
    }

    private void FlipCharacter()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f; // Yer çekimini kapatıyoruz

        // Dash yönünü belirliyoruz
        rb.velocity = new Vector2((facingRight ? 1 : -1) * dashPower, 0f);

        yield return new WaitForSeconds(dashDuration);

        rb.gravityScale = originalGravity; // Yer çekimini geri yüklüyoruz
        isDashing = false;

        // Dash bittiğinde normal harekete döner
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
