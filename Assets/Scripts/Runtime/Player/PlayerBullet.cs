using System;
using System.Collections;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using Zenject;

public class PlayerBullet : Bullet
{
   

    public override void OnTriggerEnter2D(Collider2D other) {         
        if (other.gameObject.TryGetComponent(out Enemy enemy)){
            SetDamage(enemy.health, bulletData.damage);           
            DisableBullet();            
        }
    }
    
}
