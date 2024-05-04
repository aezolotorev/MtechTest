using System;
using System.Collections;
using System.Collections.Generic;
using CustomEventBus.Signals;
using TMPro;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;    

    public IHealth  health;

    public IMovable movable;    

    public PlayerConfig playerConfig;

    private PlayerInput playerInput;

    private Rect cameraRect;    
    private Bounds spriteBounds;

    private float minX, maxX, minY, maxY;
    private PlayerWeapon playerWeapon;

    private bool isAlive=true;

    private CustomEventBus.EventBus eventBus;

    [Inject]
    public void Construct(IHealth health, GameConfig gameConfig, IMovable movable, PlayerInput playerInput, CustomEventBus.EventBus eventBus)
    {       
        this.playerConfig = gameConfig.playerConfig;
        this.health = health;  
        spriteRenderer.sprite = playerConfig.sprite;    
        this.movable= movable;    
        this.playerInput = playerInput;
        this.eventBus = eventBus; 
           
    }
    private void Start()
    {
        eventBus.Subscribe<OnPlayerDeadEvent>(LooseLevel); 
        var bottomLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
        var topRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight));
        cameraRect = new Rect(bottomLeft.x, bottomLeft.y, topRight.x - bottomLeft.x, topRight.y - bottomLeft.y);
        spriteBounds = spriteRenderer.bounds;
        minX = cameraRect.xMin + spriteBounds.extents.x;
        maxX = cameraRect.xMax - spriteBounds.extents.x;
        minY = cameraRect.yMin + spriteBounds.extents.y;
        maxY = cameraRect.yMax - spriteBounds.extents.y;
        AdjustBoxColliderSize();
        playerWeapon = GetComponentInChildren<PlayerWeapon>();
        health.TakeDamage(0);
    }    

    void Update()
    {
        if(!isAlive) return;
        playerInput.Tick();
        movable.Move(transform, playerInput.input.Move, playerConfig.speed, Time.deltaTime);
    }

    private void LateUpdate()
    {
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    private void LooseLevel(OnPlayerDeadEvent @event)
    {
        isAlive = false;       
        Destroy(gameObject);        
    }

    
    private void AdjustBoxColliderSize()
    {

        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();

        if (spriteRenderer != null && boxCollider != null)
        {
            boxCollider.size = spriteBounds.size;
        }
    }

    

    public void ApplyBenefit(BenefitType benefitType){
        switch (benefitType){
            case BenefitType.health:
                health.AddHealth(playerConfig.health);
                break;
            case BenefitType.rareBullet:
                playerWeapon.SetBullets(bulletType: BulletType.Rare);
                break;
            case BenefitType.epicBullet:
                playerWeapon.SetBullets(bulletType: BulletType.Epic);
                break;
            case BenefitType.legendaryBullet:
                playerWeapon.SetBullets(bulletType: BulletType.Legendary);
                break;
            case BenefitType.rareWeapon:
                playerWeapon.SetWeapon(weaponType: WeaponType.Rare);
                break;
            case BenefitType.epicWeapon:
                playerWeapon.SetWeapon(weaponType: WeaponType.Epic);
                break;
            case BenefitType.legendaryWeapon:
                playerWeapon.SetWeapon(weaponType: WeaponType.Legendary);
                break;


        }
    }

    public void  OnDestroy()
    {       
        eventBus.Unsubscribe<OnPlayerDeadEvent>(LooseLevel); 
    }
}
