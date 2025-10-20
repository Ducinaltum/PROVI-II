using System;
using UnityEngine;

//The old and reliable facade
public class Character : MonoBehaviour
{
    [SerializeField] private int m_defaultMaxHealth = 5;
    [SerializeField] private DamageReceiver m_damageReceiver;
    [SerializeField] private BookKeeper m_bookKeeper;
    private Door m_currentDoor;
    private SessionData m_sessionData;
    public DamageReceiver DamageReceiver => m_damageReceiver;
    public BookKeeper BookKeeper => m_bookKeeper;

    void Awake()
    {
        //If safe to obtain this service here because SessionData is a pure class
        if (ServiceLocator.TryGetService(out SessionData sessionData))
        {
            m_sessionData = sessionData;
        }
        else
        {
            m_sessionData = new(m_defaultMaxHealth);
        }

        m_damageReceiver.Initialize(m_sessionData);
        m_bookKeeper.Initialize(m_sessionData);
    }

    void Start()
    {
        if (ServiceLocator.TryGetService(out Level level))
        {
            level.RegisterCharacter(this);
        }

    }

    void Update()
    {
        if (m_currentDoor != null && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.W)))
        {
            if (ServiceLocator.TryGetService(out Level level))
            {
                level.OnDoorTrespassed(m_currentDoor);
            }
        }
    }

    public void SetIsOnDoor(Door door, bool isOnDoor)
    {
        if (isOnDoor)
        {
            m_currentDoor = door;
        }
        else if (m_currentDoor == door)
        {
            m_currentDoor = default;
        }
    }

    public void Collect(int collectibleValue)
    {
        m_bookKeeper.RecieveCurrency(collectibleValue);
    }
}
