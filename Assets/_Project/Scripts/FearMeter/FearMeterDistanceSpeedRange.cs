using UnityEngine;

[CreateAssetMenu(fileName = "Config", menuName = "ScriptableObjects/FearMeter/FearMeterSpeed")]
public class FearMeterDistanceSpeedRange : ScriptableObject
{
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;
    [SerializeField] private float speed;

    public float MinDistance { get => minDistance; }
    public float MaxDistance { get => maxDistance; }
    public float Speed { get =>  speed; }

}

