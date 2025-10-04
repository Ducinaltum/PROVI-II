using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class KeyMaster : MonoBehaviour
{
    [SerializeField] private KeysSpritesLookup m_keysSpritesLookup;
    private Dictionary<EKeys, Key> m_keys = new();
    private HashSet<Door> m_doors = new();
    private EKeys m_pickedKeys;
    public UnityEvent<Key> OnKeyRegistered;
    public UnityEvent<Key> OnKeyPickedUp;

    public EKeys PickedKeys => m_pickedKeys;

    void Awake()
    {
        ServiceLocator.RegisterService(this);
    }

    void OnDestroy()
    {
        ServiceLocator.UnregisterService<KeyMaster>();
    }

    public void RegisterKey(Key key)
    {
        if (!m_keys.ContainsKey(key.ID))
        {
            m_keys.Add(key.ID, key);
            key.OnKeyPickedUp.AddListener(CheckDoorsState);
            OnKeyRegistered?.Invoke(key);
        }
        else
        {
            Debug.LogError($"The key {key.ID} is already registered review the scene");
        }
    }

    public void RegisterDoor(Door door)
    {
        Debug.Assert(!m_doors.Contains(door), $"The door {door.gameObject.name} is already registered review the scene", door.gameObject);
        m_doors.Add(door);
    }

    private void CheckDoorsState(Key key)
    {
        m_pickedKeys |= key.ID;
        foreach (Door door in m_doors)
        {
            if (door.GetIsUnlocked(m_pickedKeys))
            {
                m_doors.Remove(door);
            }
        }
        OnKeyPickedUp?.Invoke(key);
    }
}
