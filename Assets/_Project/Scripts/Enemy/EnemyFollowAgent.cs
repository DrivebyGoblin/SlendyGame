using System.Collections;
using UnityEngine;
using UnityEngine.AI;



public class EnemyFollowAgent : MonoBehaviour, IDisable, IPausable
{
    private readonly float _maxDistance = 2f;
    private readonly int _maxAttemps = 250;
    
    private EnemyStateConfig _enemyStateConfig;
    private CharacterController _player;
    private NavMeshAgent _navMeshAgent;
    private DrugsCounter _drugsCounter;
    private PlayerFearMeter _fearMeter;

    private float _radiusAroundPlayer;
    private float _repeatRate;
    public bool IsPaused { get; private set; }

    public void Initialize(CharacterController player, DrugsCounter drugsCounter, PlayerFearMeter fearMeter, EnemyStateConfig config)
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _enemyStateConfig = config;
        _drugsCounter = drugsCounter;
        _player = player;
        _fearMeter = fearMeter;
    }

    private void Update()
    {
        LookAtPlayer();
    }

    private void ChangeState()
    {
        int index = _drugsCounter.CurrentCount;

        if (index < 0 || index >= _enemyStateConfig.States.Count)
            
            return;

        var currentState = _enemyStateConfig.States[index];
        _radiusAroundPlayer = currentState.Radius;
        _repeatRate = currentState.RepeatRate;
    }


    
    private void TeleportToPlayer(float _radius)
    {
        Vector3 center = _player.transform.position;
        float radius = _radius;

        for (int i = 0; i < _maxAttemps; i++)
        {
            Vector3 randomDirection = Random.onUnitSphere;
            Vector3 randomPoint = center + randomDirection * radius;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, _maxDistance, NavMesh.AllAreas))
            {
                if (_navMeshAgent.Warp(hit.position))
                {
                    return;
                }
            }
        }

    }

   
    private IEnumerator findPlayer()
    {
        while (_drugsCounter.CurrentCount < _drugsCounter.TotalCount)
        {
            while (IsPaused)
            {
                yield return null;
            }

            ChangeState();

            if (!_fearMeter.IsObserved)
            {
                float waitTime = _repeatRate;
                TeleportToPlayer(_radiusAroundPlayer);
                yield return new WaitForSeconds(waitTime);
            }
            else
            {
                yield return null;
            }
        }
        yield break;
    }

    public void FindPlayer()
    {
        StartCoroutine(findPlayer());
    }

    public void LookAtPlayer()
    {
        if (_player != null)
        {
            Vector3 direction = _player.transform.position - transform.position;
            direction.y = 0;
            if (direction.sqrMagnitude != 0)
            {
                float angle = Vector3.SignedAngle(-transform.right, direction, Vector3.up);
                transform.Rotate(0, angle, 0);
            }
        }
    }

    public void Disable()
    {
        transform.position = new Vector3(-100f, -500f, -100f);
    }

   
    public void Resume()
    {
        IsPaused = false;
    }

    public void Pause()
    {
        IsPaused = true;
    }
}
