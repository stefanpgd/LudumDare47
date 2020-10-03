using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Transform m_room1transf;
    [SerializeField] Animator m_levelanimator;
    [SerializeField] List<GameObject> m_gamestartactivate;
    [SerializeField] List<GameObject> m_mainmenu;
    [SerializeField] float m_animationspeedmultiplier;
    [SerializeField] float m_maxreversespeed;

    void OnEnable()
    {
        m_animationspeedmultiplier = 0f;
    }

    public void ReverseRooms()
    {
        StartCoroutine(PlayAndWaitForAnim());
    }

    public IEnumerator PlayAndWaitForAnim()
    {
        //Now, Wait until the current state is done playing
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
                m_animationspeedmultiplier = m_animationspeedmultiplier - 0.05f;
                m_levelanimator.SetFloat("RoomTurnSpeed", m_animationspeedmultiplier);
                if (m_room1transf.position == Vector3.zero)
                {
                    m_levelanimator.SetFloat("RoomTurnSpeed", 0f);
                    //Back to start?. Start game!
                    StartGame();
                }
                Debug.Log("Time: " + m_levelanimator.GetCurrentAnimatorStateInfo(0).normalizedTime);
            }

            yield return null;
        }
    }

    public void StartGame()
    {
        foreach(GameObject obj in m_gamestartactivate)
        {
            obj.SetActive(true);
        }

        foreach (GameObject obj in m_mainmenu)
        {
            obj.SetActive(false);
        }

        m_levelanimator.SetFloat("RoomTurnSpeed", 1f);
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
