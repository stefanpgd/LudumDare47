using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MainMenu : MonoBehaviour
{
    //[SerializeField] private PlayerHealth playerHealth;
    //[SerializeField] private ResourceManager resourceManager;
    //[SerializeField] private ResourceManager ResourceManager => resourceManager ?? ResourceManager.Instance;

    [SerializeField] Transform m_room1transf;
    [SerializeField] Animator m_levelanimator;
    [SerializeField] List<GameObject> m_gamestartactivate;
    [SerializeField] List<GameObject> m_mainmenu;
    [SerializeField] float m_animationspeedmultiplier;
    [SerializeField] float m_maxreversespeed;
    [SerializeField] AudioSource m_menusoundtrack;
    [SerializeField] AudioSource m_backwardsclockticking;
    [SerializeField] AudioSource m_clockstroke;

    public bool m_gamehasstarted;

    public void Update()
    {
        //if (resourceManager == null )
        //{
        //    resourceManager = GameObject.FindGameObjectWithTag("Player").GetComponent<ResourceManager>();
        //}
        //if (playerHealth == null )
        //{
        //    playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        //}
    }

    public void ReverseRooms()
    {
        m_menusoundtrack.Stop();
        m_clockstroke.Play();
        StartCoroutine("PlayAndWaitForAnim");
    }

    public IEnumerator PlayAndWaitForAnim()
    {
        if (m_gamehasstarted == false)
        {
            yield return new WaitForSeconds(1);

            while ((m_levelanimator.GetCurrentAnimatorStateInfo(0).normalizedTime) % 1 < 0.99f)
            {
                if (m_animationspeedmultiplier > 0)
                {
                    m_animationspeedmultiplier = m_animationspeedmultiplier - 0.2f;
                    m_levelanimator.SetFloat("RoomTurnSpeed", m_animationspeedmultiplier);
                    if (m_room1transf.position == Vector3.zero)
                    {
                        m_levelanimator.SetFloat("RoomTurnSpeed", 0f);
                    }
                    yield return new WaitForSeconds(0.2f);
                }
                else
                {
                    if (!m_backwardsclockticking.isPlaying)
                    {
                        m_backwardsclockticking.Play();
                    }

                    m_animationspeedmultiplier = m_animationspeedmultiplier - 0.05f;
                    m_levelanimator.SetFloat("RoomTurnSpeed", m_animationspeedmultiplier);
                    if (m_room1transf.position == Vector3.zero)
                    {
                        m_levelanimator.SetFloat("RoomTurnSpeed", 0f);
                        //Back to start?. Start game!
                        StartGame();
                    }
                    //Debug.Log("Time: " + m_levelanimator.GetCurrentAnimatorStateInfo(0).normalizedTime);
                }

                yield return null;
            }
        }
    }

    public void StartGame()
    {
        m_backwardsclockticking.Stop();

        foreach(GameObject obj in m_gamestartactivate)
        {
            obj.SetActive(true);
        }

        foreach (GameObject obj in m_mainmenu)
        {
            obj.SetActive(false);
        }

        //if (resourceManager == null)
        //{
        //    resourceManager = GameObject.FindGameObjectWithTag("Player").GetComponent<ResourceManager>();
        //}
        //if (playerHealth == null)
        //{
        //    playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        //}

        //
        StopCoroutine("PlayAndWaitForAnim");
        m_gamehasstarted = true;
        m_levelanimator.SetFloat("RoomTurnSpeed", 1f);
        m_animationspeedmultiplier = 1f;

        ////playerHealth stuff
        //playerHealth.health = 5;
        //playerHealth.m_EndScreen.SetActive(false);
        //playerHealth.m_PlayerUI.SetActive(true);

        //playerHealth.PlayerHasDied = false;  
        //Cursor.visible = false;

        //playerHealth.GetComponent<PlayerMovement>().enabled = true;
        //playerHealth.GetComponent<Weapon>().enabled = true;

        ////Resources stuff
        //resourceManager.ResetAllResources();
    }

    //IEnumerator ReverseTime()
    //{
    //    if (m_room1transf.position == Vector3.zero)
    //    {
    //        m_levelanimator.SetFloat("RoomTurnSpeed", 0f);
    //    }

    //    while (m_room1transf.position != Vector3.zero)
    //    {
    //        if (m_animationspeedmultiplier > 0)
    //        {
    //            m_animationspeedmultiplier = m_animationspeedmultiplier - 0.05f;
    //            m_levelanimator.SetFloat("RoomTurnSpeed", m_animationspeedmultiplier);
    //            if (m_room1transf.position == Vector3.zero)
    //            {
    //                m_levelanimator.SetFloat("RoomTurnSpeed", 0f);
    //            }
    //            yield return new WaitForSeconds(0.2f);
    //        }
    //        else
    //        {
    //            m_animationspeedmultiplier = m_animationspeedmultiplier - 0.1f;
    //            m_levelanimator.SetFloat("RoomTurnSpeed", m_animationspeedmultiplier);
    //            if (m_room1transf.position == Vector3.zero)
    //            {
    //                m_levelanimator.SetFloat("RoomTurnSpeed", 0f);
    //            }
    //            yield return new WaitForSeconds(0.05f);
    //            Debug.Log("Time: " + m_levelanimator.GetCurrentAnimatorStateInfo(0).normalizedTime);
    //        }
    //    }
    //}
}
