using System.Collections.Generic;
using UnityEngine;

public class KeychainIndicator : MonoBehaviour
{
    [SerializeField] private KeysSpritesLookup m_keysSpritesLookup;
    [SerializeField] private KeyIndicatorItemUI m_keyIndicatorItemUIPrefab;

    void Start()
    {
        KeyMaster.Instance.OnKeyPickedUp.AddListener(AddKey);
        List<EKeys> keys = new();
        foreach (EKeys flag in EKeys.GetValues(typeof(EKeys)))
        {
            if (KeyMaster.Instance.PickedKeys.HasFlag(flag) && flag != default)
            {
                AddKey(flag);
            }
        }
    }

    void AddKey(EKeys key)
    {
        KeyIndicatorItemUI item = Instantiate(m_keyIndicatorItemUIPrefab, transform);
        item.SetKeyColor(m_keysSpritesLookup.GetColor(key));
    }
}
