using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BulletPool : MonoBehaviour
{
    private const int MAX_POOL_SIZE = 100;
   
    [SerializeField] public List<Bullets> bullets = new();
    
    [SerializeField]public PlayerBullet playerBulletPrefab;
    public BulletConfig bulletConfig;

    public Spawner spawner;
    [Inject]
    public void Construct( BulletConfig bulletConfig, Spawner spawner){
       
        this.bulletConfig = bulletConfig;
        this.spawner = spawner;
    }


    private void Start(){
        InitPools(bulletConfig.commonBulletData);
        InitPools(bulletConfig.rareBulletData);
        InitPools(bulletConfig.epicBulletData);
        InitPools(bulletConfig.legendaryBulletData);
    }    

    private void InitPools(BulletData bulletData)
    {
        List<PlayerBullet> curBullets = new List<PlayerBullet>();
        GameObject pool =new GameObject("Pool"+bulletData.name);
        for(int i = 0; i < MAX_POOL_SIZE; i++) {           
            PlayerBullet bullet = (PlayerBullet)spawner.Spawn(playerBulletPrefab, pool.transform.position, Quaternion.identity, pool.transform);
            bullet.Setup(bulletData);
            bullet.gameObject.SetActive(false);
            curBullets.Add(bullet);
        } 
        Bullets _bullet = new Bullets(bulletData.bulletType, curBullets); 
        bullets.Add(_bullet);
    }    

    public PlayerBullet GetBullet(BulletType bulletType){
        PlayerBullet plBullet = null;
        Bullets curbullet = bullets.Find(b => b.bulletType == bulletType);
        foreach(var b in curbullet.playerBullets){
            if(!b.gameObject.activeInHierarchy){
                plBullet = b;
                break;                
            }
        }
        return plBullet;
    }
}
[Serializable]
public class Bullets
{
    public BulletType bulletType;
    public List<PlayerBullet> playerBullets = new();

    public Bullets(BulletType bulletType, List<PlayerBullet> playerBullets){
        this.bulletType = bulletType;
        this.playerBullets = playerBullets;
    }
}