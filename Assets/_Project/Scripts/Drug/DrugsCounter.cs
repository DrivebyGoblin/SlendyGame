using System;
public class DrugsCounter
{
    private const int _startCount = 0;
    private DrugsSpawnConfig _config;

    public int TotalCount { get; private set; }
    public int CurrentCount { get; private set; }
    
    public event Action onAllDrugsCollected;

    

    public DrugsCounter(DrugsSpawnConfig config)
    {
        _config = config;
        TotalCount = _config.Count;
        CurrentCount = _startCount;
    }

    public void Increase()
    {
       CurrentCount += 1;
        if (CurrentCount >= TotalCount)
        {
            onAllDrugsCollected?.Invoke();
        }
    } 

}
