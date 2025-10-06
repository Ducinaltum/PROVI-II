using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class KeyMaster : MonoBehaviour
{
    private static KeyMaster m_instance;
    public static KeyMaster Instance
    {
        get
        {
            if (m_instance == default)
            {
                GameObject obj = new(nameof(KeyMaster));
                m_instance = obj.AddComponent<KeyMaster>();
            }
            return m_instance;
        }
    }
    [SerializeField] private KeysSpritesLookup m_keysSpritesLookup;
    private Dictionary<EKeys, Key> m_keys = new();
    private HashSet<Door> m_doors = new();
    private HashSet<Door> m_openedDoors = new();
    private HashSet<Door> m_closedDoor = new();
    private EKeys m_pickedKeys;

    public UnityEvent<EKeys> OnKeyPickedUp = new();
    public EKeys PickedKeys => m_pickedKeys;

#if UNITY_EDITOR
    int doorIndex = 0;
#endif


    void Awake()
    {
        if (m_instance != null && m_instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            m_instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

#if UNITY_EDITOR
//Debug Tools
    void Update()
    {
        if (Input.GetKey(KeyCode.F7))
        {
            //Travel To door
            if (Input.GetKeyDown(KeyCode.D))
            {
                var player = FindFirstObjectByType<Mover>();
                var doors = m_doors.ToList();
                Debug.Assert(doors.Count > 0, "There are no doors in the level");

                if (doorIndex >= doors.Count)
                    doorIndex = 0;
                player.transform.position = doors[doorIndex].transform.position + (Vector3.up * 2);
                doorIndex++;
            }
            //Travel To Key
            if (Input.GetKeyDown(KeyCode.K))
            {
                var player = FindFirstObjectByType<Mover>();
                var keys = m_keys.ToList();
                Debug.Assert(keys.Count > 0, "There are no doors in the level");

                if (doorIndex >= keys.Count)
                    doorIndex = 0;
                player.transform.position = keys[doorIndex].Value.transform.position + (Vector3.up * 2);
                doorIndex++;
            }
        }
    }
#endif

    public void TravelToDoor(string doorTargetID)
    {
        Door door = m_doors.Where(d => d.Configuration.GetTargetSceneName() == doorTargetID).FirstOrDefault();
        if (door != default)
        {
            //TODO: Reference the player in other way
            var player = FindFirstObjectByType<Mover>();
            player.transform.position = door.transform.position + (Vector3.up * 2);
        }
    }

    public void RegisterKey(Key key)
    {
        if (!m_keys.ContainsKey(key.ID))
        {
            m_keys.Add(key.ID, key);
            key.OnKeyPickedUp.AddListener(CheckDoorsState);
        }
        else
        {
            Debug.LogError($"The key {key.ID} is already registered in the scene", key.gameObject);
        }
    }

    internal void UnregisterKey(Key key)
    {
        if (m_keys.ContainsKey(key.ID))
        {
            m_keys.Remove(key.ID);
            key.OnKeyPickedUp.AddListener(CheckDoorsState);
        }
        else
        {
            Debug.LogError($"The key {key.ID} is was not registered in the scene");
        }
    }

    public void RegisterDoor(Door door)
    {
        Debug.Assert(!m_doors.Contains(door), $"The door {door.gameObject.name} is already registered in the scene", door.gameObject);
        if (!door.GetIsUnlocked(m_pickedKeys))
        {
            m_openedDoors.Add(door);
        }
        else
        {
            m_closedDoor.Add(door);
        }
        m_doors.Add(door);
    }

    public void UnregisterDoor(Door door)
    {
        Debug.Assert(m_doors.Contains(door), $"The door {door.gameObject.name} is not registered the scene", door.gameObject);
        if (m_doors.Contains(door))
        {
            m_doors.Remove(door);
        }
        if (m_openedDoors.Contains(door))
        {
            m_doors.Remove(door);
        }
        else if (m_closedDoor.Contains(door))
        {
            m_doors.Remove(door);
        }
    }

    private void CheckDoorsState(Key key)
    {
        m_pickedKeys |= key.ID;
        foreach (Door door in m_doors)
        {
            if (door.GetIsUnlocked(m_pickedKeys))
            {
                m_closedDoor.Remove(door);
                m_openedDoors.Add(door);
            }
        }
        OnKeyPickedUp?.Invoke(key.ID);
    }
}
