using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeImage : MonoBehaviour
{
    [SerializeField] private RectTransform _loadingText;
    private const float _minAlpha = 0f;
    private const float _maxAlpha = 1f;
    private Image _image;


    public void Initialize()
    {
        _loadingText.gameObject.SetActive(false);
        _image = GetComponent<Image>();
        _image.raycastTarget = false;
    }


    public Coroutine Launch(float duration)
    {
        return StartCoroutine(FadeInImage(duration));
    }


    private IEnumerator FadeInImage(float duration)
    {
        _image.raycastTarget = true;
        _image.gameObject.SetActive(true);
        float elapsedTime = 0f;

        Color color = _image.color;
        color.a = _minAlpha;
        _image.color = color;

        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(_minAlpha, _maxAlpha, elapsedTime / duration);
            color.a = alpha;
            _image.color = color;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        color.a = _maxAlpha;
        _image.color = color;

        _loadingText.gameObject.SetActive(true);
       

    }

    
}



   
