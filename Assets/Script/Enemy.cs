using UnityEngine;
#pragma warning disable 649

public class Enemy : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    Rigidbody2D RigidBody;
    Animator anim;

    public float EnemyHealth = 3.0f;

    [SerializeField] private EnemyType enemyType;
    [SerializeField] private Transform ShootPosition;
    [SerializeField] private Transform Target;
    [SerializeField] private GameObject Projectile;
    [SerializeField] private GameObject AttackTrigger;
    [SerializeField] private bool Ranged;
    [SerializeField] private float projectileSpeed;

    private bool IsAttacking;
    private bool WasInRange;
    
    [SerializeField] private float AttackAnimationDuration = 1.0f;
    [SerializeField] private float MoveSpeed = 4.5f;
    [SerializeField] private float StoppingDistance = 2.0f;
    [SerializeField] private float AttackDelay = 0.5f;
    [SerializeField] private float AttackHitboxAppear = 0.4f;
    [SerializeField] private AudioSource audio;

    [SerializeField] private GameObject m_Bones, m_Blood, m_SlimeStuff;

    private float StartAttackAnimationDuration;
    private float StartAttackDelay;

    private ResourceManager resourceManager;
    private float slimeSoundTimer;

    private void Start()
    {
        if (playerHealth == null)
            playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

        if (Target == null)
            Target = GameObject.FindGameObjectWithTag("Player").transform;

        if (anim == null)
            anim = GetComponent<Animator>();

        StartAttackAnimationDuration = AttackAnimationDuration;
        StartAttackDelay = AttackDelay;

        resourceManager = ResourceManager.Instance;
    }

    private void Update()
    {
        float distanceX = transform.position.x - Target.position.x;

        if (!IsAttacking)
        {
            if (distanceX > 0)
            {
                transform.localScale = new Vector3(-0.3f, 0.3f, 0.3f);
            }
            else
            {
                transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            }
        }

        if(enemyType == EnemyType.Slime)
        {
            slimeSoundTimer += 1f * Time.deltaTime;

            if(slimeSoundTimer >= 1f)
            {
                audio.Play();
                slimeSoundTimer = 0f;
            }
        }

        if(EnemyHealth <= 0)
        {
            this.gameObject.SetActive(false);
            int coins = Random.Range(0, 4);
            resourceManager.AddResource(ResourceType.Gold, coins);
            resourceManager.AddResource(ResourceType.Kills, 1);
        }

        AttackDelay -= Time.deltaTime;

        if (Vector2.Distance(transform.position, Target.position) > StoppingDistance && IsAttacking == false && playerHealth.PlayerHasDied == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, Target.position, MoveSpeed * Time.deltaTime);

        }
        else if (Vector2.Distance(transform.position, Target.position) < StoppingDistance && WasInRange == false && playerHealth.PlayerHasDied == false)
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

            if (AttackAnimationDuration < 0 || playerHealth.PlayerHasDied)
            {
                anim.SetBool("IsAttacking", false);
                WasInRange = false;
                IsAttacking = false;

                if(!Ranged)
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

            if(enemyType == EnemyType.Skeleton)
            {
                
            }

            switch (enemyType)
            {
                case EnemyType.Skeleton:
                    Instantiate(m_Bones, transform.position, Quaternion.identity);
                    audio.Play();
                    break;
                case EnemyType.Slime:
                    Instantiate(m_SlimeStuff, transform.position, Quaternion.identity);
                    break;
                case EnemyType.FlyingEye:
                    Instantiate(m_Blood, transform.position, Quaternion.identity);
                    break;
            }
        }
    }

    public void EyeSpit()
    {
        Vector3 difference = Target.position - ShootPosition.position;
        float distance = difference.magnitude;
        Vector2 direction = difference / distance;
        direction.Normalize();

        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        GameObject b = Instantiate(Projectile, ShootPosition.position, Quaternion.identity, transform.parent);
        b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        b.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;

        if (enemyType == EnemyType.FlyingEye)
        {
            audio.Play();
        }

        AttackDelay = StartAttackDelay;
    }
}
