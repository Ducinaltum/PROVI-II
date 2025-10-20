using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private int m_collectibleValue = 1;
    [SerializeField] private ColectibleAnimator[] m_colectibleAnimators;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.TryGetComponent(out Character player))
            {
                player.Collect(m_collectibleValue);
                Destroy(gameObject);
            }
        }
    }

    void OnBecameVisible()
    {
        foreach (var animator in m_colectibleAnimators)
        {
            animator.enabled = true;
        }
    }

    void OnBecameInvisible()
    {
        foreach (var animator in m_colectibleAnimators)
        {
            animator.enabled = false;
        }
    }
}
