using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagementService : MonoBehaviour
{
    private static SceneManagementService m_instance;
    public static SceneManagementService Instance
    {
        get
        {
            if (m_instance == default)
            {
                GameObject obj = new(nameof(SceneManagementService));
                m_instance = obj.AddComponent<SceneManagementService>();
            }
            return m_instance;
        }
    }

    void Awake()
    {
        if (m_instance != null && m_instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            m_instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void GoToScene(string sceneName)
    {
        //TODO: Load fader scene additively
        //TODO: Load target scene async additively
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        //TODO: Unload fader scene additively
    }
}
