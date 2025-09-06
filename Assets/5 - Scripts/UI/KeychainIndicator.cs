using UnityEngine;

public class KeychainIndicator : MonoBehaviour
{
    [SerializeField] private KeysSpritesLookup m_keysSpritesLookup;
    [SerializeField] private KeyIndicatorItemUI m_keyIndicatorItemUIPrefab;
    
    void Start()
    {
        if (ServiceLocator.TryGetService(out KeyMaster keymaster))
        {
            keymaster.OnKeyPickedUp.AddListener(AddKey);
        }
    }

    void AddKey(Key key)
    {
        KeyIndicatorItemUI item = Instantiate(m_keyIndicatorItemUIPrefab, transform);
        item.SetKeySprite(m_keysSpritesLookup.GetHUDSprite(key.ID));
    }
}
