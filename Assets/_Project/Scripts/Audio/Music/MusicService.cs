using UnityEngine;

public class MusicService : MonoBehaviour
{
    [SerializeField] private MusicData _musicData;
    [SerializeField] private AudioSource _audioSource;

    public static MusicService Instance;

    public AudioSource AudioSource { get => _audioSource; }

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


    public void PlayMusic(MusicType musicType)
    {
        Music music = GetMusicByType(musicType);
        if (music != null)
        {
            _audioSource.clip = music.Clip;
            _audioSource.Play();
        }
    }

   
    public void StopMusic()
    {
        _audioSource.Stop();
    }


    public Music GetMusicByType(MusicType musicType)
    {
        foreach (var music in _musicData.Music)
        {
            if (musicType.ToString() == music.Name)
            {
                return music;
            }
        }

        return null;
    }


}
