using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DamageReceiver : MonoBehaviour
{
    [SerializeField] private int m_maxHealth;
    [SerializeField] private float m_invulnerabilityDuration = 1.0f;
    private int m_currentHealth;
    private bool m_isInvulnerable;
    private WaitForSeconds m_waiter;

    public UnityEvent OnDamageRecieved;
    public UnityEvent OnDeath;

    public float CurrentRatio => (float)m_currentHealth / m_maxHealth;

    void Awake()
    {
        ServiceLocator.RegisterService(this);
    }
    void OnDestroy()
    {
        ServiceLocator.UnregisterService<DamageReceiver>();
    }

    void Start()
    {
        m_currentHealth = m_maxHealth;
        m_waiter = new WaitForSeconds(m_invulnerabilityDuration);
    }


    public void RecieveDamage()
    {
        if (m_currentHealth > 0)
        {
            if (!m_isInvulnerable)
            {
                m_currentHealth--;
                OnDamageRecieved?.Invoke();
                if (m_currentHealth <= 0)
                {
                    OnDeath?.Invoke();
                }
                else
                {
                    m_isInvulnerable = true;
                    StartCoroutine(WaitAndDeactivateInvulnerability());
                }
            }
        }
    }

    IEnumerator WaitAndDeactivateInvulnerability()
    {
        yield return m_waiter;
        m_isInvulnerable = false;
    }
}
