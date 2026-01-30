using System;
using UnityEngine;

public class SFXService : MonoBehaviour
{
    [SerializeField] private AudioSource _source;
    [SerializeField] private SFXSingleData _dataSingle;
    [SerializeField] private SFXGroupData _dataGrouped;
    public static SFXService Instance;

    public AudioSource AudioSource { get => _source; }

    public float MinValue { get; private set; } = 0f;
    public float MaxValue { get; private set; } = 1f;


    public void Initialize()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        DontDestroyOnLoad(gameObject);

    }


    public void PlayRandomClipFromGroup(GroupSFX groupType)
    {
        AudioClip clip = GetRandomClipFromGroup(groupType);
        if (clip != null)
        {
            _source.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"Не удалось воспроизвести звук: клип для группы {groupType} не найден.");
        }
    }


    public AudioClip[] GetClipsFromGroup(GroupSFX groupType)
    {
        SFXGrouped group = Array.Find(_dataGrouped.SFXGrouped, g => g.Name == groupType.ToString());
        if (group != null && group.Clips.Length > 0)
        {
            return group.Clips;
        }

        Debug.LogWarning("Группа SFX не найдена или пуста: " + groupType);
        return null;
    }


    public AudioClip GetRandomClipFromGroup(GroupSFX groupType)
    {
        SFXGrouped group = Array.Find(_dataGrouped.SFXGrouped, g => g.Name == groupType.ToString());
        if (group != null && group.Clips.Length > 0)
        {
            int index = UnityEngine.Random.Range(0, group.Clips.Length);
            return group.Clips[index];
        }

        Debug.LogWarning("Группа SFX не найдена или пуста: " + groupType);
        return null;
    }


    public void PlayClip(AudioClip clip)
    {
        if (clip != null)
            _source.PlayOneShot(clip);
    }

    public void PlaySound(SingleSFX sfxType)
    {
        SFXSingle sfx = FindSFXByType(sfxType);

        if (sfx != null)
        {
            _source.clip = sfx.Clip;
            _source.PlayOneShot(_source.clip);
        }
        else
        {
            Debug.LogWarning("Звук не найден: " + sfxType);
        }
    }

    private SFXSingle FindSFXByType(SingleSFX sfxType)
    {
        foreach (SFXSingle sfx in _dataSingle.SFXSingle)
        {
            if (sfx.Name == sfxType.ToString())
            {
                return sfx;
            }
        }

        return null;
    }




}
