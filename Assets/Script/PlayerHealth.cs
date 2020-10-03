using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float Health;
    public TextMeshProUGUI HealthText;

    private float StartInvulnerableTimer;
    [SerializeField] private float InvulnerableTimer;

    private bool CanTakeDamage;

    void Start()
    {
        StartInvulnerableTimer = InvulnerableTimer;
        HealthText.text = "" + Health;
    }

    void Update()
    {
        if (InvulnerableTimer > 0)
        {
            InvulnerableTimer -= 1 * Time.deltaTime;
        }
        else
        {
            CanTakeDamage = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && CanTakeDamage)
        {
            InvulnerableTimer = StartInvulnerableTimer;
            CanTakeDamage = false;
            Health--;
            HealthText.text = "" + Health;
        }
    }
}
