using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DamageReceiver : MonoBehaviour
{
    [SerializeField] private float m_invulnerabilityDuration = 1.0f;
    private HealthData m_healthData;
    private bool m_isInvulnerable;
    private WaitForSeconds m_waiter;

    public int MaxHealth => m_healthData.MaxHealth;
    public int CurrentHealth => m_healthData.CurrentHealth;
    public UnityEvent OnDamageRecieved;
    public UnityEvent OnDeath;


    void Awake()
    {
        ServiceLocator.RegisterService(this);
    }

    void OnDestroy()
    {
        ServiceLocator.UnregisterService<DamageReceiver>();
    }

    public void Initialize(SessionData m_sessionData)
    {
        m_healthData = m_sessionData.HealthData;
        m_waiter = new WaitForSeconds(m_invulnerabilityDuration);
    }


    public void RecieveDamage()
    {
        if (m_healthData.CurrentHealth > 0)
        {
            if (!m_isInvulnerable)
            {
                m_healthData.TakeDamage();
                OnDamageRecieved?.Invoke();
                if (m_healthData.IsDead)
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
