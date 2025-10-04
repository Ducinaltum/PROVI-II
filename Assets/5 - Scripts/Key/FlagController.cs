using System;
using UnityEngine;

public class FlagController : MonoBehaviour
{
    [SerializeField] private KeysSpritesLookup m_keySpritesLookup;
    [SerializeField] private SpriteRenderer m_flag;
    private EKeys m_targetKey;
    internal void Initialize(EKeys key)
    {
        m_targetKey = key;
        m_flag.color = m_keySpritesLookup.GetColor(m_targetKey);
    }
    
    public void SetUnlockedState(EKeys keys)
    {
        m_flag.gameObject.SetActive((m_targetKey & keys) > 0);
    }
}
