using UnityEngine;

//The old and reliable facade
public class Character : MonoBehaviour
{
    [SerializeField] private DamageReceiver m_damageReceiver;

    public DamageReceiver DamageReceiver => m_damageReceiver;

    void Start()
    {
        if (ServiceLocator.TryGetService(out Level level))
        {
            level.RegisterCharacter(this);
        }
    }
}
