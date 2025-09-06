using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuCanvas : MonoBehaviour
{
    [SerializeField] private Button m_playButton;
    [SerializeField] private Button m_exitButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_playButton.onClick.AddListener(PlayGame);
        m_exitButton.onClick.AddListener(ExitGame);
    }

    private void PlayGame()
    {
        SceneManager.LoadScene("TestLevel");
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}