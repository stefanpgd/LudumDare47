using UnityEngine;
#pragma warning disable 649

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float projectileSpeed;
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
    
    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, projectileSpeed * Time.deltaTime);
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DestroyProjectile();
            Debug.Log("hithtithit");

        }
    }
}
