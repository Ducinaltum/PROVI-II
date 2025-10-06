using System;
using UnityEngine;

//The old and reliable facade
public class Character : MonoBehaviour
{
    [SerializeField] private DamageReceiver m_damageReceiver;
    public DamageReceiver DamageReceiver => m_damageReceiver;
    private Door m_currentDoor;

    void Start()
    {
        if (ServiceLocator.TryGetService(out Level level))
        {
            level.RegisterCharacter(this);
        }
    }

    void Update()
    {
        if (m_currentDoor != null && Input.GetKeyDown(KeyCode.E))
        {
            if (ServiceLocator.TryGetService(out Level level))
            {
                level.OnDoorTrespassed(m_currentDoor);
            }
        }
    }

    internal void SetIsOnDoor(Door door, bool isOnDoor)
    {
        if (isOnDoor)
        {
            m_currentDoor = door;
        }
        else if(m_currentDoor == door)
        {
            m_currentDoor = default;
        }
    }
}
