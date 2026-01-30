using System.Collections;
using UnityEngine;

public class MainMenuBackground : MonoBehaviour
{
    [SerializeField] private RectTransform[] _objects;
    [SerializeField] private float _fadeTime = 0.5f;
    [SerializeField] private float _moveDuration = 2f;
    [SerializeField] private float _moveDistance = 300f;
    [SerializeField] private float _holdTime = 0.5f;

    private CanvasGroup[] _canvasGroups;
    private int _currentIndex = 0;
    private int _totalObjects;
    private const float _maxAlpha = 1f;
    private const float _minAlpha = 0f;

    public void Initialize()
    {
        _totalObjects = _objects.Length;
        _canvasGroups = new CanvasGroup[_totalObjects];

        for (int i = 0; i < _totalObjects; i++)
        {
            CanvasGroup cg = _objects[i].GetComponent<CanvasGroup>();
            if (cg == null)
            {
                cg = _objects[i].gameObject.AddComponent<CanvasGroup>();
            }
            _canvasGroups[i] = cg;

            cg.alpha = 0f;
        }
    }

    public void Play()
    {
        StartCoroutine(AnimationLoop());
    }

    private IEnumerator AnimationLoop()
    {
        while (true)
        {
            yield return Fade(_objects[_currentIndex], _canvasGroups[_currentIndex], _minAlpha, _maxAlpha, _fadeTime);
            yield return Move(_objects[_currentIndex], _moveDistance, _moveDuration);
            yield return new WaitForSeconds(_holdTime);
            yield return Fade(_objects[_currentIndex], _canvasGroups[_currentIndex], _maxAlpha, _minAlpha, _fadeTime);
            _currentIndex = (_currentIndex + 1) % _totalObjects;
        }
    }

    private IEnumerator Fade(RectTransform obj, CanvasGroup cg, float startAlpha, float endAlpha, float duration)
    {
        cg.alpha = startAlpha;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            cg.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            yield return null;
        }
        cg.alpha = endAlpha;
    }

    private IEnumerator Move(RectTransform obj, float distance, float duration)
    {
        Vector3 startPos = obj.localPosition;
        Vector3 direction = Random.insideUnitSphere;
        direction.z = 0f;
        Vector3 endPos = startPos + direction.normalized * distance;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            obj.localPosition = Vector3.Lerp(startPos, endPos, elapsed / duration);
            yield return null;
        }
        obj.localPosition = endPos;
    }
    
}
