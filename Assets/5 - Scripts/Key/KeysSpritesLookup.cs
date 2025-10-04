using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "KeysSpritesLookup", menuName = "Scriptable Objects/KeysSpritesLookup")]
public class KeysSpritesLookup : ScriptableObject
{
    [SerializeField] private KeysGameHudPair[] m_keysSprites;
    private Dictionary<EKeys, KeysGameHudPair> m_lookup;

    public Sprite GetGameSprite(EKeys id)
    {
        m_lookup ??= GenerateLookup();
        if (m_lookup.TryGetValue(id, out KeysGameHudPair pair))
        {
            return pair.GameSprite;
        }
        return null;
    }

    public Sprite GetHUDSprite(EKeys id)
    {
        m_lookup ??= GenerateLookup();
        if (m_lookup.TryGetValue(id, out KeysGameHudPair pair))
        {
            return pair.HUDSprite;
        }
        return null;
    }

    private Dictionary<EKeys, KeysGameHudPair> GenerateLookup()
    {
        return m_keysSprites.ToDictionary(kghp => kghp.ID);
    }
}

[Serializable]
public class KeysGameHudPair
{
    public EKeys ID;
    public Sprite GameSprite;
    public Sprite HUDSprite;
    public Color KeyColor;
}