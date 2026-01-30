using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameplayCutscene : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private Image _logo;

    private const float _maxValue = 1f;
    private const float _minValue = 0f;
    private float _duration = 4f;
    private float _delay = 1f;
    private int _logoMaxValue = 1;

    private GameState _gameState;

    public void Initialize(GameState state)
    {
        _gameState = state;
    }

    public void Launch()
    {
        _gameState.SetState(GameStates.Cutscene);
        StartCoroutine(FillRoutine(_duration));
    }

    private IEnumerator FillRoutine(float fillDuration)
    {
        yield return FillToValue(_minValue, _maxValue, fillDuration);
        yield return new WaitForSeconds(_delay);
        _logo.fillOrigin = _logoMaxValue;
        yield return FillToValue(_maxValue, _minValue, fillDuration);
        yield return FadeInImage(_background, _duration);
        _gameState.SetState(GameStates.Gameplay);
    }

    private IEnumerator FillToValue(float from, float to, float duration)
    {
        float timer = 0f;
        _logo.fillAmount = from;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / duration);
            _logo.fillAmount = Mathf.Lerp(from, to, t);
            yield return null;
        }
        _logo.fillAmount = to;
    }



    private IEnumerator FadeInImage(Image image, float duration)
    {       
        float elapsedTime = 0f;

        Color color = image.color;
        color.a = _maxValue;
        image.color = color;

        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(_maxValue, _minValue, elapsedTime / duration);
            color.a = alpha;
            image.color = color;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        color.a = _minValue;
        image.color = color;
    }

}
