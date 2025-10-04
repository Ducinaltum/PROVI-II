using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagementService : MonoBehaviour
{
    void Awake()
    {
        ServiceLocator.RegisterService(this);
    }

    void OnDestroy()
    {
        ServiceLocator.UnregisterService<SceneManagementService>();
    }

    public void GoToScene(string sceneName)
    {
        //TODO: Load fader scene additively
        //TODO: Load target scene async additively
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        //TODO: Unload fader scene additively
    }
}
