using UnityEngine;

[CreateAssetMenu(fileName = "SoundDB", menuName = "Scriptable Objects/SoundDB")]
public class SoundDB : ScriptableObject
{
    [SerializeField] SFXAudioClip[] m_audioClips;

    public SFXAudioClip[] AudioClips => m_audioClips;
}

[System.Serializable]
public class SFXAudioClip
{
    public string ID;
    public AudioClip AudioClip;
}
