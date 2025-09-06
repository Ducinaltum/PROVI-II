using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthIndicator : MonoBehaviour
{
    [SerializeField] private GameObject m_healthIndicatorItemPrefab;
    private GameObject[] m_indicators;
    private DamageReceiver m_target;

    void Start()
    {
        if (ServiceLocator.TryGetService(out m_target))
        {
            m_target.OnDamageRecieved.AddListener(UpdateBar);
            m_indicators = new GameObject[m_target.MaxHealth];
            for (int i = 0; i < m_target.MaxHealth; i++)
            {
                m_indicators[i] = Instantiate(m_healthIndicatorItemPrefab, transform);
            }
        }
        else
        {
            enabled = false;
        }
    }

    private void UpdateBar()
    {
        for (int i = 0; i < m_indicators.Length; i++)
        {
            m_indicators[i].SetActive(i < m_target.CurrentHealth);
        }
    }
}
