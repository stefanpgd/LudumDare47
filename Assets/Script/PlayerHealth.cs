using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
#pragma warning disable 649

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private MainMenu mainMenu;
    public int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private float invulnerableTimer;

    [SerializeField] private List<GameObject> hearthSprites;
    [SerializeField] private List<GameObject> emptyHearthSprites;

    public GameObject m_EndScreen, m_PlayerUI;

    private float StartInvulnerableTimer;
    private bool CanTakeDamage;
    public bool PlayerHasDied;

    #region Singleton

    public static PlayerHealth Instance;

    private void Awake() => Instance = this;

    #endregion

    private void Start()
    {
        StartInvulnerableTimer = invulnerableTimer;
    }

    private void Update()
    {
        for(int i = 0; i < hearthSprites.Count; i++)
        {
            if(i < health)
            {
                hearthSprites[i].gameObject.SetActive(true);
            }
            else
            {
                hearthSprites[i].gameObject.SetActive(false);
            }
        }

        for(int i = 0; i < emptyHearthSprites.Count; i++)
        { 
            if (i < maxHealth)
            {
                emptyHearthSprites[i].gameObject.SetActive(true);
            }
            else
            {
                emptyHearthSprites[i].gameObject.SetActive(false);
            }
        }

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

            if(health <= 0f)
            {
                m_EndScreen.SetActive(true);
                m_PlayerUI.SetActive(false);

                PlayerHasDied = true;
                Cursor.visible = true;

                mainMenu.m_gamehasstarted = false;
                GetComponent<PlayerMovement>().enabled = false;
                GetComponent<Weapon>().enabled = false;
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
