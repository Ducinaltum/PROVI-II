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
        Door[] unlockedDoors = m_doors.Where(d => d.Configuration.GetIsUnlocked(m_pickedKeys)).ToArray();
        foreach (Door door in unlockedDoors)
        {
            door.Open();
            m_doors.Remove(door);
        }
        OnKeyPickedUp?.Invoke(key);
    }
}
