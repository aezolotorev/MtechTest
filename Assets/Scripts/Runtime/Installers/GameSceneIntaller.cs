using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using CustomEventBus;
using Zenject;
using Zenject.SpaceFighter;
using Unity.Mathematics;
using CustomEventBus.Signals;

public class GameSceneIntaller : MonoInstaller
{
    [SerializeField] private Joystick _joystick;    
    [SerializeField] private Transform pointForInputSpawn;
    [SerializeField] private GameConfig _gameConfig;

    [SerializeField] private Player playerPrefab;

    [SerializeField] private Transform pointOfPlayerSpawn;

    [SerializeField] private EnemySystem enemySystem;

    [SerializeField] private Spawner spawner;

    [SerializeField] PointOfEnemySpwnMarker pointOfEnemySpwnMarker;

    [SerializeField] PlayerBullet playerBulletPrefab;

    [SerializeField]  BulletConfig bulletConfig;

    [SerializeField] BulletPool bulletPoolPrefab;

    [SerializeField] PlayerWeapon playerWeaponPrefab;
    [SerializeField] WeaponConfig weaponConfig;

    [SerializeField] UISystem uiSystem;

    private Player _playerInstance;


    public override void InstallBindings()
    {
       BindInput();        
       BindPlayer();
       BindEnemy();
       BindBullets();
       BindWeapon();
       BindUi();
    }

    private void BindInput(){
       if(Application.isMobilePlatform)
        {
        var joyStick = Container.InstantiatePrefabForComponent<Joystick>(_joystick, pointForInputSpawn);
        Container.Bind<Joystick>().FromInstance(joyStick).AsSingle();   
        Container.Bind<IInput>().To<MobileInput>().FromNew().AsSingle();
        }
        else
        Container.Bind<IInput>().To<PCInput>().FromNew().AsSingle();          
        Container.Bind<PlayerInput>().FromNew().AsSingle();  
    }

    
    private void BindPlayer(){  
        Container.Bind<GameConfig>().FromInstance(_gameConfig).AsSingle();     
        Container.Bind<IHealth>().To<PlayerHealth>().AsSingle();
        Container.Bind<IMovable>().To<SimpleMovement>().AsTransient();
        _playerInstance = Container.InstantiatePrefabForComponent<Player>(playerPrefab, pointOfPlayerSpawn);        
    }
    private void BindEnemy(){
        var enemySpawnMarkerInstance = Container.InstantiatePrefabForComponent<PointOfEnemySpwnMarker>(pointOfEnemySpwnMarker);
        Container.Bind<PointOfEnemySpwnMarker>().FromInstance(enemySpawnMarkerInstance).AsSingle();
        var spawnerInstance = Container.InstantiatePrefabForComponent<Spawner>(spawner, pointOfPlayerSpawn);
        Container.Bind<Spawner>().FromInstance(spawnerInstance).AsSingle();
        var enemySystemInstance = Container.InstantiatePrefabForComponent<EnemySystem>(enemySystem, pointOfPlayerSpawn);      
        Container.Bind<EnemySystem>().FromInstance(enemySystemInstance).AsSingle(); 
    }
    private void BindBullets()
    {
        Container.Bind<BulletConfig>().FromInstance(bulletConfig).AsSingle();
        Container.Bind<IDamagable>().To<PlayerBullet>().AsTransient();
        //Container.Bind<IMovable>().To<SimpleMovement>().FromFactory<PlayerBulletFactory>().AsTransient();
        Container.Bind<PlayerBullet>().FromInstance(playerBulletPrefab).AsTransient();
        var bulletPoolInstance = Container.InstantiatePrefabForComponent<BulletPool>(bulletPoolPrefab, pointOfPlayerSpawn);
        Container.Bind<BulletPool>().FromInstance(bulletPoolInstance).AsSingle();
    }
    private void BindWeapon(){
        Container.Bind<WeaponConfig>().FromInstance(weaponConfig).AsSingle();
        var playerWeaponInstance = Container.InstantiatePrefabForComponent<PlayerWeapon>(playerWeaponPrefab, _playerInstance.transform.position, Quaternion.identity, _playerInstance.transform);
        Container.Bind<PlayerWeapon>().FromInstance(playerWeaponInstance).AsSingle();
    }
    private void BindUi(){
        var uiSystemInstance = Container.InstantiatePrefabForComponent<UISystem>(uiSystem);
        Container.Bind<UISystem>().FromInstance(uiSystemInstance).AsSingle();        
    }
}