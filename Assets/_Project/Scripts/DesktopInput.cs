using UnityEngine;

public class DesktopInput : IInput
{
    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";
    private const string mouseX = "Mouse X";
    private const string mouseY = "Mouse Y";


    public bool IsSprinting()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }

    public float GetHorizontalInput()
    {
        return Input.GetAxis(horizontal);
    }

    public float GetVerticalInput()
    {
        return Input.GetAxis(vertical);   
    }

    public bool Pause()
    {
        return Input.GetKeyDown(KeyCode.Escape);
    }

    public float GetPointerDeltaX()
    {
        return Input.GetAxis(mouseX);
    }

    public float GetPointerDeltaY()
    {
        return Input.GetAxis(mouseY);
    }


    public bool Click()
    {
        return Input.GetMouseButtonDown(0);
    }
    public bool RightClick()
    {
        return Input.GetMouseButton(1);
    }
}
