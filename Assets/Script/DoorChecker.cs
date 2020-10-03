using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorChecker : MonoBehaviour
{
    [SerializeField] private GameObject m_RoomCamera;

    private bool m_CanUse;
    private GameObject m_NextRoom;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            m_CanUse = true;
            m_NextRoom = collision.gameObject;
        }

        if (collision.gameObject.CompareTag("Player") && m_CanUse)
        {
            collision.gameObject.transform.position = m_NextRoom.transform.GetChild(0).transform.position;
            collision.gameObject.transform.parent = m_NextRoom.transform.parent;

            m_RoomCamera.SetActive(false);
            m_NextRoom.GetComponent<DoorChecker>().m_RoomCamera.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            m_CanUse = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
