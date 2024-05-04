using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
[CreateAssetMenu(menuName = "MTechTestProject/BulletData")]
public class BulletData : ScriptableObject
{
    public int damage;
    public float speed;
    public float lifeTime;
    public Sprite sprite;
    public BulletType bulletType;    
}

public enum BulletType{
    Common, Rare, Epic, Legendary
}
