using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] private GameObject m_TotalView;
    [SerializeField] private float m_TotalViewTime;

    private float m_Delay;
    private GameObject m_RoomView;

    // Start is called before the first frame update
    void Start()
    {
        m_TotalView.SetActive(false);
    }

    public void NextRoom(GameObject camera)
    {
        m_Delay = 0;
        m_RoomView = camera;

        m_TotalView.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_RoomView != null)
        {
            if (m_Delay < m_TotalViewTime)
            {
                m_Delay += 1 * Time.deltaTime;
            }

            else
            {
                m_TotalView.SetActive(false);
                m_RoomView.SetActive(true);

                m_RoomView = null;
            }
        }
    }
}
