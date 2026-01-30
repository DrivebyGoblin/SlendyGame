using System.Collections;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private SceneLoaderView _sceneLoaderView;

    private FadeImage _fadeImage;
    private float _duration = 2.5f;
    private bool _isFadeTransition;

    public void Initialize(FadeImage fadeImage = null, bool IsFadeTransition = false)
    {
        _fadeImage = fadeImage;
        _isFadeTransition = IsFadeTransition;
        _sceneLoaderView.onButtonClicked += Transition;
    }

    public void Transition()
    {
        if (_isFadeTransition && _fadeImage != null)
        {
            StartCoroutine(TransitionCoroutine());
        }
        else
        {
            _sceneLoaderView.Launch(_sceneLoaderView.SceneName);
        }
    }

    private IEnumerator TransitionCoroutine()
    {
        if (_fadeImage != null)
        {
            yield return _fadeImage.Launch(_duration);
        }
        _sceneLoaderView.Launch(_sceneLoaderView.SceneName);
    }

    private void OnDisable()
    {
        _sceneLoaderView.onButtonClicked -= Transition;
    }


    
}
