using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "StateConfig", menuName = "ScriptableObjects/Enemy/EnemyState")]
public class EnemyStateConfig : ScriptableObject
{
    [SerializeField] private List<EnemyStateParameters> _states;
    public List<EnemyStateParameters> States { get => _states; }
}

[System.Serializable]
public class EnemyStateParameters
{
    [SerializeField] private float radius;
    [SerializeField] private float repeatRate;

    public float Radius { get => radius; }
    public float RepeatRate { get => repeatRate; }
}
