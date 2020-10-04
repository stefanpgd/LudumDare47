using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 649

public class DoorChecker : MonoBehaviour
{
    public Room room;
    public GameObject m_Black;
    public bool isInteractable = true;

    [SerializeField] private GameObject m_RoomCamera;
    [SerializeField] private CameraSwitch m_GameManager;

    private bool m_CanUse;
    private GameObject m_NextRoom;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            m_CanUse = true;
            m_NextRoom = collision.gameObject;
        }

        if (isInteractable)
        { 
            if (collision.gameObject.CompareTag("Player") && m_CanUse)
            {
                collision.gameObject.transform.position = m_NextRoom.transform.GetChild(0).transform.position;
                collision.gameObject.transform.parent = m_NextRoom.transform.parent;
                DoorChecker door = m_NextRoom.gameObject.GetComponent<DoorChecker>();
                door.m_Black.SetActive(false);
                door.room.EnableRoom();

                m_RoomCamera.SetActive(false);
                m_Black.SetActive(true);
                room.DisableRoom();

                m_GameManager.NextRoom(door.m_RoomCamera);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isInteractable)
        {
            if (collision.gameObject.CompareTag("Door"))
            {
                m_CanUse = false;
            }
        }
    }
}
