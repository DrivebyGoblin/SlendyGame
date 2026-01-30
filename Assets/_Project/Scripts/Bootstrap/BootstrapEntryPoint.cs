using UnityEngine;


public class BootstrapEntryPoint : MonoBehaviour
{  
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private SFXService _sfxService;
    [SerializeField] private MusicService _musicService;

    private void Start()
    {
        _sceneLoader.Initialize();
        _sfxService.Initialize();
        _musicService.Initialize();
        _sceneLoader.LoadScene(SceneID.MainMenu);
    }

}
