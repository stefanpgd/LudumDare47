using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D RigidBody;

    [SerializeField] private Transform ShootPosition;
    [SerializeField] private Transform Target;
    [SerializeField] private GameObject Projectile;
    [SerializeField] private bool Ranged;

    [SerializeField] private float MoveSpeed = 4.5f;
    [SerializeField] private float StoppingDistance = 2.0f;
    [SerializeField] private float ProjectileSpeed = 3.0f;
    [SerializeField] private float ShootDelay = 2.0f;

    private float StartShootDelay;

    private void Start()
    {
        if (Target == null)
            Target = GameObject.FindGameObjectWithTag("Player").transform;

        StartShootDelay = ShootDelay;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, Target.position) > StoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, Target.position, MoveSpeed * Time.deltaTime);
        }
    }
}
