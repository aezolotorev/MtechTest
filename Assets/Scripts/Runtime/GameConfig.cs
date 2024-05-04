using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject.SpaceFighter;
[CreateAssetMenu(menuName ="MTechTestProject/GameConfig")]
public class GameConfig : ScriptableObject
{
    public  EnemyConfig enemyConfig;
    public  PlayerConfig playerConfig;  


}
