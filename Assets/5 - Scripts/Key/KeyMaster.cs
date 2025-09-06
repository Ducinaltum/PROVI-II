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
        Debug.Assert(!m_keys.ContainsKey(key.ID), $"The key {key.ID} is already registered review the scene");
        m_keys.Add(key.ID, key);
        key.SetSprite(m_keysSpritesLookup.GetGameSprite(key.ID));
        key.OnKeyPickedUp.AddListener(CheckDoorsState);
        OnKeyRegistered?.Invoke(key);
    }

    public void RegisterDoor(Door door)
    {
        Debug.Assert(!m_doors.Contains(door), $"The door {door.gameObject.name} is already registered review the scene", door.gameObject);
        m_doors.Add(door);
    }

    private void CheckDoorsState(Key key)
    {
        m_pickedKeys |= key.ID;
        Door[] unlockedDoors = m_doors.Where(d => (d.UnlockerKeys & m_pickedKeys) == d.UnlockerKeys).ToArray();
        foreach (Door door in unlockedDoors)
        {
            door.Open();
            m_doors.Remove(door);
        }
    }
}
