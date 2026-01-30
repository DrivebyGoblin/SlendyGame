using UnityEngine;
using UnityEngine.UI;

public class ExitGame : MonoBehaviour
{
    [SerializeField] private Button _button;

    public void Initialize()
    {
        _button.onClick.AddListener(Exit);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Exit);
    }

    public void Exit() => Application.Quit();

}
