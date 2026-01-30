using UnityEngine;

public class PauseView : MonoBehaviour
{
    [SerializeField] private RectTransform _popup;
    private PauseService _pauseService;


    public void Initialize(PauseService model)
    {
        _pauseService = model;
        _pauseService.OnPause += Pause;
        _pauseService.OnResume += Resume;
    }

    private void OnDisable()
    {
        _pauseService.OnPause -= Pause;
        _pauseService.OnResume -= Resume;
    }


    public void Pause()
    {
        _popup.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void Resume()
    {
        _popup.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

}


