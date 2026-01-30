using UnityEngine;
using UnityEngine.UI;
using System;

public class SceneLoaderView : MonoBehaviour
{
    [SerializeField] private Button _button;
    private SceneLoader _sceneLoader;
    private string _sceneName;
    public string SceneName { get => _sceneName; }

    public event Action onButtonClicked;



    public void Initialize(string sceneName)
    {
        _sceneLoader = FindObjectOfType<SceneLoader>();
        _sceneName = sceneName;
        _button.onClick.AddListener(OnButtonPress);
    }


    public void OnButtonPress()
    {
        SFXService.Instance.PlaySound(SingleSFX.UIClick);
        onButtonClicked?.Invoke();
    }

    public void Launch(string sceneName)
    {
        _sceneLoader.LoadScene(_sceneName); 
    }

    
    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonPress);
    }

}

