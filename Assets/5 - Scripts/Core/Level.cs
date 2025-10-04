using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] private string m_levelName;
    private Character m_character;

    public string LevelName => m_levelName;

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
        Debug.Log("Door trespassed");
        SceneManagementService.Instance.GoToScene(door.Configuration.GetTargetSceneName());
    }

    internal void RegisterCharacter(Character character)
    {
        m_character = character;
        m_character.DamageReceiver.OnDeath.AddListener(RestartLevel);
    }

    private void RestartLevel()
    {
        if (ServiceLocator.TryGetService(out SceneManagementService service))
        {
            service.GoToScene(SceneManager.GetActiveScene().name);
        }
    }


}
