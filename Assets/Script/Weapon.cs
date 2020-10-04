using UnityEngine;
#pragma warning disable 649

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject crosshair;
    [SerializeField] private GameObject playerArm;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject projectileStart;
    [SerializeField] private float bulletSpeed = 20f;

    private Vector3 target;

    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        crosshair.transform.position = new Vector2(target.x, target.y);

        Vector3 difference = target - playerArm.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        playerArm.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

        if (Input.GetMouseButtonDown(0))
        {
            float distance = difference.magnitude;
            Vector2 direction = difference / distance;
            direction.Normalize();
            fireBullet(direction, rotationZ);
        }
    }

    void fireBullet(Vector2 direction, float rotationZ)
    {
        GameObject b = Instantiate(projectilePrefab, projectileStart.transform.position, Quaternion.identity, transform.parent);
        b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        b.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }
}
