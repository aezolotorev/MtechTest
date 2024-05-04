using System;
using UniRx;
using UnityEngine;

public class Bullet : MonoBehaviour, IDamagable, IMovable, IDisposable
{
    [SerializeField] protected SpriteRenderer spriteRenderer;
    private Sprite sprite;
    

    protected BulletData bulletData;
    protected CompositeDisposable _disposables= new CompositeDisposable();     
 
       
   
    public void Move(Transform movableTransform, Vector2 moveVector, float speed, float delta)
    {
        Vector3 moveDir = new Vector3(moveVector.x, moveVector.y, 0);
        Vector3 newPosition = movableTransform.position + moveDir * delta * speed;
        movableTransform.position = newPosition;        
    }

    public void SetDamage(IHealth health, int damage)
    {
        health.TakeDamage(damage);
    }

    public virtual void OnTriggerEnter2D(Collider2D other) { }       
      


    public void Setup(BulletData bulletData)
    {
        this.bulletData = bulletData;
        sprite = bulletData.sprite;
        spriteRenderer.sprite = sprite;       
    }

    public Vector3 GetDirection()
    {        
        if (this is PlayerBullet)
        {
            return Vector3.up;
        }
        else 
        {
            return Vector3.down;
        }

    }

    private  void OnEnable()
    {        
        Observable.EveryUpdate()
            .Subscribe(_ => Move(transform, GetDirection(), bulletData.speed, Time.deltaTime))
            .AddTo(_disposables);

         Observable.Timer(TimeSpan.FromSeconds(bulletData.lifeTime))
        .Subscribe(_ =>
        {            
            DisableBullet();
        })
        .AddTo(_disposables);
    }

    
    public void DisableBullet()
    {           
         _disposables.Clear(); 
         gameObject.SetActive(false);       
    }

   

    private void OnDisable()
    {        
        _disposables.Clear();
              
    }

    public void Dispose()
    {
        _disposables.Clear();
    }
}
