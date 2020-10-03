using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D RigidBody;
    [SerializeField] private GameObject Target;

    [SerializeField] private float MoveSpeed = 4.5f;

    private void Start()
    {
        if (Target == null)
            Target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, MoveSpeed * Time.deltaTime);
    }
}
