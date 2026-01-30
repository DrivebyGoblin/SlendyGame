using System.Collections.Generic;
using UnityEngine;


public class DrugSpawnPositions : MonoBehaviour
{
    [SerializeField] private List<Transform> _allLocations;
    public List<Transform> AllLocations { get => _allLocations; }


    public List<Transform> GetRandomLocation(int drugsCount)
    {
        List<Transform> locations = new List<Transform>();

        if (_allLocations.Count >= drugsCount)
        {
            for (int i = 0; i < drugsCount; i++)
            {
                var index = Random.Range(0, _allLocations.Count);
                var childIndex = Random.Range(0, _allLocations[index].childCount);
                var position = _allLocations[index].GetChild(childIndex);

                locations.Add(position);

                _allLocations.RemoveAt(index);
            }

            return locations;
        }
        else
        {
            return null;
        }
    }

}


