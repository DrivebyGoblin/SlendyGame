using System.Collections;
using TMPro;
using UnityEngine;

public class CollectedDrugsView : MonoBehaviour
{
    private readonly string _packagesLabel = "Packages ";

    [SerializeField] private TextMeshProUGUI _count;
    private DrugsCounter _drugsCounter;
    private float _delay = 5f; 


    public void Initialize(DrugsCounter drugsCounter) => _drugsCounter = drugsCounter;

    private void ShowCount(bool isActive) => _count.gameObject.SetActive(isActive);

    private void GetCount() => _count.text = $"{_packagesLabel}{_drugsCounter.CurrentCount}/{_drugsCounter.TotalCount}";
   


    public void ShowInfo()
    {
        ShowCount(true);
        StartCoroutine(ShowAndHideCoroutine());
    }

    private IEnumerator ShowAndHideCoroutine()
    {
        GetCount();
        yield return new WaitForSeconds(_delay);
        ShowCount(false);
    }

}
