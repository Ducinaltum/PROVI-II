using UnityEngine;

[CreateAssetMenu(fileName = "DoorConection", menuName = "Scriptable Objects/DoorConection")]
public class DoorConfiguration : ScriptableObject
{
    [SerializeField] private string ConnectsTo;
    [SerializeField] private EKeys UnlocksWith;

    public string GetTargetSceneName()
    {
        return $"Level_{ConnectsTo}";
    }

    public bool GetIsUnlocked(EKeys keys)
    {
        return (UnlocksWith & keys) == UnlocksWith;
    }
}
