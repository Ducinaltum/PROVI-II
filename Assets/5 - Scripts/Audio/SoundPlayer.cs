using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    private static SoundPlayer m_instance;
    public static SoundPlayer Instance
    {
        get
        {
            if (m_instance == default)
            {
                SoundPlayer obj = Resources.Load<SoundPlayer>("SoundPlayer");
                m_instance = Instantiate(obj); ;
            }
            return m_instance;
        }
    }
    [SerializeField] private AudioSource m_auso;
    [SerializeField] private SoundDB m_soundDB;
    private Dictionary<string, AudioClip> m_sfxLookup = new();

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

    void Start()
    {
        m_sfxLookup = m_soundDB.AudioClips.ToDictionary(ac => ac.ID, ac => ac.AudioClip);
    }

    public void PlaySound(string id, float volume = 1)
    {
        if(m_sfxLookup.TryGetValue(id, out AudioClip clip))
        {
            m_auso.volume = volume;
            m_auso.PlayOneShot(clip);
        }
    }
}
