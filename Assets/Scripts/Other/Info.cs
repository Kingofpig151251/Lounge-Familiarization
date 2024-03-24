using UnityEngine;

public class Info : MonoBehaviour
{
    private int m_movingDir = 0;
    private Vector3 m_originalPos;

    private const float m_displacement = 0.05f;

    private void Start()
    {
        m_originalPos = transform.position;
    }

    private void Update()
    {
        if (m_movingDir == 0)
        {
            if (transform.position.y >= m_originalPos.y + m_displacement)
            {
                m_movingDir = 1;
            }
            else
            {
                transform.position += Vector3.up * m_displacement / 2 * Time.deltaTime;
            }
        }
        else if (m_movingDir == 1)
        {
            if (transform.position.y <= m_originalPos.y - m_displacement)
            {
                m_movingDir = 0;
            }
            else
            {
                transform.position += Vector3.down * m_displacement / 2f * Time.deltaTime;
            }
        }

        transform.LookAt(Camera.main.gameObject.transform);
        transform.eulerAngles = new Vector3(90f, transform.eulerAngles.y, transform.eulerAngles.z);
    }
}