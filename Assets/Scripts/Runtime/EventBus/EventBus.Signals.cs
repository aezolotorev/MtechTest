using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomEventBus.Signals
{
    
    public class OnAddHealthPlayerEvent
    {
        public readonly int Value;

        public OnAddHealthPlayerEvent(int value)
        {
            Value = value;
        }
    }
   
    public class OnHealthPlayerChangedEvent
    {
        public readonly int Health;
        public OnHealthPlayerChangedEvent(int health)
        {
            Health = health;
        }
    }
    
    public class OnPlayerDamagedEvent
    {
        public readonly int Health;

        public OnPlayerDamagedEvent(int health)
        {
            Health = health;
        }
    }
    public class OnPlayerAddHealthEvent
    {
        public readonly int Health;

        public OnPlayerAddHealthEvent(int health)
        {
            Health = health;
        }
    }
    public class OnPlayerDeadEvent
    {
        public OnPlayerDeadEvent() { }
    }


    public class OnEnemyDamagedEvent{
        public readonly int Health;
        public Enemy _enemy;
        public OnEnemyDamagedEvent(int health, Enemy enemy){
            Health = health;
            _enemy = enemy;
        }
    }
    public class OnEnemyDiedEvent{        
        public Enemy _enemy;
        public OnEnemyDiedEvent( Enemy enemy){           
            _enemy = enemy;
        }
    }

    public class OnAllEnemyDiedEvent{
        public OnAllEnemyDiedEvent(){}
    }

    public class OnLevelEndEvent{
        public OnLevelEndEvent(){}
    }

    public class OnLooseLevelEvent{
        public OnLooseLevelEvent(){}
    }
}
