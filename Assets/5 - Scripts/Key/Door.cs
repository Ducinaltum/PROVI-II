using System;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private DoorConfiguration m_configuration;
    [SerializeField] private GameObject m_openDoor;
    [SerializeField] private GameObject m_closedDoor;
    [SerializeField] private FlagController m_flagPrefab;
    [SerializeField] private Transform[] m_flagPositions;
    private FlagController[] m_flags;
    private bool m_isUnlocked;
    public DoorConfiguration Configuration => m_configuration;

    void Awake()
    {
        List<EKeys> neededKeys = m_configuration.GetNeededKeys();
        m_flags = new FlagController[neededKeys.Count];
        for (int i = 0; i < neededKeys.Count; i++)
        {
            FlagController flag = Instantiate(m_flagPrefab, m_flagPositions[i]);
            flag.Initialize(neededKeys[i]);
            m_flags[i] = flag;
        }
        KeyMaster.Instance.RegisterDoor(this);
    }

    void OnDestroy()
    {
        KeyMaster.Instance.UnregisterDoor(this);        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.TryGetComponent(out Character player))
            {
                player.SetIsOnDoor(this, true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.TryGetComponent(out Character player))
            {
                player.SetIsOnDoor(this, false);
            }
        }
    }

    internal bool GetIsUnlocked(EKeys m_pickedKeys)
    {
        foreach (var flag in m_flags)
        {
            flag.SetUnlockedState(m_pickedKeys);
        }
        if (!m_isUnlocked && Configuration.GetIsUnlocked(m_pickedKeys))
        {
            SetOpen();
            m_isUnlocked = true;
        }
        else
        {
            m_isUnlocked = false;
        }
        return m_isUnlocked;
    }
    
    public void SetOpen()
    {
        m_openDoor.SetActive(true);
        m_closedDoor.SetActive(false);
    }
}