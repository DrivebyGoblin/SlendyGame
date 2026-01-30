using System.Collections.Generic;
using UnityEngine;

public class DrugsSpawner
{
    private DrugSpawnPositions _drugSpawnPositions;
    private DrugsSpawnConfig _config;
    private int _drugsAmount;

    public DrugsSpawner(DrugsSpawnConfig config , DrugSpawnPositions drugSpawnPositions)
    {
        _config = config;
        _drugsAmount = _config.Count;
        _drugSpawnPositions = drugSpawnPositions;
    }

    public List<Drug> Spawn()
    {
        List<Drug> drugs = new List<Drug>();
    

        if (_drugSpawnPositions.AllLocations.Count >= _drugsAmount)
        {
            var locations = _drugSpawnPositions.GetRandomLocation(_drugsAmount);          

            for (int i = 0; i < _drugsAmount; i++)
            {
                var drug = GameObject.Instantiate(_config.Prefab, locations[i].position, _config.Prefab.transform.rotation);
                drugs.Add(drug);
            }

            return drugs;
        }

        else
        {
            return null;
        }
    }



}
