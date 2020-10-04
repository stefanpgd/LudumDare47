using UnityEngine;
#pragma warning disable 649

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject crosshair;
    [SerializeField] private GameObject playerArm;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject projectileStart;
    [SerializeField] private float ShootDelay = 0.4f;
    private float StartShootDelay;
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private AudioSource shootSound;

    [SerializeField] private SpriteRenderer m_CrossbowSprite;

    private Vector3 target;

    private SpriteRenderer m_Sprite;
    private Vector3 m_MousePos;
    private Animator m_Animator;

    void Start()
    {
        Cursor.visible = false;

        StartShootDelay = ShootDelay;

        m_Sprite = GetComponent<SpriteRenderer>();
        m_Animator = GetComponent<Animator>();
    }

    void Update()
    {
        ShootDelay -= 1 * Time.deltaTime;

        target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        crosshair.transform.position = new Vector2(target.x, target.y);

        Vector3 difference = target - playerArm.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        playerArm.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

        if (Input.GetMouseButton(0) && ShootDelay <= 0)
        {
            float distance = difference.magnitude;
            Vector2 direction = difference / distance;
            direction.Normalize();
            fireBullet(direction, rotationZ);

            ShootDelay = StartShootDelay;
        }

        m_MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (m_MousePos.x > transform.position.x)
        {
            m_Sprite.flipX = false;
            m_CrossbowSprite.flipY = false;
        }

        else
        {
            m_Sprite.flipX = true;
            m_CrossbowSprite.flipY = true;
        }

        if (m_MousePos.y > transform.position.y)
        {
            m_Animator.SetBool("Front", false);
            m_Sprite.sortingOrder = 1;
        }

        else
        {
            m_Animator.SetBool("Front", true);
            m_Sprite.sortingOrder = -1;
        }
    }

    void fireBullet(Vector2 direction, float rotationZ)
    {
        GameObject b = Instantiate(projectilePrefab, projectileStart.transform.position, Quaternion.identity, transform.parent);
        b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        b.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        shootSound.Play();
    }
}
