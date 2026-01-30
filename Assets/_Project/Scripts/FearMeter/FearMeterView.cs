using UnityEngine;
using UnityEngine.Video;

public class FearMeterView : MonoBehaviour, IDisable, IPausable
{
    private const float NormalizationFactor = 100f;
    [SerializeField] private AudioSource _audioSource;
    private VideoPlayer _videoPlayer;
    private PlayerFearMeter _fearMeter;
    private MusicService MusicService { get => MusicService.Instance; }

    public bool IsPaused { get; private set; }


    public void Initialize()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
        _fearMeter = GetComponent<PlayerFearMeter>();
        _audioSource.clip = MusicService.GetMusicByType(MusicType.SlenderApproximation).Clip;
    }
    
    public void VisualEffect()
    {
        _videoPlayer.targetCameraAlpha = _fearMeter.Value / NormalizationFactor;
        _audioSource.volume = _fearMeter.Value / NormalizationFactor;
    }

    public void StartFearMeterView()
    {
        _audioSource.Play();
    }

    private void Update()
    {
        VisualEffect();
    }

    public void Disable()
    {
        _videoPlayer.Stop();
        _audioSource.Stop();
    }

    public void Resume()
    {
        _videoPlayer.enabled = true;
    }

    public void Pause()
    {
        _videoPlayer.enabled = false;
    }
}
