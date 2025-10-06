using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Key : MonoBehaviour
{
    [SerializeField] private KeysSpritesLookup m_keysSpritesLookup;
    [SerializeField] private EKeys m_ID;
    [SerializeField] private SpriteRenderer m_spriteRenderer;

    public EKeys ID => m_ID;
    public UnityEvent<Key> OnKeyPickedUp;

    private void Start()
    {
        KeyMaster.Instance.RegisterKey(this);
        m_spriteRenderer.color = m_keysSpritesLookup.GetColor(m_ID);
    }

    void OnDestroy()
    {
        KeyMaster.Instance.UnregisterKey(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            OnKeyPickedUp?.Invoke(this);
        }
    }

}