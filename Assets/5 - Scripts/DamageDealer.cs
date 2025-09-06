using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(collision.gameObject.TryGetComponent(out DamageReceiver reciever))
            {
                reciever.RecieveDamage();
            }
        }
    }
}
