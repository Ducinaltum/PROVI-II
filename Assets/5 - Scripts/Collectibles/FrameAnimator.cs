using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(SpriteRenderer))]
public class CoinAnimator : ColectibleAnimator
{
    [SerializeField] private SpriteRenderer m_renderer;
    [SerializeField] private Sprite[] m_frames;
    [SerializeField] private float m_frameRate = 4f; // frames per second
    [SerializeField] private bool m_randomOffset = true;

    private float m_offset;
    private float m_eTime;

    private void Awake()
    {
        if (m_randomOffset)
            m_offset = Random.value;
    }

    private void Update()
    {
        int index = Mathf.FloorToInt((m_eTime + m_offset) * m_frameRate) % m_frames.Length;
        m_renderer.sprite = m_frames[index];
        m_eTime += Time.deltaTime;
    }
}