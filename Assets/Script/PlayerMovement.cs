using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D RigidBody;

    [SerializeField] private float MoveSpeed = 5.0f;
    [SerializeField] private float DiagonalMoveSpeed = 0.7f;

    private float Horizontal;
    private float Vertical;
    
    private void Start()
    {
        RigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        Vertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (Horizontal != 0 && Vertical != 0)
        {
            Horizontal *= DiagonalMoveSpeed;
            Vertical *= DiagonalMoveSpeed;
        }

        RigidBody.velocity = new Vector2(Horizontal * MoveSpeed, Vertical * MoveSpeed);
    }
}
