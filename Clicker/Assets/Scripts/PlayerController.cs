using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private static readonly int isRunning = Animator.StringToHash("IsRunning");
    private static readonly int isJumping = Animator.StringToHash("IsJumping");
    private static readonly int isFalling = Animator.StringToHash("IsFalling");


    Animator animator;

    public float maxSpeed;// 최대속도 설정
    Rigidbody2D rigid;

    bool Jumping = false;
    bool Falling = false;
    public bool isGrounded = true;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        Move();
    }
    void Update()
    {
        Jump();
    }

    private void Move()
    {
        Vector3 moveVelocity = Vector3.zero;

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            animator.SetBool(isRunning, true);
            moveVelocity = Vector3.left;
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            animator.SetBool(isRunning, true);
            moveVelocity = Vector3.right;
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            animator.SetBool(isRunning, false);
        }

        transform.position += moveVelocity * maxSpeed * Time.deltaTime;
    }

    private void Jump()
    {
        //Debug.Log(rigid.velocity.y);


        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) // && !Jumping
        {
            isGrounded = false;
            
            StartCoroutine(DoJump());
            return;
            //Debug.Log("Try Jumping");
        }

        if (rigid.velocity.y < 0 && !isGrounded && Jumping)
        {
            animator.SetBool(isJumping, false);
            animator.SetBool(isFalling, true);
            Falling = true;
        }

    }



    IEnumerator DoJump()
    {
        rigid.AddForce(Vector2.up * 15 * rigid.mass, ForceMode2D.Impulse);
        animator.SetBool(isJumping, true);
        yield return new WaitForSeconds(0.1f);
        Jumping = true;
    }


    private void OnCollisionStay2D(Collision2D collider)
    {

        Debug.Log(collider.gameObject.tag);
        if (collider.gameObject.CompareTag("Floor"))
        {
            
            if (!isGrounded && Jumping)
            {
                Falling = false;
                isGrounded = true;
                Jumping = false;
                animator.SetBool(isFalling, false);
                animator.SetBool(isJumping, false);
            }
        }
    }



}
