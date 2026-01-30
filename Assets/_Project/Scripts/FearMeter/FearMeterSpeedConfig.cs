using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Config", menuName = "ScriptableObjects/FearMeter/FearMeterSpeedConfig")]
public class FearMeterSpeedConfig : ScriptableObject
{
    [field: SerializeField] public float DownSpeed { get; private set; }

    [SerializeField] private List<FearMeterDistanceSpeedRange> _config;
    public List<FearMeterDistanceSpeedRange> Config => _config;
}
