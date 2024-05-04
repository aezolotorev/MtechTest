using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using CustomEventBus.Signals;
using UniRx;
using System;
public class GameManager : MonoBehaviour, IDisposable
{
    SceneLoader sceneLoader;
    public LevelSystem levelSystem;

    public ReactiveProperty<int> score = new ReactiveProperty<int>(0);
    public int Level => levelSystem.GetLevel();
    public ReactiveProperty<int> life = new ReactiveProperty<int>();

    CustomEventBus.EventBus eventBus;

    [Inject]
    public void Construct(SceneLoader sceneLoader, LevelSystem levelSystem, CustomEventBus.EventBus eventBus){

        this.sceneLoader = sceneLoader;
        this.levelSystem = levelSystem;  
        this.eventBus = eventBus;   
        eventBus.Subscribe<OnLevelEndEvent>(LevelEnd);
        eventBus.Subscribe<OnEnemyDiedEvent>(EnemyDead); 
               
    }
    private void Start(){      
        if(sceneLoader.GetCurrentScene() == 0){
            sceneLoader.LoadScene(1);
        }
    }

    private void LevelEnd(OnLevelEndEvent ctx){
        levelSystem.FinishLevel();       
    }

    public void Restart(){
        sceneLoader.LoadScene(1);
        score.Value = 0;
    }

    public void NextLevel(){
        sceneLoader.LoadScene(1);        
    }

    private void EnemyDead(OnEnemyDiedEvent ctx){
        score.Value += ctx._enemy.enemyData.score;
    }

    public void Dispose()
    {
        eventBus.Unsubscribe<OnLevelEndEvent>(LevelEnd);
        eventBus.Unsubscribe<OnEnemyDiedEvent>(EnemyDead);
    }
}
