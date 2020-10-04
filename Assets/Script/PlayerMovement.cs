using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D RigidBody;
    Animator anim;

    [SerializeField] private float MoveSpeed = 5.0f;
    [SerializeField] private float DiagonalMoveSpeed = 0.7f;

    private bool IsMoving;

    private float Horizontal;
    private float Vertical;
    
    private void Start()
    {
        RigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        Vertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (RigidBody.velocity.magnitude > 0 || RigidBody.velocity.magnitude < 0)
        {
            anim.SetBool("IsMoving", true);
        }
        else
            anim.SetBool("IsMoving", false);

        if (Horizontal != 0 && Vertical != 0)
        {
            Horizontal *= DiagonalMoveSpeed;
            Vertical *= DiagonalMoveSpeed;
        }

        RigidBody.velocity = new Vector2(Horizontal * MoveSpeed, Vertical * MoveSpeed);
    }
}
