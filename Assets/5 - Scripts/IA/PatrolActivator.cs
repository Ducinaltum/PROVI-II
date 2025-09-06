using UnityEngine;

public class PatrolActivator : MonoBehaviour
{
    [SerializeField] private Patrol m_patrol;
    void Start()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            m_patrol.enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            m_patrol.enabled = false;
        }
    }


}
