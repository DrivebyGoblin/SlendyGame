using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class LoseGame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _collectedDrugs;
    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private RectTransform _popup;
    public event Action onGameLost;

    private DrugsCounter _drugsCounter;
    private DisableService _disableService;
    private GameState _gameState;
    private float _delay;
    private int _collectedDrugsCount;
    private string _packageStatus => $"PACKAGES {_drugsCounter.CurrentCount}/{_drugsCounter.TotalCount}";




    public void Initialize(DisableService service, DrugsCounter counter, GameState state)
    {
        _gameState = state;
        _disableService = service;
        _drugsCounter = counter;
        _collectedDrugs.text = _packageStatus;
        _delay = (float)_videoPlayer.length + 1f;
        onGameLost += _disableService.Disable;
    }


    private void OnDisable()
    {
        onGameLost -= _disableService.Disable;
    }

    public void UpdateUI()
    {
        _collectedDrugs.text = _packageStatus;
    }

    private IEnumerator endGameCoroutine()
    {
        _collectedDrugsCount = _drugsCounter.CurrentCount;
        MusicService.Instance.StopMusic();
        _gameState.SetState(GameStates.Cutscene);
        onGameLost?.Invoke();
        _videoPlayer.Play();
        yield return new WaitForSeconds(_delay);
        _popup.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        StopCoroutine(endGameCoroutine());
    }



    public void Lose()
    {
        StartCoroutine(endGameCoroutine());
    }

}
