using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SFXGrouped", menuName = "ScriptableObjects/AudioSystem/SFXGroupedData")]
public class SFXGroupData : ScriptableObject
{
    [SerializeField] private SFXGrouped[] _SFXGrouped;
    public SFXGrouped[] SFXGrouped { get => _SFXGrouped; }
}


[Serializable]
public class SFXGrouped
{
    [SerializeField] private string _name;
    [SerializeField] private AudioClip[] _clips;

    public string Name { get => _name; }
    public AudioClip[] Clips { get => _clips; }
}

public enum GroupSFX
{
    WoodFootsteps,
    GrassFootsteps,
}
