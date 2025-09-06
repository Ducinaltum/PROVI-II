using UnityEngine;
using UnityEngine.UI;

public class KeyIndicatorItemUI : MonoBehaviour
{
    [SerializeField] private Image m_image;

    public void SetKeySprite(Sprite sprite) => m_image.sprite = sprite;
}
