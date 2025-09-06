using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    Character m_character;
    void Awake()
    {
        ServiceLocator.RegisterService(this);
    }

    void OnDestroy()
    {
        ServiceLocator.UnregisterService<Level>();
    }

    public void OnDoorTrespassed(Door door)
    {
        Debug.Log("PLAYER WON!!!");
        SceneManager.LoadScene("MainMenu");
    }

    internal void RegisterCharacter(Character character)
    {
        m_character = character;
        m_character.DamageReceiver.OnDeath.AddListener(RestartLEvel);
    }

    private void RestartLEvel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
