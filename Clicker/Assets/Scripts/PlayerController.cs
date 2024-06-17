using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private static readonly int isRunning = Animator.StringToHash("IsRunning");
    private static readonly int isJumping = Animator.StringToHash("IsJumping");
    private static readonly int isFalling = Animator.StringToHash("IsFalling");
    private static readonly int isRolling = Animator.StringToHash("IsRolling");
    private static readonly int isAttacking = Animator.StringToHash("IsAttacking");


    Animator animator;

    public float maxSpeed;// 최대속도 설정
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;

    bool Jumping = false;
    //bool Falling = false;
    bool Rolling = false;
    public bool isGrounded = true;
    bool canCombo = false;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {

        

        Move();

        
    }
    void Update()
    {
        Attack();

        Roll();

        Jump();
    }

    void Roll()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !Rolling && !Jumping)
        {
            animator.SetBool(isRolling, true);
            Rolling = true;
        }
        
    }


    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Attack!!");
            animator.SetBool(isAttacking, true);

            if (canCombo) animator.SetTrigger("NextCombo");

        }
    }

    public void ComboEnable()
    {
        canCombo = true;
    }

    public void ComboDisAble()
    {
        canCombo = false;        
    }


    public void AttackEnd()
    {
        Debug.Log("Combo!!");
        animator.SetBool(isAttacking, false);
    }
    

    public void RollEnd()
    {
        animator.SetBool(isRolling, false);
        Rolling = false;
    }


    private void Move()
    {
        Vector3 moveVelocity = Vector3.zero;
        

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            animator.SetBool(isRunning, true);
            moveVelocity = Vector3.left;
            spriteRenderer.flipX = true;
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            animator.SetBool(isRunning, true);
            moveVelocity = Vector3.right;
            spriteRenderer.flipX = false;
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
        if (Rolling) return;

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
            //Falling = true;
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

        //Debug.Log(collider.gameObject.tag);
        if (collider.gameObject.CompareTag("Floor"))
        {
            
            if (!isGrounded && Jumping)
            {
                //Falling = false;
                isGrounded = true;
                Jumping = false;
                animator.SetBool(isFalling, false);
                animator.SetBool(isJumping, false);
            }
        }
    }



}
