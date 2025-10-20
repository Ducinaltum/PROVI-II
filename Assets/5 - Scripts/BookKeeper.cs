using System;
using UnityEngine;
using UnityEngine.Events;

public class BookKeeper : MonoBehaviour
{
    private EconomicData m_economicData;

    public UnityEvent<int> OnAmmountUpdated;


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
        m_economicData = m_sessionData.EconomicData;
    }

    public void RecieveCurrency(int collectibleValue)
    {
        m_economicData.AddCoins(collectibleValue);
        OnAmmountUpdated?.Invoke(m_economicData.CurrentCoins);
    }
}
