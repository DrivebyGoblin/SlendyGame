using System.Collections.Generic;

public class DisableService 
{
    private readonly List<IDisable> _handlers = new List<IDisable>();

    
    public void Register(IDisable handler)
    {
        _handlers.Add(handler);
    }

    public void UnRegister(IDisable handler)
    {
        _handlers.Remove(handler);
    }

    public void Disable()
    {
        
        foreach (var handler in _handlers)
        {
            handler.Disable();
        }
    }
}
