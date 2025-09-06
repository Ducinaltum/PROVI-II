using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private EKeys m_unlockerKeys;
    [SerializeField] private GameObject m_openDoor;
    [SerializeField] private GameObject m_closedDoor;

    public EKeys UnlockerKeys => m_unlockerKeys;
    void Start()
    {
        if (ServiceLocator.TryGetService(out KeyMaster level))
        {
            level.RegisterDoor(this);
        }
    }

    public void Open()
    {
        m_openDoor.SetActive(true);
        m_closedDoor.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (ServiceLocator.TryGetService(out Level level))
            {
                level.OnDoorTrespassed(this);
            }
        }
    }
}