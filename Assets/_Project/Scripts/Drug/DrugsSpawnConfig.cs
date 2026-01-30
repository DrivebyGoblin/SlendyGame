using UnityEngine;

[CreateAssetMenu(fileName = "DrugSpawnConfig", menuName = "ScriptableObjects/DrugsSpawn")]
public class DrugsSpawnConfig : ScriptableObject
{
    [field: SerializeField] public Drug Prefab { get; private set; }
    [field: SerializeField] public int Count { get; private set; }    
}
