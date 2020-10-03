﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private ResourceManager resourceManager;
    private PlayerHealth playerHealth;
    private bool IsPlayerNear;

    private void Start()
    {
        resourceManager = ResourceManager.Instance;

    }

    private void Update()
    {
        if(IsPlayerNear)
        {
            // Health potion??
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                // playerHealth.RestoreHealth();
                // resourceManager.RemoveResource(ResourceType.Gold, 20f);
            }

            // Bonus Hearth??
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                // playerHealth.AddHearth();
                // resourceManager.RemoveResource(ResourceType.Gold, 50f);
            }

            // Strong Arrows??
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                //Weapon.AddStrength(..f);
                // resourceManager.RemoveResource(ResourceType.Gold, 50f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            IsPlayerNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            IsPlayerNear = false;
        }
    }
}