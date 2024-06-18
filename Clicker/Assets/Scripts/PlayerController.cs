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
    private static readonly int isDashing = Animator.StringToHash("IsDashing");


    Animator animator;

    public float maxSpeed;// 최대속도 설정
    public float jumpPower;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    GhostDash ghostDash;

    bool Jumping = false;           // AM i Jumping?
    //bool Falling = false;
    bool Rolling = false;           // AM i rolling?
    public bool isGrounded = true;  // AM i on the ground?
    bool canCombo = false;          // AM i doing combo attack
    //public float attackRate = 10f;
    
    public LayerMask enemyLayerMask;

    public bool canRoll = false;
    public bool canDash = false;
    public bool canComboAttack = false;
    

    int floatCount = 0;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        ghostDash = GetComponent<GhostDash>();
    }
    private void FixedUpdate()
    {
        Move();
    }
    void Update()
    {
        //Dash();

        //Attack();

        //Roll();

        JumpCheck(); // Checking wheter can jump

        if (Input.GetMouseButtonDown(0))
        {
            OnClick();
        }

    }

    private void Start()
    {
        StartCoroutine(DoClick());
    }

    IEnumerator DoClick()
    {
        while (true)
        {
            OnClick();
            yield return new WaitForSeconds(3f);
        }
    }

  

    public void CheckHit()
    {
        //Debug.Log("I'm hitting!!");
        float CheckDir = 1f;
        

        if (spriteRenderer.flipX) CheckDir = -1f;

        RaycastHit2D hit = Physics2D.Raycast(transform.position+ new Vector3(0, 1.3f, 0), new Vector2(1, 0) * CheckDir, 2f, enemyLayerMask);
        {
            
            if (hit.collider == null) return;
            //Debug.Log(hit.collider.name);
            if (hit.transform.gameObject.TryGetComponent(out Reward reward))
            {
                reward.GivePlayerReward(reward.inven.attackRate * (1+floatCount*0.1f));
            }
                
        }

    }


    void OnDash()
    {

        if (!canDash) return;

        if (!ghostDash.makeGhost)
        {
            ghostDash.makeGhost = true;
            animator.SetBool(isDashing, true);
            StartCoroutine(DoingDash());
        }


    }

    void OnClick()
    {
        OnAttack();
    }

    IEnumerator DoingDash()
    {
        while (ghostDash.makeGhost)
        {
            if (spriteRenderer.flipX)
            {
                transform.position -= new Vector3(0.2f, 0, 0);
            }
            else
            {
                transform.position += new Vector3(0.2f, 0, 0);
            }
            yield return new WaitForSeconds(0.02f);
        }
        yield return null;
    }

    public void DashOff()
    {
        ghostDash.makeGhost = false;
        animator.SetBool(isDashing, false);
    }
    

    void OnRoll()
    {
        if (!canRoll) return;

        if (!Rolling && !Jumping)
        {
            animator.SetBool(isRolling, true);
            Rolling = true;
        }
        
    }

    void ComboStart()
    {
        floatCount = 0;    
    }

    void ComboSum()
    {
        floatCount++;
        //Debug.Log(floatCount);
    }

    void OnAttack()
    {
            //Debug.Log("Attack!!");
        animator.SetBool(isAttacking, true);
        
        if (canCombo && canComboAttack) animator.SetTrigger("NextCombo");

    }

    public void ComboEnable()
    {

        canCombo = true;
        //Debug.Log("ComboEnable");
    }

    public void ComboDisAble()
    {
        canCombo = false;        
    }


    public void AttackEnd()
    {
        //Debug.Log("Combo!!");
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

        if (ghostDash.makeGhost) return;
        

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

        if (Rolling)
        {
            transform.position += moveVelocity * 1.2f * maxSpeed * Time.deltaTime;
            return;
        }
        transform.position += moveVelocity * maxSpeed * Time.deltaTime;
    }

    private void OnJump()
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
    }

    void JumpCheck()
    {
        if (rigid.velocity.y < 0 && !isGrounded && Jumping)
        {
            animator.SetBool(isJumping, false);
            animator.SetBool(isFalling, true);
            //Falling = true;
        }
    }



    IEnumerator DoJump()
    {
        rigid.AddForce(Vector2.up * jumpPower * rigid.mass, ForceMode2D.Impulse);
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
