using System;
using UnityEngine;


[CreateAssetMenu(fileName = "MusicData", menuName = "ScriptableObjects/AudioSystem/MusicData")]
public class MusicData : ScriptableObject
{
    public Music[] Music { get => _music; }
    [SerializeField] private Music[] _music;
}

[Serializable]
public class Music
{
    [SerializeField] private string _name;
    [SerializeField] private AudioClip _clip;

    public string Name { get => _name; }
    public AudioClip Clip { get => _clip; }
    
}

public enum MusicType
{
    MainMenu,
    GameplayAmbient,
    PackagesOne,
    PackagesTwo,
    PackagesThree,
    PackagesFour,
    SlenderApproximation
}
