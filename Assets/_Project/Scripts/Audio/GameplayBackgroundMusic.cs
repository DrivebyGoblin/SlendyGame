using System.Collections;
using UnityEngine;


public class GameplayBackgroundMusic : MonoBehaviour, IDisable
{
    private const float _maxValue = 1f;
    private const float _minValue = 0f;
    private AudioSource _audioSource;
    private DrugsCounter _drugsCounter; 
    private float _swithDuration = 0.3f;
    private float _fadeDurtaion = 5f;
    private bool _isSwitching = false;
    private MusicService MusicService { get => MusicService.Instance; }




    public void Initialize(DrugsCounter drugsCounter)
    {
        _drugsCounter = drugsCounter;
        _audioSource = GetComponent<AudioSource>();
    } 


    private void NextAudioClip(Music music)
    {
        _audioSource.clip = music.Clip;
        _audioSource.Play();
    }

    private IEnumerator FadeVolume(float startVolume, float endVolume, float duration)
    {
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            _audioSource.volume = Mathf.Lerp(startVolume, endVolume, timer / duration);
            yield return null;
        }

        _audioSource.volume = endVolume;
    }




    private IEnumerator FadeAndSwitch(Music music)
    {
        _isSwitching = true;
        float duration = _swithDuration;

        yield return StartCoroutine(FadeVolume(_audioSource.volume, _minValue, duration));
        NextAudioClip(music);
        yield return StartCoroutine(FadeVolume(_minValue, _maxValue, duration));

        _isSwitching = false;
    }




    public void SwitchClip()
    {
        switch (_drugsCounter.CurrentCount)
        {
            case 1:
                StartCoroutine(FadeAndSwitch(MusicService.GetMusicByType(MusicType.PackagesOne)));
                break;
            case 3:
                StartCoroutine(FadeAndSwitch(MusicService.GetMusicByType(MusicType.PackagesTwo)));
                break;
            case 5:
                StartCoroutine(FadeAndSwitch(MusicService.GetMusicByType(MusicType.PackagesThree)));
                break;
            case 7:
                StartCoroutine(FadeAndSwitch(MusicService.GetMusicByType(MusicType.PackagesFour)));
                break;
            case 8:
                StartCoroutine(FadeVolume(_audioSource.volume, _minValue, _fadeDurtaion));
                break;
            default:
                break;
        }
    }

    public void StopMusic()
    {
        _audioSource.Stop();
    }

    public void StopMusic(float delay)
    {
        StartCoroutine(FadeVolume(_audioSource.volume, _minValue, delay));
    }


    public void Disable()
    {
        StartCoroutine(FadeVolume(_audioSource.volume, _minValue, _fadeDurtaion));
    }
}















