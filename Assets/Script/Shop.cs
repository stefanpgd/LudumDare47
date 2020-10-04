using UnityEngine;
using System.Collections.Generic;
#pragma warning disable 649

public class Shop : MonoBehaviour
{
    private ResourceManager resourceManager;
    private ResourceManager ResourceManager => resourceManager ?? ResourceManager.Instance;

    private PlayerHealth playerHealth;
    private PlayerHealth PlayerHealth => playerHealth ?? PlayerHealth.Instance;

    private bool IsPlayerNear;
    [SerializeField] private List<Animator> animators;

    [SerializeField] private int healthPotionCost;
    [SerializeField] private int extraHearthCost;

    private void Update()
    {
        if(IsPlayerNear)
        {
            if(ResourceManager.GetResourceValue() >= healthPotionCost)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    PlayerHealth.RestoreToMaxHealth();
                    ResourceManager.RemoveResource(ResourceType.Gold, healthPotionCost);
                }
            }

            if (ResourceManager.GetResourceValue() >= extraHearthCost)
            {
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    PlayerHealth.IncreaseMaxHealth(1);
                    PlayerHealth.RestoreToMaxHealth();
                    ResourceManager.RemoveResource(ResourceType.Gold, extraHearthCost); 
                }
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

            foreach(Animator anim in animators)
            {
                anim.SetBool("show", true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            IsPlayerNear = false;

            foreach (Animator anim in animators)
            {
                anim.SetBool("show", false);
            }
        }
    }
}
