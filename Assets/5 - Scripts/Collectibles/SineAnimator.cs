using UnityEngine;

public class SineAnimator : ColectibleAnimator
{
    [SerializeField] private float m_frequency = 1f;
    [SerializeField] private float m_amplitude = 1f;
    [SerializeField] private bool m_randomOffset = true;
    private float m_startYPosition;
    private float m_offset;
    private float m_eTime;
    private Vector3 position;

    private void Awake()
    {
        if (m_randomOffset)
        {
            m_eTime = Random.value * m_frequency;
        }
        m_startYPosition = transform.position.y;
        position = transform.position;
    }

    private void Update()
    {
        float ammount = Mathf.Sin(Mathf.PI * m_eTime * m_frequency) * m_amplitude;
        position.y = m_startYPosition + ammount;
        transform.position = position;
        m_eTime += Time.deltaTime;
    }
}
