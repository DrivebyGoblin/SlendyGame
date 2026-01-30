
public interface IInput
{
    public bool IsSprinting();
    public float GetHorizontalInput();
    public float GetVerticalInput();

    public float GetPointerDeltaX();
    public float GetPointerDeltaY();
    public bool Pause();
    public bool Click();
    public bool RightClick();
    
}
