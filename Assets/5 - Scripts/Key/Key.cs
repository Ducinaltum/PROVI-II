using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Key : MonoBehaviour
{
    [SerializeField] private EKeys m_ID;
    [SerializeField] private SpriteRenderer m_spriteRenderer;

    public EKeys ID => m_ID;
    public UnityEvent<Key> OnKeyPickedUp;

    private void Start()
    {
        if (ServiceLocator.TryGetService(out KeyMaster level))
        {
            level.RegisterKey(this);
        }
    }

    public void SetSprite(Sprite sprite)
    {
        m_spriteRenderer.sprite = sprite;
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