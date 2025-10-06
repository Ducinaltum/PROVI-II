using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DoorConection", menuName = "Scriptable Objects/DoorConection")]
public class DoorConfiguration : ScriptableObject
{
    [SerializeField] private string ConnectsFrom;
    [SerializeField] private string ConnectsTo;
    [SerializeField] private EKeys UnlocksWith;

    public string GetSourceSceneName()
    {
        return ConnectsFrom;
    }
    public string GetTargetSceneName()
    {
        return ConnectsTo;
    }

    public bool GetIsUnlocked(EKeys keys)
    {
        return (UnlocksWith & keys) == UnlocksWith;
    }

    public  List<EKeys> GetNeededKeys()
    {
        List<EKeys> keys = new();
        foreach (EKeys flag in EKeys.GetValues(typeof(EKeys)))
        {
            if (UnlocksWith.HasFlag(flag) && flag != default)
            {
                keys.Add(flag);
            }
        }
        return keys;
    }
}
