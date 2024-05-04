using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;
using Zenject;

public class PlayerHealth : IHealth
{
    EventBus eventBus;   
    private int _health;    
    private int _maxHealth;
    

    [Inject]
    public void Construct(EventBus eventBus, GameConfig gameConfig)
    {
        this.eventBus = eventBus;
         _health = gameConfig.playerConfig.health;   
        _maxHealth = _health;
    }
   

    public void Die()
    {
       eventBus.Publish(new OnPlayerDeadEvent());
    }

    public void TakeDamage(int damage)
    {
       _health -= (int)damage;
       _health = Mathf.Clamp(_health, 0, _maxHealth);
       if(_health == 0) {
           Die();           
       }      
       eventBus.Publish(new OnPlayerDamagedEvent(_health));
    }
     public void AddHealth(int count)
    {
       _health += count;
       _health = Mathf.Clamp(_health, 0, _maxHealth);        
       eventBus.Publish(new OnPlayerAddHealthEvent(_health));
    }

  
}
