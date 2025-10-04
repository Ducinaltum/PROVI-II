using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class LevelNameTitle : MonoBehaviour
{
    [SerializeField] private TMP_Text m_levelTitleText;
    [SerializeField] private CanvasGroup m_canvasGroup;
    [SerializeField] private float m_animationDuration = 1.0f;
    [SerializeField] private float m_showDuration = 2.0f;
    private float m_speedCoeficent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_speedCoeficent = 1 / m_animationDuration;
        if (ServiceLocator.TryGetService(out Level level))
        {
            Debug.Assert(!string.IsNullOrEmpty(level.LevelName), $"{nameof(Level)}'s component name is not set");
            m_levelTitleText.SetText(level.LevelName);
            m_canvasGroup.alpha = 0;
            StartCoroutine(ShowLevelTitle());
        }
        else
        {
            Debug.LogError($"{nameof(Level)} component is not existent in this scene");
        }
    }

    private IEnumerator ShowLevelTitle()
    {
        yield return new WaitForSeconds(1.0f);
        yield return StartCoroutine(m_canvasGroup.Fade(1, m_animationDuration));
        yield return new WaitForSeconds(m_showDuration);
        yield return StartCoroutine(m_canvasGroup.Fade(0, m_animationDuration));
    }

}

