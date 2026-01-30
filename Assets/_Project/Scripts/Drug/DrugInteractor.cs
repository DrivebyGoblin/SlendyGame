using UnityEngine;

public class DrugInteractor : MonoBehaviour
{
    private float _radius = 5f;
    private IInput _input;

    public void Initialize(IInput input)
    {
        _input = input;
    }



    public void Pickup()
    {
        var detected = Physics.OverlapSphere(transform.position, _radius);

        foreach (var item in detected)
        {
            var drug = item.GetComponent<IInteractable>();

            if (drug != null)
                drug.Interact();       
        }
    }



    private void Update()
    {
        if (_input != null && _input.Click())
        {
            Pickup();
        }
    }

}
