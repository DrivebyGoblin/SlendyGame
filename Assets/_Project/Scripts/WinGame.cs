using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WinGame : MonoBehaviour
{
    private const float _minAlpha = 0f;
    private const float _maxAlpha = 1f;
    private float _delay = 8f;
    private float _fadeDuration = 7f;

    [SerializeField] private Image _imagePopup;
    [SerializeField] private RectTransform _buttons;
    private DisableService _disableService;
    private GameState _gameState;
    private DrugsCounter _drugsCounter;

    public event Action onPackagesCollected;


    public void Initialize(DisableService disableService, GameState gameState, DrugsCounter drugsCounter)
    {
        _gameState = gameState;
        _disableService = disableService;
        _drugsCounter = drugsCounter;
        _drugsCounter.onAllDrugsCollected += Win;
        onPackagesCollected += _disableService.Disable;
    }


    private void OnDisable()
    {
        _drugsCounter.onAllDrugsCollected -= Win;
        onPackagesCollected -= _disableService.Disable;
    }


    private IEnumerator endGameCoroutine()
    {
        _gameState.SetState(GameStates.Cutscene);
        onPackagesCollected?.Invoke();
        FadeImage();
        yield return new WaitForSeconds(_delay);
        _buttons.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        
    }


    public void Win()
    {
        StartCoroutine(endGameCoroutine());
    }


    private void FadeImage()
    {
        StartCoroutine(FadeInImage(_imagePopup, _fadeDuration));
    }


    private IEnumerator FadeInImage(Image image, float duration)
    {
        _imagePopup.gameObject.SetActive(true);
        float elapsedTime = 0f;

        Color color = image.color;
        color.a = _minAlpha;
        image.color = color;

        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(_minAlpha, _maxAlpha, elapsedTime / duration);
            color.a = alpha;
            image.color = color;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        color.a = _maxAlpha;
        image.color = color;
    }

}
