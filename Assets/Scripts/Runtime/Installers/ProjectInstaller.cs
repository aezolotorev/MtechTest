using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] GameManager gameManagerPrefab;


    public override void InstallBindings(){      
       
        CustomEventBus.EventBus eventBus = new CustomEventBus.EventBus();
        Container.Bind<CustomEventBus.EventBus>().FromInstance(eventBus).AsSingle();
    
        LevelSystem levelSystem= new LevelSystem();       
        Container.Bind<LevelSystem>().FromInstance(levelSystem).AsSingle();

        SceneLoader sceneLoader = new SceneLoader();        
        Container.Bind<SceneLoader>().FromInstance(sceneLoader).AsSingle();

        GameManager gameManager = Container.InstantiatePrefabForComponent<GameManager>(gameManagerPrefab);
        Container.Bind<GameManager>().FromInstance(gameManager).AsSingle();

      
    }

  

    
}
