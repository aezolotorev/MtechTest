using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System.Threading;
using Cysharp.Threading.Tasks;
using System;


public class PlayerWeapon : MonoBehaviour
{
    private BulletPool bulletPool;
    private WeaponConfig weaponConfig;

    private BulletType curBulletType;
    private WeaponData curWeapon;      
    

    private float shootPointOffset=0.5f;

    private CancellationTokenSource cancellationTokenSource;
    [Inject]
    public void Construct(BulletPool bulletPool, WeaponConfig weaponConfig)
    {
        this.bulletPool = bulletPool;
        this.weaponConfig = weaponConfig;
        cancellationTokenSource = new CancellationTokenSource();
    }

    private void Start()
    {       
        SetDefaultWeapon();             
    }

    private void SetDefaultWeapon(){
        
        SetWeapon(weaponConfig.weapons[0].weaponType);
        curBulletType = BulletType.Common;
    }

    private void Update(){
        if(Input.GetKeyDown(KeyCode.Q)){
            SetWeapon(weaponConfig.weapons[1].weaponType);
        }
        if(Input.GetKeyDown(KeyCode.E)){
            SetWeapon(weaponConfig.weapons[2].weaponType);
        }
    }
    private void OnDestroy()
    {
        cancellationTokenSource?.Cancel();
    }

    private async UniTask Attacker(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(curWeapon.weaponDelay), cancellationToken: cancellationToken);
            Shoot();
        }
    }
  

    private void Shoot(){
        GetBullets(curBulletType);
    }

    public void SetWeapon(WeaponType weaponType)
    {
        cancellationTokenSource?.Cancel();
        cancellationTokenSource = new CancellationTokenSource();

       
        curWeapon = weaponConfig.weapons.Find(x => x.weaponType.Equals(weaponType));
        Attacker(cancellationTokenSource.Token).Forget();
    }

    public void GetBullets(BulletType bulletType){
       
            var curBullet = bulletPool.GetBullet(bulletType);
            curBullet.gameObject.SetActive(true);
            curBullet.transform.position = transform.position;
               
    }
        public void SetBullets(BulletType bulletType){
            curBulletType = bulletType;
       
        }
               
    



}
