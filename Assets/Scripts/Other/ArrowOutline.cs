using UnityEngine;

public class ArrowOutline : MonoBehaviour
{
    private int m_scalingDir = 0;
    private Vector3 m_originalSize;

    private const float m_deltaScale = 0.25f;

    private void Start()
    {
        m_originalSize = transform.localScale;
    }

    private void Update()
    {
        if (m_scalingDir == 0)
        {
            if (transform.localScale.x >= m_originalSize.x + m_deltaScale)
            {
                m_scalingDir = 1;
            }
            else
            {
                transform.localScale += Vector3.one * m_deltaScale / 2 * Time.deltaTime;
            }
        }
        else if (m_scalingDir == 1)
        {
            if (transform.localScale.x <= m_originalSize.x - m_deltaScale)
            {
                m_scalingDir = 0;
            }
            else
            {
                transform.localScale -= Vector3.one * m_deltaScale / 2f * Time.deltaTime;
            }
        }
    }
}