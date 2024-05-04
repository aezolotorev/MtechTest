using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System;
using CustomEventBus;
using CustomEventBus.Signals;
using UniRx;
using Random = UnityEngine.Random;
using Zenject.SpaceFighter;


public class EnemySystem : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;  
    [SerializeField] private EnemyBullet enemyBulletPrefab;  

    [SerializeField] private BulletData bulletData;

    [SerializeField] private Benefit benefitPrefab;
    [SerializeField] private BulletData benefitBullet;
    private int count=30;

    public  List<Enemy> enemies = new List<Enemy>();
    private List<Bounds> bounds = new List<Bounds>();
    
    [SerializeField] private Spawner spawner;
    private GameConfig gameConfig;

    private PointOfEnemySpwnMarker pointOfEnemySpwnMarker;
    public EventBus eventBus;

    private LevelSystem levelSystem;

    [Inject]
    public void Construct(Spawner spawner, GameConfig gameConfig, 
    PointOfEnemySpwnMarker pointOfEnemySpwnMarker, EventBus eventBus, LevelSystem levelSystem)
    {
        this.spawner = spawner;
        this.gameConfig = gameConfig;
        this.pointOfEnemySpwnMarker = pointOfEnemySpwnMarker;
        this.eventBus = eventBus;
        eventBus.Subscribe<OnEnemyDiedEvent>(EnemyDead);
        this.levelSystem = levelSystem;
    }

    private void EnemyDead(OnEnemyDiedEvent ctx)
    {         
        if (enemies.Contains(ctx._enemy))
        {
            enemies.Remove(ctx._enemy);
            Destroy(ctx._enemy.gameObject);
        }
        if(!CheckEndLevel())
        CheckForBenefit(ctx);
    }

    private bool CheckEndLevel(){
        if(enemies.Count == 0){
            eventBus.Publish(new OnLevelEndEvent());
           return true;
        }
        return false;
    }

    private void CheckForBenefit(OnEnemyDiedEvent ctx){
        if(enemies.Count > 0){
            int x = Random.Range(0, 100);
            if(x < 10){
               Benefit benefit = (Benefit) spawner.Spawn(benefitPrefab, ctx._enemy.transform.position, Quaternion.identity, null);
               benefit.Setup(benefitBullet);
               benefit.gameObject.SetActive(true);
            }
        }
    }

    void Start()
    {
        InitEnemyes();
        Observable.Interval(TimeSpan.FromSeconds(GetRandomSpawnTime()))
       .Where(_ => IsHasAliveEnemys())
       .Subscribe(_ => SpawnBullet())
       .AddTo(this);
        CreateBoundList();

        Vector3[] targetPositions = new Vector3[enemies.Count];     
    } 
    private float GetRandomSpawnTime()
    {       
        return UnityEngine.Random.Range(1, 5);
    }


    private void CheckCountOfEnemys(){
        if(enemies.Count == 0){
            eventBus.Publish(new OnAllEnemyDiedEvent());            
        }       
    }
    private bool IsHasAliveEnemys(){
        return enemies.Count > 0;
    }

    private void SpawnBullet(){
        int index = UnityEngine.Random.Range(0, enemies.Count);
        Bullet bullet = (Bullet)spawner.Spawn(enemyBulletPrefab, enemies[index].transform.position, Quaternion.identity, null); 
        bullet.Setup(bulletData);   
        bullet.gameObject.SetActive(true);    
    }

    public void CreateBoundList(){
        for (int i = 0; i < enemies.Count; i++){
            bounds.Add(enemies[i].GetComponent<SpriteRenderer>().sprite.bounds);
        }
    }

    public List<Bounds> GetEnemys(){
        return bounds;
    }

    private void InitEnemyes()
    {
        for (int i = 0; i < count; i++)
        {
            var spawnedObject = spawner.Spawn(enemyPrefab, GetTransformPosition(i), Quaternion.identity, pointOfEnemySpwnMarker.transform);
            Enemy enemy = spawnedObject as Enemy;
            enemy.Setup(gameConfig.enemyConfig._enemies[levelSystem.GetLevel()%gameConfig.enemyConfig._enemies.Count], eventBus);
            enemies.Add(enemy);
        }
    }

    private Vector3 GetTransformPosition(int i)
    {
        int columns = 10;
        int rows = 3;
        float columnSpacing = 2.0f;
        float rowsSpacing = 5.0f;
        Vector3 center = pointOfEnemySpwnMarker.transform.position;
        Vector3 gridCenter = new Vector3((columns - 1) * columnSpacing/ 2, (rows - 1) * rowsSpacing/ 2, 0);
        int row = i % rows;
        int column = i % columns;
        Vector3 offset = new Vector3(column * columnSpacing - gridCenter.x, row * rowsSpacing - gridCenter.y, 0);
        Vector3 finalPosition = center + offset;
        return finalPosition;
    }

    public  void OnDestroy()
    {      
        eventBus.Unsubscribe<OnEnemyDiedEvent>(EnemyDead);
    }
}
