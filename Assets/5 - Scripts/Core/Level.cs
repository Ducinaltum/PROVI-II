using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] private string m_levelName;
    private Character m_character;
    private static DoorConfiguration m_lastTraveledDoor = default;
    public string LevelName => m_levelName;

    void Awake()
    {
        ServiceLocator.RegisterService(this);
    }

    void Start()
    {
        if (m_lastTraveledDoor != default)
        {
            string doorID = m_lastTraveledDoor.GetSourceSceneName();
            KeyMaster.Instance.TravelToDoor(doorID);
            m_lastTraveledDoor = default;
        }
    }

    void OnDestroy()
    {
        ServiceLocator.UnregisterService<Level>();
    }

    public void OnDoorTrespassed(Door door)
    {
        Debug.Log("Door trespassed");
        m_lastTraveledDoor = door.Configuration;
        SceneManagementService.Instance.GoToScene("Level_" + door.Configuration.GetTargetSceneName());
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
