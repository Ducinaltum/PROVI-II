using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "KeysSpritesLookup", menuName = "Scriptable Objects/KeysSpritesLookup")]
public class KeysSpritesLookup : ScriptableObject
{
    [SerializeField] private KeysGameHudPair[] m_keysSprites;
    private Dictionary<EKeys, KeysGameHudPair> m_lookup;

    public Color GetColor(EKeys id)
    {
        m_lookup ??= GenerateLookup();
        if (m_lookup.TryGetValue(id, out KeysGameHudPair pair))
        {
            return pair.Color;
        }
        return default;
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
    public Color Color = Color.white;
}