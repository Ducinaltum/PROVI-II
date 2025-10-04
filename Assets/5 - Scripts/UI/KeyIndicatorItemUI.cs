using UnityEngine;
using UnityEngine.UI;

public class KeyIndicatorItemUI : MonoBehaviour
{
    [SerializeField] private Image m_image;

    public void SetKeyColor(Color color) => m_image.color = color;
}
