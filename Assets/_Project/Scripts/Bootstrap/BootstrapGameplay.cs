using System.Collections.Generic;
using UnityEngine;

public class BootstrapGameplay : MonoBehaviour
{
    [SerializeField] private EnemyStateConfig _enemyStateConfig;
    [SerializeField] private DrugsSpawnConfig _drugsSpawnConfig;
    [SerializeField] private FearMeterSpeedConfig _fearMeterSpeedConfig;
    [SerializeField] private DrugSpawnPositions _drugSpawnPositions;
    [SerializeField] private TerrainObstacles _terrainObstacles;
    [SerializeField] private SceneTransition _sceneTransition;
    [SerializeField] private CollectedDrugsView _collectedDrugsView;
    [SerializeField] private SceneLoaderView _sceneLoaderView;
    [SerializeField] private GameplayBackgroundMusic _backgroundMusic;
    [SerializeField] private EnemyFollowAgent _enemyFollowAgent;
    [SerializeField] private DrugInteractor _drugInteractor;
    [SerializeField] private CharacterController _player;
    [SerializeField] private PlayerCameraController _playerCameraController;
    [SerializeField] private PlayerGravity _playerGravity;
    [SerializeField] private MovementHandler _movementHanlder;
    [SerializeField] private PlayerFootsteps _playerFootsteps;
    [SerializeField] private PlayerFearMeter _fearMeter;
    [SerializeField] private FearMeterView _fearMeterVisual;
    [SerializeField] private GameplayCutscene _gameplayCutscene;
    [SerializeField] private PauseController _pauseController;
    [SerializeField] private PauseView _pauseView; 
    [SerializeField] private LoseGame _loseGame;
    [SerializeField] private WinGame _winGame;
    [SerializeField] private Flashlight _flashLight;

    private DrugsCounter _drugsCounter;
    private DrugsSpawner _drugsSpawner;
    private DisableService _disableService;
    private PauseService _pauseService;
    private SceneLoader _sceneLoader;
    private GameState _gameState;
    private IInput _inputSystem;


    
    void Start()
    {
        MusicService.Instance.PlayMusic(MusicType.GameplayAmbient);
         _sceneLoader = FindObjectOfType<SceneLoader>();
        _sceneLoaderView.Initialize(SceneID.MainMenu);
        _inputSystem = new DesktopInput();
        _disableService = new DisableService();
        _gameState = new GameState();
        _drugsCounter = new DrugsCounter(_drugsSpawnConfig);
        _pauseService = new PauseService(_gameState);
        _pauseView.Initialize(_pauseService);
        _pauseController.Initialize(_pauseService, _inputSystem);
        _drugInteractor.Initialize(_inputSystem);
        _flashLight.Initialize(_inputSystem);
        _movementHanlder.Initialize(_inputSystem, _gameState);
        _playerCameraController.Initialize(_inputSystem, _player);
        _playerGravity.Initialize();
        _playerFootsteps.Initialize();
        _backgroundMusic.Initialize(_drugsCounter);
        _collectedDrugsView.Initialize(_drugsCounter);
        _loseGame.Initialize(_disableService, _drugsCounter, _gameState);
        _winGame.Initialize(_disableService, _gameState, _drugsCounter);
        _fearMeter.Initialize(_enemyFollowAgent, _fearMeterSpeedConfig, _loseGame);
        _fearMeterVisual.Initialize();
        _fearMeterVisual.StartFearMeterView();
        _terrainObstacles.GetObstacles();
        _drugsSpawner = new DrugsSpawner(_drugsSpawnConfig, _drugSpawnPositions);
        var allDrugs = _drugsSpawner.Spawn();
        DrugsInitialize(allDrugs);
        _enemyFollowAgent.Initialize(_player, _drugsCounter, _fearMeter, _enemyStateConfig);
        _enemyFollowAgent.FindPlayer();
        RegisterDisableService();
        RegisterPauseService();
        _sceneTransition.Initialize();
        _gameplayCutscene.Initialize(_gameState);
        _gameplayCutscene.Launch();
    }


    private void RegisterDisableService()
    {
        _disableService.Register(_enemyFollowAgent);
        _disableService.Register(_fearMeter);
        _disableService.Register(_fearMeterVisual);
        _disableService.Register(_movementHanlder);
        _disableService.Register(_playerFootsteps);
        _disableService.Register(_backgroundMusic);
    }

    private void RegisterPauseService()
    {
        _pauseService.Register(_fearMeter);
        _pauseService.Register(_fearMeterVisual);
        _pauseService.Register(_enemyFollowAgent);
        _pauseService.Register(_movementHanlder);
        _pauseService.Register(_playerCameraController);
    }


    private void DrugsInitialize(List<Drug> drugs)
    {
        foreach (var item in drugs)
        {
            item.Initialize(_drugsCounter, _backgroundMusic, _collectedDrugsView, _loseGame);
        }
    }

    
}
