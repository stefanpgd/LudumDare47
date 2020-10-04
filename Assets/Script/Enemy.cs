using UnityEngine;
#pragma warning disable 649

public class Enemy : MonoBehaviour
{
    Rigidbody2D RigidBody;
    Animator anim;

    public float EnemyHealth = 3.0f;

    [SerializeField] private Transform ShootPosition;
    [SerializeField] private Transform Target;
    [SerializeField] private GameObject Projectile;
    [SerializeField] private GameObject AttackTrigger;
    [SerializeField] private bool Ranged;

    private bool IsAttacking;
    private bool WasInRange;
    
    [SerializeField] private float AttackAnimationDuration = 1.0f;
    [SerializeField] private float MoveSpeed = 4.5f;
    [SerializeField] private float StoppingDistance = 2.0f;
    [SerializeField] private float AttackDelay = 0.5f;
    [SerializeField] private float AttackHitboxAppear = 0.4f;

    private float StartAttackAnimationDuration;
    private float StartAttackDelay;

    private void Start()
    {
        if (Target == null)
            Target = GameObject.FindGameObjectWithTag("Player").transform;

        if (anim == null)
            anim = GetComponent<Animator>();

        StartAttackAnimationDuration = AttackAnimationDuration;
        StartAttackDelay = AttackDelay;
    }

    private void Update()
    {
        if(EnemyHealth <= 0)
        {
            this.gameObject.SetActive(false);
            Debug.Log("Destroy enemy");
        }

        AttackDelay -= Time.deltaTime;

        if (Vector2.Distance(transform.position, Target.position) > StoppingDistance && IsAttacking == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, Target.position, MoveSpeed * Time.deltaTime);

        }
        else if (Vector2.Distance(transform.position, Target.position) < StoppingDistance && WasInRange == false)
        {
            transform.position = this.transform.position;
            AttackAnimationDuration = StartAttackAnimationDuration;
            WasInRange = true;
        }

        if (WasInRange && AttackAnimationDuration >= 0)
        {
            if(IsAttacking)
            {
                AttackAnimationDuration -= Time.deltaTime;
            }

            if (Ranged && AttackDelay < 0)
            {
                IsAttacking = true;
                anim.SetBool("IsAttacking", true);

                if (AttackAnimationDuration < 0)
                {
                    Instantiate(Projectile, ShootPosition.position, transform.rotation);
                    AttackDelay = StartAttackDelay;
                }
            }
            else if (!Ranged && AttackDelay < 0)
            {
                IsAttacking = true;
                anim.SetBool("IsAttacking", true);
                AttackDelay = StartAttackDelay;

                if (AttackAnimationDuration < AttackHitboxAppear)
                {
                    AttackTrigger.SetActive(true);
                }
            }

            if (AttackAnimationDuration < 0)
            {
                Debug.Log("anim attack false");

                anim.SetBool("IsAttacking", false);
                WasInRange = false;
                IsAttacking = false;
                AttackTrigger.SetActive(false);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Arrow")
        {
            EnemyHealth--;
            Destroy(collision.gameObject);
        }
    }
}
