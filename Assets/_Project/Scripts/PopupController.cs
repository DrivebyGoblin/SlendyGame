using UnityEngine;
using UnityEngine.UI;

public class PopupController : MonoBehaviour
{
    [SerializeField] private GameObject _popup;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _openButton;

    public void Initialize()
    {
        _closeButton.onClick.AddListener(Close);
        _openButton.onClick.AddListener(Open);
    }

    private void OnDisable()
    {
        _closeButton?.onClick.RemoveListener(Close);
        _closeButton?.onClick.RemoveListener(Open);
    }

    public void Close()
    {
        _popup.SetActive(false);
        SFXService.Instance.PlaySound(SingleSFX.UIClick);
    }

    public void Open()
    {
        _popup.SetActive(true);
        SFXService.Instance.PlaySound(SingleSFX.UIClick);
    }
}
