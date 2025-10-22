using System;
using UnityEngine;

//The old and reliable facade
public class Character : MonoBehaviour
{
    [SerializeField] private int m_defaultMaxHealth = 5;
    [SerializeField] private Animator m_animator;
    [SerializeField] private DamageReceiver m_damageReceiver;
    [SerializeField] private BookKeeper m_bookKeeper;
    [SerializeField] private Mover m_mover;
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
        m_mover.OnJump.AddListener(OnCharacterJump);
        m_mover.OnLanded.AddListener(OnCharacterLanded);
        m_damageReceiver.OnDamageRecieved.AddListener(OnDamageRecieved);
    }

    private void OnDamageRecieved() => m_animator.SetTrigger("take_damage");
    public void OnCharacterJump() => m_animator.SetTrigger("jump");
    public void OnCharacterLanded() => m_animator.SetTrigger("on_grounded");

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

    void LateUpdate()
    {
        m_animator.SetFloat("speed", m_mover.Speed);
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
        SoundPlayer.Instance.PlaySound(SoundKeys.COLLECT_COIN);
        m_bookKeeper.RecieveCurrency(collectibleValue);
    }
}
