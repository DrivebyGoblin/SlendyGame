using UnityEngine;
using UnityEngine.AI;

public class TerrainObstacles : MonoBehaviour
{
    
    private TreeInstance[] _obstacles;
    private Terrain _terrain;
    private float _width;
    private float _lenght;
    private float _hight;
    private bool _isError;


    public void GetObstacles()
    {
        _terrain = Terrain.activeTerrain;
        _obstacles = _terrain.terrainData.treeInstances;

        _lenght = _terrain.terrainData.size.z;
        _width = _terrain.terrainData.size.x;
        _hight = _terrain.terrainData.size.y;

        int i = 0;
        GameObject parent = new GameObject("Tree_Obstacles");

        foreach (TreeInstance tree in _obstacles)
        {
            Vector3 tempPos = new Vector3(tree.position.x * _width, tree.position.y * _hight, tree.position.z * _lenght);
            Quaternion tempRot = Quaternion.AngleAxis(tree.rotation * Mathf.Rad2Deg, Vector3.up);

            GameObject obs = new GameObject("Obstacle" + i);
            obs.transform.SetParent(parent.transform);
            obs.transform.position = tempPos;
            obs.transform.rotation = tempRot;

            obs.AddComponent<NavMeshObstacle>();
            NavMeshObstacle obsElement = obs.GetComponent<NavMeshObstacle>();
            obsElement.carving = true;
            obsElement.carveOnlyStationary = true;

            if (_terrain.terrainData.treePrototypes[tree.prototypeIndex].prefab.GetComponent<Collider>() == null)
            {
                _isError = true;
                break;
            }
            Collider coll = _terrain.terrainData.treePrototypes[tree.prototypeIndex].prefab.GetComponent<Collider>();
            if (coll.GetType() == typeof(CapsuleCollider) || coll.GetType() == typeof(BoxCollider))
            {

                if (coll.GetType() == typeof(CapsuleCollider))
                {
                    CapsuleCollider capsuleColl = _terrain.terrainData.treePrototypes[tree.prototypeIndex].prefab.GetComponent<CapsuleCollider>();
                    obsElement.shape = NavMeshObstacleShape.Capsule;
                    obsElement.center = capsuleColl.center;
                    obsElement.radius = capsuleColl.radius * 25;
                    obsElement.height = capsuleColl.height;

                }
                else if (coll.GetType() == typeof(BoxCollider))
                {
                    BoxCollider boxColl = _terrain.terrainData.treePrototypes[tree.prototypeIndex].prefab.GetComponent<BoxCollider>();
                    obsElement.shape = NavMeshObstacleShape.Box;
                    obsElement.center = boxColl.center;
                    obsElement.size = boxColl.size;
                }

            }
            else
            {
                _isError = true;
                break;
            }


            i++;
        }
        parent.transform.position = _terrain.GetPosition();
        if (!_isError) Debug.Log("All " + _obstacles.Length + " NavMeshObstacles were succesfully added");
    }
}

    