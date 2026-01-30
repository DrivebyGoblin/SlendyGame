using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SFX", menuName = "ScriptableObjects/AudioSystem/SFXSingleData")]
public class SFXSingleData : ScriptableObject
{
    [SerializeField] private SFXSingle[] _sfxSingle;
    public SFXSingle[] SFXSingle { get => _sfxSingle ; }

}



[Serializable]
public class SFXSingle
{
    [SerializeField] private string _name;
    [SerializeField] private AudioClip _clip;


    public string Name { get => _name; }
    public AudioClip Clip { get => _clip; }
}

public enum SingleSFX
{
    UIClick,
    Drug,
    MetallHit,
    MiddleCreepy,
    SlenderScreamer
}