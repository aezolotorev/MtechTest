using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;
using Zenject;

public class Enemy : MonoBehaviour
{    
    public EnemyData enemyData;
    public IHealth health;
    
    [SerializeField] SpriteRenderer spriteRenderer;  

    public CustomEventBus.EventBus eventBus;
   

    
    public void Setup(EnemyData enemydata, CustomEventBus.EventBus eventBus)
    { 
        enemyData=enemydata;   
        spriteRenderer.sprite = enemyData.sprite;  
        this.eventBus = eventBus;           
        health = new EnemyHealth(this);
    }
       

   
}
