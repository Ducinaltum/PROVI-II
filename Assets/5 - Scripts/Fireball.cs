using UnityEngine;

public class Fireball : MonoBehaviour, IPoolable
{
    [SerializeField] private float m_speed = 2.0f;
    [SerializeField] private float m_rotationSpeed = 15.0f;
    [SerializeField] private Transform m_body;
    float m_currentSpeed;
    public void Dispose()
    {
        gameObject.SetActive(false);
        ObjectPool<Fireball>.Instance.DisposeObject(this);
    }

    public void Initialize(Vector3 spawnPos, float spawnDistance, float direction)
    {
        m_currentSpeed = m_speed * direction;
        spawnPos.x += spawnDistance * direction;
        transform.position = spawnPos;
        gameObject.SetActive(true);
    }

    void Update()
    {
        transform.Translate(Vector3.right * m_currentSpeed * Time.deltaTime);
        m_body.Rotate(Vector3.forward * m_rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Dispose();
    }
}
