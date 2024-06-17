using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private static readonly int isRunning = Animator.StringToHash("IsRunning");
    Animator animator;

    public float maxSpeed;// 최대속도 설정
    Rigidbody2D rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        Move();

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




}
