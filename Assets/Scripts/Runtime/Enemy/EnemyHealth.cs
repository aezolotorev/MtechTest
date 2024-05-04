using System;
using System.Collections;
using System.Collections.Generic;
using CustomEventBus.Signals;
using UnityEngine;
using Zenject;
[Serializable]
public class EnemyHealth : IHealth
{       
    private Enemy enemy;
    private int _health;
    public int Health{ get{ return _health; } set{}}
    private int _maxHealth;    
    

    public EnemyHealth(Enemy enemy){
        this.enemy = enemy;
        _maxHealth = enemy.enemyData.maxHealth;
        _health = _maxHealth;
    }    

    public void Die()
    {       
        enemy.eventBus.Publish(new OnEnemyDiedEvent(enemy));
    }

    public void TakeDamage(int damage)
    {        
       _health -= (int)damage;
       _health = Mathf.Clamp(_health, 0, _maxHealth);
       if(_health == 0) {
           Die();
       }      
    }   

    public void AddHealth(int count)
    {
        _health += count;
    }
}
