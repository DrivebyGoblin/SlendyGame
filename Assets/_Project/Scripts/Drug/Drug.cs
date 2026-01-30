using System;
using UnityEngine;

public class Drug : MonoBehaviour, IInteractable
{
    public event Action onDrugCollected;
    public event Action onDrugCountReached;
    

    private DrugsCounter _drugsCounter;
    private GameplayBackgroundMusic _backgroundMusic;
    private CollectedDrugsView _collectedDrugsView;
    private LoseGame _loseGame;

    public void Initialize(DrugsCounter drugsCounter, GameplayBackgroundMusic backgroundMusic, CollectedDrugsView collectedDrugsView, LoseGame loseGame)
    {
        _drugsCounter = drugsCounter;
        _backgroundMusic = backgroundMusic;
        _collectedDrugsView = collectedDrugsView;
        _loseGame = loseGame;
        onDrugCollected += _drugsCounter.Increase;
        onDrugCountReached += _backgroundMusic.SwitchClip;
        onDrugCollected += _collectedDrugsView.ShowInfo;
        onDrugCollected += _loseGame.UpdateUI;
    }



    private void OnDisable()
    {
        onDrugCollected -= _drugsCounter.Increase;
        onDrugCountReached -= _backgroundMusic.SwitchClip;
        onDrugCollected -= _collectedDrugsView.ShowInfo;
        onDrugCollected -= _loseGame.UpdateUI;
    }



    public void Interact()
    {
        onDrugCollected.Invoke();
        onDrugCountReached?.Invoke();
        SFXService.Instance.PlaySound(SingleSFX.Drug);
        Destroy(this.gameObject);
    }


}
