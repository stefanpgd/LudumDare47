using UnityEngine;
using System.Collections.Generic;
#pragma warning disable 649

public class Shop : MonoBehaviour
{
    private ResourceManager resourceManager;
    private PlayerHealth playerHealth;
    private bool IsPlayerNear;
    [SerializeField] private List<Animator> animators;

    private void Start()
    {
        resourceManager = ResourceManager.Instance;
        playerHealth = PlayerHealth.Instance;
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
