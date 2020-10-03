using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] private GameObject m_TotalView;
    [SerializeField] private GameObject[] m_RoomViews;

    private int m_CurrentRoom;

    // Start is called before the first frame update
    void Start()
    {
        m_TotalView.SetActive(false);

        for (int i = 0; i < m_RoomViews.Length; i++)
        {
            m_RoomViews[i].SetActive(false);
        }

        m_RoomViews[m_CurrentRoom].SetActive(true);
    }

    public void NextRoom()
    {
        m_RoomViews[m_CurrentRoom].SetActive(false);

        m_CurrentRoom++;

        if (m_CurrentRoom > m_RoomViews.Length)
        {
            m_CurrentRoom = 0;
        }

        m_RoomViews[m_CurrentRoom].SetActive(true);

        //m_TotalView.SetActive(true);
    }

    public void PreviousRoom()
    {
        m_RoomViews[m_CurrentRoom].SetActive(false);

        m_CurrentRoom--;

        if (m_CurrentRoom < m_RoomViews.Length)
        {
            m_CurrentRoom = m_RoomViews.Length;
        }

        m_RoomViews[m_CurrentRoom].SetActive(true);

        //m_TotalView.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
