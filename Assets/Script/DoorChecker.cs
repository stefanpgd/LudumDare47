using UnityEngine;
#pragma warning disable 649

public class DoorChecker : MonoBehaviour
{
    public GameObject m_Black;

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

        if (collision.gameObject.CompareTag("Player") && m_CanUse)
        {
            collision.gameObject.transform.position = m_NextRoom.transform.GetChild(0).transform.position;
            collision.gameObject.transform.parent = m_NextRoom.transform.parent;
            m_NextRoom.gameObject.GetComponent<DoorChecker>().m_Black.SetActive(false);

            m_RoomCamera.SetActive(false);
            m_Black.SetActive(true);
            //m_NextRoom.GetComponent<DoorChecker>().m_RoomCamera.SetActive(true);

            m_GameManager.NextRoom(m_NextRoom.GetComponent<DoorChecker>().m_RoomCamera);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            m_CanUse = false;
        }
    }
}
