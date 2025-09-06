using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] private Transform[] m_points;
    [SerializeField] private float m_moveSpeed = 1.0f;
    private int m_currentPointIndex = 0;
    private Transform m_currentPoint = null;

    void Start()
    {
        m_currentPoint = m_points[m_currentPointIndex];
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, m_currentPoint.position, m_moveSpeed);
        if (Mathf.Approximately((transform.position - m_currentPoint.position).sqrMagnitude, 0))
        {
            m_currentPointIndex++;
            m_currentPointIndex %= m_points.Length;
            m_currentPoint = m_points[m_currentPointIndex];
        }

    }
}
