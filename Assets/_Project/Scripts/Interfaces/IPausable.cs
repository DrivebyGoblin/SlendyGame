
public interface IPausable
{
    bool IsPaused { get; }
    public void Pause();
    public void Resume();

}
