using UnityEngine;

public class BootstrapMainMenu : MonoBehaviour
{
    private SFXService _sfxService => SFXService.Instance;
    private MusicService _musicService => MusicService.Instance;

    private SceneLoader _sceneLoader;
    [SerializeField] private PopupController _popupController;
    [SerializeField] private SceneLoaderView _sceneLoaderView;
    [SerializeField] private FadeImage _fadeImage;
    [SerializeField] private ExitGame _exitGame;
    [SerializeField] private SceneTransition _sceneTransition;
    [SerializeField] private MainMenuBackground _mainMenuBackground;
    [SerializeField] private PlayerCameraSettings _cameraControllerSettings;
    [SerializeField] private SettingsView _sensetivityView;
    [SerializeField] private SettingsView _sfxView;
    [SerializeField] private SettingsView _musicView;


    public void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        MusicService.Instance.PlayMusic(MusicType.MainMenu);
        SensitivityData.Initialize(_cameraControllerSettings);
        _sceneLoader = FindObjectOfType<SceneLoader>();
        _sceneLoaderView.Initialize(SceneID.Gameplay);
        _fadeImage.Initialize();
        _sceneTransition.Initialize(_fadeImage, true);
        _popupController.Initialize();
        _exitGame.Initialize();      
        _sensetivityView.Initialize(SensitivityData.MinValue,SensitivityData.MaxValue,SensitivityData.Sensetivity, (value) => { SensitivityData.Sensetivity = value; });
        _musicView.Initialize(_musicService.MinValue, _musicService.MaxValue, _musicService.AudioSource.volume, (value) => { _musicService.AudioSource.volume = value; });
        _sfxView.Initialize(_sfxService.MinValue, _sfxService.MaxValue, _sfxService.AudioSource.volume,(value) => { _sfxService.AudioSource.volume = value; });
        _mainMenuBackground.Initialize();
        _mainMenuBackground.Play();
    }
}
