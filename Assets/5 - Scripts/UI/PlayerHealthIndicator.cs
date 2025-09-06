using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthIndicator : MonoBehaviour
{
    [SerializeField] private Image m_bar;
    private DamageReceiver m_target;

    void Start()
    {
        if (ServiceLocator.TryGetService(out m_target))
        {
            m_target.OnDamageRecieved.AddListener(UpdateBar);
        }
        else
        {
            enabled = false;
        }
    }

    private void UpdateBar()
    {
        m_bar.fillAmount = m_target.CurrentRatio;
    }
}
