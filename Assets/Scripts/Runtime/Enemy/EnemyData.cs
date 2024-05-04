using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="MTechTestProject/EnemyData")]
public class EnemyData : ScriptableObject 
{
    public string nameEnemy;
    public int maxHealth;
    public int damage;
    public int score;   
    public Sprite sprite;
}

