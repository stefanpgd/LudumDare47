using UnityEngine;
using TMPro;
#pragma warning disable 649

public class PlayerHealth : MonoBehaviour
{
    public TextMeshProUGUI HealthText;

    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private float invulnerableTimer;

    private float StartInvulnerableTimer;
    private bool CanTakeDamage;

    #region Singleton

    public static PlayerHealth Instance;

    private void Awake() => Instance = this;

    #endregion

    private void Start()
    {
        StartInvulnerableTimer = invulnerableTimer;
        HealthText.text = "" + health;
    }

    private void Update()
    {
        if (invulnerableTimer > 0)
        {
            invulnerableTimer -= 1 * Time.deltaTime;
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
            invulnerableTimer = StartInvulnerableTimer;
            CanTakeDamage = false;
            health--;
            HealthText.text = "" + health;

            if(health <= 0f)
            {
                Debug.LogError("Player died...");
            }
        }
    }

    public void IncreaseMaxHealth(int value) => maxHealth += value;

    public void RestoreToMaxHealth() => health = maxHealth;

    public void RestoreHealth(int value)
    {
        health += value;
        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }
}
