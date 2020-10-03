using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Vector3 mousePosition;
    public Transform playerArm;
    public GameObject bullet;
    public GameObject crosshair;
    public Transform firePoint;
    public float bulletSpeed = 5f;

    private void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        crosshair.transform.position = new Vector3(mousePosition.x, mousePosition.y);
        Vector3 difference = mousePosition - playerArm.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        playerArm.rotation = Quaternion.Euler(0f, 0f, rotationZ);

        if (Input.GetMouseButtonDown(0))
        {
            float distance = difference.magnitude;
            Vector2 direction = difference / distance;
            direction.Normalize();
            Fire(direction, rotationZ);
        }
    }
    private void Fire(Vector2 dir, float RotationZ)
    {
        GameObject b = Instantiate(bullet, firePoint.position, Quaternion.identity);
        b.transform.rotation = Quaternion.Euler(0f, 0f, RotationZ);
        b.GetComponent<Rigidbody2D>().velocity = dir * bulletSpeed;
    }
}
