using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
     public override void OnTriggerEnter2D(Collider2D other) {         
        if (other.gameObject.TryGetComponent(out Player player)){
            SetDamage(player.health, bulletData.damage);           
            DisableBullet();            
        }
    }
}
