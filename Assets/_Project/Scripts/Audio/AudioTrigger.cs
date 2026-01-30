using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    [SerializeField] private SingleSFX _sfx;
    [SerializeField] private bool _destroyable;

    private void OnTriggerEnter(Collider other)
    {
        Play();       
    }


    private void Play()
    {
        SFXService.Instance.PlaySound(_sfx);
        if (_destroyable)
        {
            Destroy(this.gameObject);
        }
    }
}
