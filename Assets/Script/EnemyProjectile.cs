using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float ProjectileSpeed;
    [SerializeField] private Transform player;
    [SerializeField] private Vector2 target;

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;

            target = new Vector2(player.position.x, player.position.y);
        }
    }
    
    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, ProjectileSpeed * Time.deltaTime);

        void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag("Player"))
            {
                DestroyProjectile();
                Debug.Log("hithtithit");

            }
        }

        void DestroyProjectile()
        {
            Destroy(gameObject);
        }
    }
}
